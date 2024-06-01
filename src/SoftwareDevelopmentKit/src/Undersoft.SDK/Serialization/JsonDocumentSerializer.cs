using Undersoft.SDK.Extracting;
using Undersoft.SDK.Rubrics.Attributes;

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Serialization;

public class JsonDocumentSerializer : ISerializableJsonDocument, IJsonDocumentSerializer
{
    internal object _structure;
    internal ISerializableJsonDocument _container;
    internal Type _type;

    public JsonDocumentSerializer() { }

    public JsonDocumentSerializer(ISerializableJsonDocument container)
    {
        _container = container;
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
                        GetObject();
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

    public JsonDocument Document { get => _container.Document; set => _container.Document = value; }
    public string TypeName { get => _container.TypeName; set => _container.TypeName = value; }

    public virtual JsonDocument ToDocument<T>(T detail)
    {
        if (Type != null)
        {
            return detail.ToJsonDocument<T>();
        }
        return null;
    }

    public virtual JsonDocument ToDocument(object detail)
    {
        if (Type != null)
        {
            return detail.ToJsonDocument(Type);
        }
        return null;
    }

    public virtual JsonElement ToElement<T>(T detail)
    {
        if (Type != null)
        {
            return detail.ToJsonElement<T>();
        }
        return default(JsonElement);
    }

    public virtual JsonElement ToElement(object detail)
    {
        if (Type != null)
        {
            return detail.ToJsonElement(Type);
        }
        return default(JsonElement);
    }

    public virtual T FromDocument<T>(JsonDocument doc)
    {
        if (Type != null)
        {
            return doc.FromJsonDocument<T>();
        }
        return default(T);
    }

    public virtual object FromDocument(JsonDocument doc)
    {
        if (Type != null)
        {
            return Type.FromJsonDocument(doc);
        }
        return null;
    }

    public virtual T FromElement<T>(JsonElement ele)
    {
        if (Type != null)
        {
            return ele.FromJsonElement<T>();
        }
        return default(T);
    }

    public virtual object FromElement(JsonElement ele, Type type)
    {
        if (Type != null)
        {
            return type.FromJsonElement(ele);
        }
        return default(JsonElement);
    }

    public virtual byte[] Serialize<T>(T detail)
    {
        if (Type != null)
        {
            return detail.ToJsonBytes(Type);
        }
        return null;
    }

    public virtual byte[] Serialize(object detail)
    {
        if (Type != null)
        {
            return detail.ToJsonBytes(Type);
        }
        return null;
    }

    public virtual T Deserialize<T>(byte[] data)
    {
        if (Type != null)
        {
            return data.FromJson<T>();
        }
        return default(T);
    }

    public virtual object Deserialize(byte[] data)
    {
        if (Type != null)
        {
            return data.FromJson(Type);
        }
        return null;
    }

    public virtual T GetObject<T>()
    {
        if (Document != null)
            return (T)(_structure ??= FromDocument<T>(Document));
        return default(T);
    }

    public virtual object GetObject()
    {
        if (Document != null)
            return _structure ??= FromDocument(Document);
        return null;
    }

    public virtual T GetProperty<T>(Func<JsonDocument, JsonElement> select)
    {
        if (Document != null)
            return (T)(_structure ??= FromElement<T>(select(Document)));
        return default(T);
    }

    public virtual object GetProperty(Type type, Func<JsonDocument, JsonElement> select)
    {
        if (Document != null)
            return _structure ??= FromElement(select(Document), type);
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
}
