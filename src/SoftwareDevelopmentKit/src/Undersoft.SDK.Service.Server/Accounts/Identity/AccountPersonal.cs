using Undersoft.SDK.Service.Access.Identity;

namespace Undersoft.SDK.Service.Server.Accounts.Identity;

public class AccountPersonal : Personal, IEntity
{
    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
