using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command.Notification;

using Command;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;

public class ChangedDocument<TStore, TEntity, TDto> : Notification<Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    public ChangedDocument(Command<TDto> command) : base(command) { }

    public ChangedDocument(ChangeDocument<TStore, TEntity, TDto> command) : base(command)
    {
        Predicate = command.Predicate;
    }

    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }
}
