using Undersoft.SDK.Service.Data.Object;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Operation.Command;

public class CreateSet<TStore, TEntity, TDto> : CommandSet<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TEntity, Expression<Func<TEntity, bool>>> Predicate { get; }

    public CreateSet(EventPublishMode publishPattern, TDto input, object key)
        : base(
            OperationType.Create,
            publishPattern,
            new[] { new Create<TStore, TEntity, TDto>(publishPattern, input, key) }
        )
    { }

    public CreateSet(EventPublishMode publishPattern, TDto[] inputs)
        : base(
            OperationType.Create,
            publishPattern,
            inputs
                .Select(input => new Create<TStore, TEntity, TDto>(publishPattern, input))
                .ToArray()
        )
    { }

    public CreateSet(
        EventPublishMode publishPattern,
        TDto[] inputs,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicate
    )
        : base(
            OperationType.Create,
            publishPattern,
            inputs
                .Select(
                    input => new Create<TStore, TEntity, TDto>(publishPattern, input, predicate)
                )
                .ToArray()
        )
    {
        Predicate = predicate;
    }
}
