using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Undersoft.SDK.Service.Server.Accounts;

public class RoleClaim : IdentityRoleClaim<long>, IIdentifiable, IAccountRoleClaim
{
    public RoleClaim() : base() { Id = Unique.NewId; }

    public new long Id { get; set; }

    public long TypeId { get; set; }

    public long AccountRoleId { get; set; }

    public virtual Role Role { get; set; }

    [NotMapped]
    public Claim Claim { get => this.ToClaim(); set => InitializeFromClaim(value); }
}
