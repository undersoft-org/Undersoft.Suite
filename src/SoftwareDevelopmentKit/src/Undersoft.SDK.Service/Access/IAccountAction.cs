
namespace Undersoft.SDK.Service.Access
{
    public interface IAccountAction
    {
        Task<IAuthorization> ConfirmEmail(IAuthorization account);
        Task<IAuthorization> ResetPassword(IAuthorization account);
    }
}