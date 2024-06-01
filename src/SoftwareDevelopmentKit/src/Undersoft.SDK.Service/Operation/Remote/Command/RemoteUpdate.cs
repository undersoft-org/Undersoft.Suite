using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteUpdate<TStore, TDto, TModel> : RemoteCommand<TModel>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TDto, bool>>> Predicate { get; }

    [JsonIgnore]
    public Func<TDto, Expression<Func<TDto, bool>>>[] Conditions { get; }

    public RemoteUpdate(EventPublishMode publishPattern, TModel input, params object[] keys)
        : base(OperationType.Update, publishPattern, input, keys) { }

    public RemoteUpdate(
        EventPublishMode publishPattern,
        TModel input,
        Func<TDto, Expression<Func<TDto, bool>>> predicate
    ) : base(OperationType.Update, publishPattern, input)
    {
        Predicate = predicate;
    }

    public RemoteUpdate(
        EventPublishMode publishPattern,
        TModel input,
        Func<TDto, Expression<Func<TDto, bool>>> predicate,
        params Func<TDto, Expression<Func<TDto, bool>>>[] conditions
    ) : base(OperationType.Update, publishPattern, input)
    {
        Predicate = predicate;
        Conditions = conditions;
    }
}
