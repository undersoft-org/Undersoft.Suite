using System.Security.Claims;

namespace Undersoft.SDK.Service.Server.Accounts
{
    public interface IAccountRoleClaim : IIdentifiable
    {
        Claim Claim { get; }
    }
}