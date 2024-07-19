using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command.Notification;

using Command;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;

public class GeneralizedChangedSet<TStore, TEntity, TDto> : NotificationSet<Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    [JsonIgnore]
    public Func<TDto, Expression<Func<TEntity, bool>>> Predicate { get; }

    public GeneralizedChangedSet(GeneralizedChangeSet<TStore, TEntity, TDto> commands)
        : base(
            commands.PublishMode,
            commands
                .ForOnly(
                    c => c.Result != null,
                    c => new GeneralizedChanged<TStore, TEntity, TDto>((GeneralizedChange<TStore, TEntity, TDto>)c)
                )
                .ToArray()
        )
    {
        Predicate = commands.Predicate;
    }
}
