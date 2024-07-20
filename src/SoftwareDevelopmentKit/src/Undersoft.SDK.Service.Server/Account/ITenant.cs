
namespace Undersoft.SDK.Service.Server.Accounts
{
    public interface ITenant : IOrigin
    {
        EntitySet<Account> Accounts { get; set; }
        string TenantFullName { get; set; }
        string TenantName { get; set; }
        string TenantRoute { get; set; }
        string TenantUrl { get; set; }
    }
}