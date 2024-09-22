using System.Security.Claims;

namespace Undersoft.SDK.Service.Access
{
    public interface IAccess : IAccessAction, IAccessSetup
    {
        Task<IAuthorization> SignIn(IAuthorization account);
        Task<IAuthorization> SignOut(IAuthorization account);
        Task<IAuthorization> SignUp(IAuthorization account);
        Task<IAuthorization> SignedIn(IAuthorization account);
        Task<IAuthorization> SignedUp(IAuthorization account);

        Task<ClaimsPrincipal> RefreshAsync();
    }
}