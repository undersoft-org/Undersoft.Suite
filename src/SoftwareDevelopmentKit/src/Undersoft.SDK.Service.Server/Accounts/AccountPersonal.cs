using Undersoft.SDK.Service.Access.Models;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountPersonal : Personal, IEntity
{
    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
