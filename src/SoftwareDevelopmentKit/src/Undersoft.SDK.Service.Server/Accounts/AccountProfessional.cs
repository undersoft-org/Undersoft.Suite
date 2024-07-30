using Undersoft.SDK.Service.Access.Models;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountProfessional : Professional
{
    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
