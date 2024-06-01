using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

public class DeleteSet<TStore, TEntity, TDto> : CommandSet<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    public DeleteSet(EventPublishMode publishPattern, object key)
        : base(
            OperationType.Create,
            publishPattern,
            new[] { new Delete<TStore, TEntity, TDto>(publishPattern, key) }
        )
    { }

    public DeleteSet(EventPublishMode publishPattern, TDto input, object key)
        : base(
            OperationType.Create,
            publishPattern,
            new[] { new Delete<TStore, TEntity, TDto>(publishPattern, input, key) }
        )
    { }

    public DeleteSet(EventPublishMode publishPattern, TDto[] inputs)
        : base(
            OperationType.Delete,
            publishPattern,
            inputs
                .Select(input => new Delete<TStore, TEntity, TDto>(publishPattern, input))
                .ToArray()
        )
    { }

    public DeleteSet(
        EventPublishMode publishPattern,
        TDto[] inputs,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    )
        : base(
            OperationType.Delete,
            publishPattern,
            inputs
                .Select(
                    input => new Delete<TStore, TEntity, TDto>(publishPattern, input, predicate)
                )
                .ToArray()
        )
    {
        Predicate = predicate;
    }
}
