using Microsoft.AspNetCore.Identity;

namespace Undersoft.SDK.Service.Server.Accounts;

public class Role : IdentityRole<long>, IIdentifiable, IAccountRole
{
    public Role() : base() { Id = Unique.NewId; }
    public Role(string roleName) : base(roleName) { Id = Unique.NewId; }

    public long TypeId { get; set; }

    public virtual ObjectSet<RoleClaim> Claims { get; set; }

    public virtual ObjectSet<Account> Accounts { get; set; }
}
