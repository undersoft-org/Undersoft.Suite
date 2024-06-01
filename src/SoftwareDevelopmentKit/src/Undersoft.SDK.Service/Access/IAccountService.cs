namespace Undersoft.SDK.Service.Access
{
    public interface IAccountService<TAccount> : IAccountAccess where TAccount : class, IOrigin, IAuthorization
    {
        Task<TAccount> Register(TAccount account);

        Task<TAccount> Unregister(TAccount account);

        Task<TAccount> Registered(TAccount account);

        Task<IAuthorization> CompleteRegistration(IAuthorization account);
    }
}