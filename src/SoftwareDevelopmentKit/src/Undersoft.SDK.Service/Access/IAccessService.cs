namespace Undersoft.SDK.Service.Access
{
    public interface IAccessService<TAccount> : IAccess where TAccount : class, IOrigin, IAuthorization
    {
        Task<TAccount> Register(TAccount account);
        Task<TAccount> Unregister(TAccount account);
        Task<TAccount> Registered(TAccount account);
    }
}