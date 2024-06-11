using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Object.Setting;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Undersoft.SDK.Serialization;
using Undersoft.SDK.Service.Data.Identifier;

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class ObjectSetting<TSetting, TKind> : DataObject, ISerializableJsonDocument, ISetting
    where TSetting : class, ISetting
    where TKind : struct, Enum
{
    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    internal IJsonDocumentSerializer _serializer;

    public ObjectSetting() : base()
    {
        _serializer = new JsonDocumentSerializer(this);
    }

    public ObjectSetting(TKind kind) : base()
    {
        Kind = kind;
    }

    [DataMember(Order = 12)]
    public virtual byte[] Data { get; set; }

    [DataMember(Order = 13)]
    public virtual JsonDocument Document { get; set; }

    [DataMember(Order = 14)]
    public virtual string Name { get; set; }

    [DataMember(Order = 15)]
    public virtual IdentifierSet<TSetting> Identifiers { get; set; }

    [DataMember(Order = 16)]
    public TKind Kind { get; set; }

    public virtual T GetDetail<T>()
    {
        return _serializer.GetDetail<T>();
    }

    public virtual object GetDetail()
    {
        return _serializer.GetDetail();
    }

    public virtual void SetGeneral<T>(T structure)
    {
        _serializer.SetGeneral(structure);
    }

    public virtual void SetGeneral(object structure)
    {
        _serializer.SetGeneral(structure);
    }
}
