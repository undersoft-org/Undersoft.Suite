using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

public class ChangeDocument<TStore, TEntity, TDto> : Command<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    public ChangeDocument(EventPublishMode publishMode, TDto input, params object[] keys)
        : base(OperationType.Change, publishMode, input, keys) { }

    public ChangeDocument(
        EventPublishMode publishMode,
        TDto input,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    ) : base(OperationType.Change, publishMode, input)
    {
        Predicate = predicate;
    }
}
