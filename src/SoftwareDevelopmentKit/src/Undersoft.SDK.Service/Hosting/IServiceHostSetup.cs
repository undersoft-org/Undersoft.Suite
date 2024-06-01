namespace Undersoft.SDK.Service.Hosting
{
    public interface IServiceHostSetup
    {
        IServiceHostSetup UseServiceClients();

        IServiceHostSetup UseInternalProvider();

        IServiceHostSetup UseDataMigrations();

        IServiceHostSetup RebuildProviders();

        IServiceManager Manager { get; }
    }
}