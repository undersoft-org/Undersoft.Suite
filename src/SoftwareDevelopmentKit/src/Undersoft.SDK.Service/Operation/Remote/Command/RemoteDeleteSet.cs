using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteDeleteSet<TStore, TDto, TModel> : RemoteCommandSet<TModel>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    [JsonIgnore]
    public Func<TModel, Expression<Func<TDto, bool>>> Predicate { get; }

    public RemoteDeleteSet(EventPublishMode publishPattern, object key)
        : base(
            OperationType.Create,
            publishPattern,
            new[] { new RemoteDelete<TStore, TDto, TModel>(publishPattern, key) }
        )
    { }

    public RemoteDeleteSet(EventPublishMode publishPattern, TModel input, object key)
        : base(
            OperationType.Create,
            publishPattern,
            new[] { new RemoteDelete<TStore, TDto, TModel>(publishPattern, input, key) }
        )
    { }

    public RemoteDeleteSet(EventPublishMode publishPattern, TModel[] inputs)
        : base(
            OperationType.Delete,
            publishPattern,
            inputs
                .Select(input => new RemoteDelete<TStore, TDto, TModel>(publishPattern, input))
                .ToArray()
        )
    { }

    public RemoteDeleteSet(
        EventPublishMode publishPattern,
        TModel[] inputs,
        Func<TModel, Expression<Func<TDto, bool>>> predicate
    )
        : base(
            OperationType.Delete,
            publishPattern,
            inputs
                .Select(
                    input => new RemoteDelete<TStore, TDto, TModel>(publishPattern, input, predicate)
                )
                .ToArray()
        )
    {
        Predicate = predicate;
    }
}
