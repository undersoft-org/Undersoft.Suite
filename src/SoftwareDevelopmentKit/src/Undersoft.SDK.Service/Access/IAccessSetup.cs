namespace Undersoft.SDK.Service.Access
{
    public interface IAccessSetup
    {
        Task<IAuthorization> ChangePassword(IAuthorization account);
    }
}