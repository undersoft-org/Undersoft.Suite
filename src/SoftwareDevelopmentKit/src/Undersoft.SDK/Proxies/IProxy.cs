namespace Undersoft.SDK.Proxies;

using Rubrics;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK.Instant;

public interface IProxy<T> : IProxy
{
    object this[Expression<Func<T, object>> member] { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    new T Target { get; set; }
}

public interface IProxy : IInstant
{
    [JsonIgnore]
    [IgnoreDataMember]
    IRubrics Rubrics { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    object Target { get; set; }
}
