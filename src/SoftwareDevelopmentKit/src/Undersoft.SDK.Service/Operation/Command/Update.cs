using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Command;

public class Update<TStore, TEntity, TDto> : Command<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>>[] Conditions { get; }

    public Update(EventPublishMode publishPattern, TDto input, params object[] keys)
        : base(OperationType.Update, publishPattern, input, keys) { }

    public Update(
        EventPublishMode publishPattern,
        TDto input,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    ) : base(OperationType.Update, publishPattern, input)
    {
        Predicate = predicate;
    }

    public Update(
        EventPublishMode publishPattern,
        TDto input,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate,
        params Func<TDto, Expression<Func<TEntity, bool>>>[] conditions
    ) : base(OperationType.Update, publishPattern, input)
    {
        Predicate = predicate;
        Conditions = conditions;
    }
}
