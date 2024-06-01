using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteUpdateSet<TStore, TDto, TModel> : RemoteCommandSet<TModel>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    [JsonIgnore]
    public Func<TModel, Expression<Func<TDto, bool>>> Predicate { get; }

    [JsonIgnore]
    public Func<TModel, Expression<Func<TDto, bool>>>[] Conditions { get; }

    public RemoteUpdateSet(EventPublishMode publishPattern, TModel input, object key)
        : base(
            OperationType.Change,
            publishPattern,
            new[] { new RemoteUpdate<TStore, TDto, TModel>(publishPattern, input, key) }
        )
    { }

    public RemoteUpdateSet(EventPublishMode publishPattern, TModel[] inputs)
        : base(
            OperationType.Update,
            publishPattern,
            inputs
                .Select(input => new RemoteUpdate<TStore, TDto, TModel>(publishPattern, input))
                .ToArray()
        )
    { }

    public RemoteUpdateSet(
        EventPublishMode publishPattern,
        TModel[] inputs,
        Func<TModel, Expression<Func<TDto, bool>>> predicate
    )
        : base(
            OperationType.Update,
            publishPattern,
            inputs
                .Select(
                    input =>
                        new RemoteUpdate<TStore, TDto, TModel>(publishPattern, input, predicate)
                )
                .ToArray()
        )
    {
        Predicate = predicate;
    }

    public RemoteUpdateSet(
        EventPublishMode publishPattern,
        TModel[] inputs,
        Func<TModel, Expression<Func<TDto, bool>>> predicate,
        params Func<TModel, Expression<Func<TDto, bool>>>[] conditions
    )
        : base(
            OperationType.Update,
            publishPattern,
            inputs
                .Select(
                    input =>
                        new RemoteUpdate<TStore, TDto, TModel>(
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
