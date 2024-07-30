using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

public class ChangeDocumentSet<TStore, TEntity, TDto> : CommandSet<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    public ChangeDocumentSet(EventPublishMode publishPattern, TDto input, object key)
        : base(
            OperationType.Change,
            publishPattern,
            new[] { new ChangeDocument<TStore, TEntity, TDto>(publishPattern, input, key) }
        )
    { }

    public ChangeDocumentSet(EventPublishMode publishPattern, TDto[] inputs)
        : base(
            OperationType.Change,
            publishPattern,
            inputs.Select(c => new ChangeDocument<TStore, TEntity, TDto>(publishPattern, c, c.Id)).ToArray()
        )
    { }

    public ChangeDocumentSet(
        EventPublishMode publishPattern,
        TDto[] inputs,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    )
        : base(
            OperationType.Change,
            publishPattern,
            inputs
                .Select(c => new ChangeDocument<TStore, TEntity, TDto>(publishPattern, c, predicate))
                .ToArray()
        )
    {
        Predicate = predicate;
    }
}
