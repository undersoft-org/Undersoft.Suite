using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteCreateSet<TStore, TDto, TModel> : RemoteCommandSet<TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TDto, bool>>> Predicate { get; }

    public RemoteCreateSet(EventPublishMode publishPattern, TModel input, object key)
        : base(
            OperationType.Create,
            publishPattern,
            new[] { new RemoteCreate<TStore, TDto, TModel>(publishPattern, input, key) }
        )
    { }

    public RemoteCreateSet(EventPublishMode publishPattern, TModel[] inputs)
        : base(
            OperationType.Create,
            publishPattern,
            inputs
                .Select(input => new RemoteCreate<TStore, TDto, TModel>(publishPattern, input))
                .ToArray()
        )
    { }

    public RemoteCreateSet(
        EventPublishMode publishPattern,
        TModel[] inputs,
        Func<TDto, Expression<Func<TDto, bool>>> predicate
    )
        : base(
            OperationType.Create,
            publishPattern,
            inputs
                .Select(
                    input => new RemoteCreate<TStore, TDto, TModel>(publishPattern, input, predicate)
                )
                .ToArray()
        )
    {
        Predicate = predicate;
    }
}
