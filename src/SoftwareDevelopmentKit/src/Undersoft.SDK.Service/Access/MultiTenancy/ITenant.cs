namespace Undersoft.SDK.Service.Access.MultiTenancy
{
    public interface ITenant : IOrigin
    {
        string TenantName { get; set; }
        string TenantPath { get; set; }
        string TenantUrl { get; set; }
    }
}