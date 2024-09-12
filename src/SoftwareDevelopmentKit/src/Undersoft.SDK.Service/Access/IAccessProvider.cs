using System.Security.Claims;

namespace Undersoft.SDK.Service.Access
{
    public interface IAccessProvider
    {
        IAuthorization Authorization { get; }

        DateTime? AccessExpiration { get; }

        Task<ClaimsPrincipal> CurrentState();

        Task<ClaimsPrincipal> RefreshState();
    }
}