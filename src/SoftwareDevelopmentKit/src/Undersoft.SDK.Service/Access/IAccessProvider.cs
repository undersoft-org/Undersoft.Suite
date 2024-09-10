using System.Security.Claims;

namespace Undersoft.SDK.Service.Access
{
    public interface IAccessProvider
    {
        IAuthorization Authorization { get; }

        Task<ClaimsPrincipal> CurrentState();
    }
}