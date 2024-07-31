using Undersoft.SDK.Service.Access.Identity;

namespace Undersoft.SDK.Service.Server.Accounts.Identity;

public class AccountProfessional : Professional
{
    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
