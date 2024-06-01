namespace Undersoft.SDK.Proxies;

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

public interface IInnerProxy : IIdentifiable
{
    [JsonIgnore]
    [IgnoreDataMember]
    [NotMapped]
    object this[string propertyName] { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [NotMapped]
    object this[int fieldId] { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [NotMapped]
    IProxy Proxy { get; }
}