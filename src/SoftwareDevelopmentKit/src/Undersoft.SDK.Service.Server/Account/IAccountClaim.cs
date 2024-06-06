using System.Security.Claims;

namespace Undersoft.SDK.Service.Server.Accounts
{
    public interface IAccountClaim : IIdentifiable
    {
        Claim Claim { get; }
    }
}