namespace Undersoft.SDK.Service.Server.Accounts.Identity
{
    public interface IAccountRole : IIdentifiable
    {
        ObjectSet<RoleClaim> Claims { get; set; }
    }
}