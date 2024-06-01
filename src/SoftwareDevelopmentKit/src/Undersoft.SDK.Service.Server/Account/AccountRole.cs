using Microsoft.AspNetCore.Identity;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountRole : IdentityUserRole<long>, IIdentifiable
{
    public long Id { get; set; }

    public long TypeId { get; set; }
}
