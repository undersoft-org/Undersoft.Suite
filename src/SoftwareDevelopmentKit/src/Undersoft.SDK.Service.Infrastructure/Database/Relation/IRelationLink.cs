using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Infrastructure.Database.Relation;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Object;

public interface IRelationLink<TLeft, TRight> : IDataObject where TLeft : class, IOrigin, IInnerProxy where TRight : class, IOrigin, IInnerProxy
{
    [JsonIgnore]
    TLeft? LeftEntity { get; set; }
    long? LeftEntityId { get; set; }
    [JsonIgnore]
    TRight? RightEntity { get; set; }
    long? RightEntityId { get; set; }
}