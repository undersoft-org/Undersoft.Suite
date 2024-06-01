using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountClaim : IdentityUserClaim<long>, IIdentifiable, IAccountClaim
{
    public AccountClaim() : base() { Id = Unique.NewId; }

    public new long Id { get; set; }

    public long TypeId { get; set; }

    [NotMapped]
    public Claim Claim { get => this.ToClaim(); set => InitializeFromClaim(value); }
}
