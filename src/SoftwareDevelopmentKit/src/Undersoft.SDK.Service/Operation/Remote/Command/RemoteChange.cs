using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteChange<TStore, TDto, TModel> : RemoteCommand<TModel>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    [JsonIgnore]
    public Func<TModel, Expression<Func<TDto, bool>>> Predicate { get; }

    public RemoteChange(EventPublishMode publishMode, TModel input, params object[] keys)
        : base(OperationType.Change, publishMode, input, keys) { }

    public RemoteChange(
        EventPublishMode publishMode,
        TModel input,
        Func<TModel, Expression<Func<TDto, bool>>> predicate
    ) : base(OperationType.Change, publishMode, input)
    {
        Predicate = predicate;
    }
}
