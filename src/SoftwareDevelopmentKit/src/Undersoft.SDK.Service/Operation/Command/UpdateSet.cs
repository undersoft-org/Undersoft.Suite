using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Command;

public class UpdateSet<TStore, TEntity, TDto> : CommandSet<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>>[] Conditions { get; }

    public UpdateSet(EventPublishMode publishPattern) : base(OperationType.Update)
    {
        PublishMode = publishPattern;
    }

    public UpdateSet(EventPublishMode publishPattern, TDto input, object key)
        : base(
            OperationType.Change,
            publishPattern,
            new[] { new Update<TStore, TEntity, TDto>(publishPattern, input, key) }
        )
    { }

    public UpdateSet(EventPublishMode publishPattern, TDto[] inputs)
        : base(
            OperationType.Update,
            publishPattern,
            inputs
                .Select(input => new Update<TStore, TEntity, TDto>(publishPattern, input))
                .ToArray()
        )
    { }

    public UpdateSet(
        EventPublishMode publishPattern,
        TDto[] inputs,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    )
        : base(
            OperationType.Update,
            publishPattern,
            inputs
                .Select(
                    input => new Update<TStore, TEntity, TDto>(publishPattern, input, predicate)
                )
                .ToArray()
        )
    {
        Predicate = predicate;
    }

    public UpdateSet(
        EventPublishMode publishPattern,
        TDto[] inputs,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate,
        params Func<TDto, Expression<Func<TEntity, bool>>>[] conditions
    )
        : base(
            OperationType.Update,
            publishPattern,
            inputs
                .Select(
                    input =>
                        new Update<TStore, TEntity, TDto>(
                            publishPattern,
                            input,
                            predicate,
                            conditions
                        )
                )
                .ToArray()
        )
    {
        Predicate = predicate;
        Conditions = conditions;
    }
}
