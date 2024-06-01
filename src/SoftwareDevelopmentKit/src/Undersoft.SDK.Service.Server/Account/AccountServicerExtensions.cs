namespace Undersoft.SDK.Service.Server.Accounts;

public static class AccountServicerExtensions
{
    public static AccountManager GetIdentityManager(this IServicer servicer)
    {
        return servicer.Registry.GetRequiredService<AccountManager>();
    }
}
