using Microsoft.AspNetCore.Identity;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountToken : IdentityUserToken<long>, IIdentifiable
{
    public long Id { get; set; }

    public long TypeId { get; set; }

    public long AccountId { get; set; }
    public virtual Account Account {  get; set; }
}
