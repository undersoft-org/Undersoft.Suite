using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command;

using Undersoft.SDK.Service.Data.Object;

using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;

public class Create<TStore, TEntity, TDto> : Command<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TEntity, Expression<Func<TEntity, bool>>> Predicate { get; }

    public Create(EventPublishMode publishPattern, TDto input)
        : base(OperationType.Create, publishPattern, input)
    {
        input.AutoId();
    }

    public Create(EventPublishMode publishPattern, TDto input, object key)
        : base(OperationType.Create, publishPattern, input)
    {
        input.SetId(key);
    }

    public Create(
        EventPublishMode publishPattern,
        TDto input,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicate
    ) : base(OperationType.Create, publishPattern, input)
    {
        input.AutoId();
        Predicate = predicate;
    }
}
