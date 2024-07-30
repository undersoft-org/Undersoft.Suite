using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command.Notification;

using Command;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;

public class ChangedDocumentSet<TStore, TEntity, TDto> : NotificationSet<Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    public ChangedDocumentSet(ChangeDocumentSet<TStore, TEntity, TDto> commands)
        : base(
            commands.PublishMode,
            commands
                .ForOnly(
                    c => c.Result != null,
                    c => new ChangedDocument<TStore, TEntity, TDto>((ChangeDocument<TStore, TEntity, TDto>)c)
                )
                .ToArray()
        )
    {
        Predicate = commands.Predicate;
    }
}
