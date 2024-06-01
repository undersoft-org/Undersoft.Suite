using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Undersoft.SDK.Logging;
using Undersoft.SDK.Uniques;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;

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
