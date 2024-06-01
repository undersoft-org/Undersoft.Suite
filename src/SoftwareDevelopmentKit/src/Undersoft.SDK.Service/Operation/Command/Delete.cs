using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Command;

public class Delete<TStore, TEntity, TDto> : Command<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    public Delete(EventPublishMode publishPattern, TDto input)
        : base(OperationType.Delete, publishPattern, input) { }

    public Delete(
        EventPublishMode publishPattern,
        TDto input,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    ) : base(OperationType.Delete, publishPattern, input)
    {
        Predicate = predicate;
    }

    public Delete(
        EventPublishMode publishPattern,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    ) : base(OperationType.Delete, publishPattern)
    {
        Predicate = predicate;
    }

    public Delete(EventPublishMode publishPattern, params object[] keys)
        : base(OperationType.Delete, publishPattern, keys) { }
}
