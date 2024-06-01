using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command;

using Undersoft.SDK.Service.Data.Object;

using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;

public class ChangeSet<TStore, TEntity, TDto> : CommandSet<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    public ChangeSet(EventPublishMode publishPattern, TDto input, object key)
        : base(
            OperationType.Change,
            publishPattern,
            new[] { new Change<TStore, TEntity, TDto>(publishPattern, input, key) }
        )
    { }

    public ChangeSet(EventPublishMode publishPattern, TDto[] inputs)
        : base(
            OperationType.Change,
            publishPattern,
            inputs.Select(c => new Change<TStore, TEntity, TDto>(publishPattern, c, c.Id)).ToArray()
        )
    { }

    public ChangeSet(
        EventPublishMode publishPattern,
        TDto[] inputs,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    )
        : base(
            OperationType.Change,
            publishPattern,
            inputs
                .Select(c => new Change<TStore, TEntity, TDto>(publishPattern, c, predicate))
                .ToArray()
        )
    {
        Predicate = predicate;
    }
}
