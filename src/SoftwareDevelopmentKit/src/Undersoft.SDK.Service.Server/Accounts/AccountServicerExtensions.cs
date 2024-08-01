namespace Undersoft.SDK.Service.Server.Accounts;

public static class AccountServicerExtensions
{
    public static AccountManager GetAccountManager(this IServicer servicer)
    {
        return servicer.Registry.GetRequiredService<AccountManager>();
    }
}
