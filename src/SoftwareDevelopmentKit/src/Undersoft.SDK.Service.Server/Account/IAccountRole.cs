namespace Undersoft.SDK.Service.Server.Accounts
{
    public interface IAccountRole : IIdentifiable
    {
        ObjectSet<RoleClaim> Claims { get; set; }
    }
}