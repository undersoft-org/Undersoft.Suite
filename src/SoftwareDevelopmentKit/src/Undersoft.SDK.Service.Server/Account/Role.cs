using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Undersoft.SDK.Logging;
using Undersoft.SDK.Series;
using Undersoft.SDK.Uniques;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undersoft.SDK.Service.Server.Accounts;

public class Role : IdentityRole<long>, IIdentifiable, IAccountRole
{
    public Role() : base() { Id = Unique.NewId; }
    public Role(string roleName) : base(roleName) { Id = Unique.NewId; }

    public long TypeId { get; set; }

    public virtual Listing<RoleClaim> Claims { get; set; }

    public virtual Listing<Account> Accounts { get; set;}
}
