namespace Undersoft.SDK.Service.Server.Accounts.Identity
{
    public interface IAccountRole : IIdentifiable
    {
        Listing<RoleClaim> Claims { get; set; }
    }
}