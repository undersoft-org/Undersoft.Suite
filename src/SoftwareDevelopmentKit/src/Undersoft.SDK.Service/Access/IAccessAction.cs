namespace Undersoft.SDK.Service.Access
{
    public interface IAccessAction
    {
        Task<IAuthorization> ConfirmEmail(IAuthorization account);
        Task<IAuthorization> ResetPassword(IAuthorization account);
    }
}