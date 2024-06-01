using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteDelete<TStore, TDto, TModel> : RemoteCommand<TModel>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    [JsonIgnore]
    public Func<TModel, Expression<Func<TDto, bool>>> Predicate { get; }

    public RemoteDelete(EventPublishMode publishPattern, TModel input)
        : base(OperationType.Delete, publishPattern, input) { }

    public RemoteDelete(
        EventPublishMode publishPattern,
        TModel input,
        Func<TModel, Expression<Func<TDto, bool>>> predicate
    ) : base(OperationType.Delete, publishPattern, input)
    {
        Predicate = predicate;
    }

    public RemoteDelete(
        EventPublishMode publishPattern,
        Func<TModel, Expression<Func<TDto, bool>>> predicate
    ) : base(OperationType.Delete, publishPattern)
    {
        Predicate = predicate;
    }

    public RemoteDelete(EventPublishMode publishPattern, params object[] keys)
        : base(OperationType.Delete, publishPattern, keys) { }
}
