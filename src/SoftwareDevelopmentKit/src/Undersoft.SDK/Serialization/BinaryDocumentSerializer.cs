using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Serialization;

public class BinaryDocumentSerializer : IBinaryDocumentSerializable
{
    internal object _structure;
    internal IBinaryDocumentSerializable _container;
    internal IBinarySerializable _surrogate;
    internal Type _type;

    public BinaryDocumentSerializer() { }

    public BinaryDocumentSerializer(IBinaryDocumentSerializable container)
    {
        _container = container;
        _surrogate = BinarySerializer.EnsureGet(Type);
    }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual Type Type
    {
        get
        {
            if (_type == null)
            {
                if (TypeName == null)
                {
                    if (_structure == null)
                    {
                        GetStructure();
                    }
                    else
                    {
                        _type = _structure.GetType();
                        TypeName = _type.FullName;
                    }
                }
                else
                {
                    _type = Type.GetType(TypeName);
                }
            }
            return _type;
        }
    }

    public byte[] Document { get => _container.Document; set => _container.Document = value; }

    public string TypeName { get => _container.TypeName; set => _container.TypeName = value; }

    public virtual byte[] ToDocument<T>(T detail)
    {
        if (Type != null)
        {
            return detail.ToBinaryBytes<T>();
        }
        return null;
    }

    public virtual byte[] ToDocument(object detail)
    {
        if (Type != null)
        {
            return detail.ToBinaryBytes(Type);
        }
        return null;
    }

    public virtual T FromDocument<T>(byte[] doc)
    {
        if (Type != null)
        {
            return doc.FromBinary<T>();
        }
        return default(T);
    }

    public virtual object FromDocument(byte[] doc)
    {
        if (Type != null)
        {
            return Type.FromBinary(doc);
        }
        return null;
    }

    public virtual byte[] Serialize<T>(T detail)
    {
        if (Type != null)
        {
            return detail.ToBinaryBytes(Type);
        }
        return null;
    }

    public virtual byte[] Serialize(object detail)
    {
        if (Type != null)
        {
            return detail.ToBinaryBytes(Type);
        }
        return null;
    }

    public virtual T Deserialize<T>(byte[] data)
    {
        if (Type != null)
        {
            return data.FromBinary<T>();
        }
        return default(T);
    }

    public virtual object Deserialize(byte[] data)
    {
        if (Type != null)
        {
            return data.FromBinary(Type);
        }
        return null;
    }

    public virtual T GetStructure<T>()
    {
        if (Document != null)
            return (T)(_structure ??= FromDocument<T>(Document));
        return default(T);
    }

    public virtual object GetStructure()
    {
        if (Document != null)
            return _structure ??= FromDocument(Document);
        return null;
    }

    public virtual void SetDocument<T>(T structure)
    {
        _structure = structure;
        Document = ToDocument<T>(structure);
    }

    public virtual void SetDocument(object structure)
    {
        _structure = structure;
        Document = ToDocument(structure);
    }

    public virtual void SetElement<T, E>(E element, Func<T, E> select)
    {
        select((T)_structure).PatchFrom(element);
        Document = ToDocument(_structure);
    }

    public virtual void SetElement<T>(object element, Func<T, object> select)
    {
        select((T)_structure).PatchFrom(element);
        Document = ToDocument(_structure);
    }

    public void Serialize(BinaryWriter writer, object graph)
    {
        _surrogate.Serialize(writer, graph);
    }

    public object Deserialize(BinaryReader reader)
    {
        return _surrogate.Deserialize(reader);
    }

    public int GetTypeHandle()
    {
        return _surrogate.GetTypeHandle();
    }

    public void SetTypeHandle(int h)
    {
        _surrogate.SetTypeHandle(h);
    }
}
