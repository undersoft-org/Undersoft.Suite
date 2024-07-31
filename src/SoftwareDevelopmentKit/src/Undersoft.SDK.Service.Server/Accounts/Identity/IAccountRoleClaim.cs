using System.Security.Claims;

namespace Undersoft.SDK.Service.Server.Accounts.Identity
{
    public interface IAccountRoleClaim : IIdentifiable
    {
        Claim Claim { get; }
    }
}