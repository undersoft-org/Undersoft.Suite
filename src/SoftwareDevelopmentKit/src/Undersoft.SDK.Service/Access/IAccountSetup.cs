namespace Undersoft.SDK.Service.Access
{
    public interface IAccountSetup
    {
        Task<IAuthorization> ChangePassword(IAuthorization account);
    }
}