using System.Security.Claims;

namespace Undersoft.SDK.Service.Server.Accounts.Identity
{
    public interface IAccountClaim : IIdentifiable
    {
        Claim Claim { get; }
    }
}