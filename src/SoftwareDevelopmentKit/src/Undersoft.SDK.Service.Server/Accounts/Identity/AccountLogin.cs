using Microsoft.AspNetCore.Identity;

namespace Undersoft.SDK.Service.Server.Accounts.Identity;

public class AccountLogin : IdentityUserLogin<long>, IIdentifiable
{
    public long Id { get; set; }

    public long TypeId { get; set; }
}
