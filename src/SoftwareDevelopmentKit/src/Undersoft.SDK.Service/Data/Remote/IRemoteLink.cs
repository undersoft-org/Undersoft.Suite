using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Data.Remote;

using System.Runtime.Serialization;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Object;

public interface IRemoteLink<TSource, TTarget> : IRemoteLink, IDataObject
    where TSource : class, IOrigin, IInnerProxy
    where TTarget : class, IOrigin, IInnerProxy
{
    [JsonIgnore]
    [IgnoreDataMember]
    TSource LeftEntity { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    TTarget RightEntity { get; set; }
}

public interface IRemoteLink : IDataObject
{
    long LeftEntityId { get; set; }

    long RightEntityId { get; set; }
}
