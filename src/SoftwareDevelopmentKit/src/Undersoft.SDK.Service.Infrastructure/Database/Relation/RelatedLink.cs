namespace Undersoft.SDK.Service.Infrastructure.Database.Relation;

using System.Text.Json.Serialization;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Object;

public class RelatedLink<TLeft, TRight> : RelatedLink, IRelatedLink<TLeft, TRight> where TLeft : class, IOrigin, IInnerProxy where TRight : class, IOrigin, IInnerProxy
{
    [JsonIgnore]
    public virtual TRight? RightEntity { get; set; }

    [JsonIgnore]
    public virtual TLeft? LeftEntity { get; set; }
}

public class RelatedLink : DataObject
{
    public virtual long? RightEntityId { get; set; }
    public virtual long? LeftEntityId { get; set; }
}