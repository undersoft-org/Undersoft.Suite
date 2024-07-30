using Undersoft.GCC.Service.Clients;
using Undersoft.GCC.Service.Extensions;
using Undersoft.SDK.Logging;
using Undersoft.SDK.Service.Hosting;
using Undersoft.SDK.Service.Scheduler;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.GCC.Service.API;

public class Program
{
    static string[] _args = new string[0];

    static IServerHost? server;
    static IServiceHost? scheduler;

    public static void Main(string[] args)
    {
        _args = args;

        Launch();
    }

    public static void Launch()
    {
        try
        {
            Log.Info<Runlog>(null, "Starting Undersoft.GCC.Service.API ....");

            _ = StartScheduler();

            scheduler.Info<Runlog>("Started Undersoft.SDK.Service.Scheduler.SchedulerServiceHost ....");

            StartServer();

            server.Info<Runlog>("Started Undersoft.SDK.Service.Server.Hosting.ServerHost ....");
        }
        catch (Exception exception)
        {
            server.Error<Runlog>("Undersoft.GCC.Service.API terminated unexpectedly ....", exception);
        }
        finally
        {
            server.Info<Runlog>("Undersoft.GCC.Service.API shutted down ....");
        }
    }

    public static void Restart()
    {
        Log.Info<Runlog>(null, "Restarting Undersoft.SSC.Service.API ....");

        Shutdown();

        Launch();
    }

    public static void Shutdown()
    {
        Log.Info<Runlog>(null, "Shutting down Undersoft.GCC.Service.API ....");

        scheduler?.Host.StopAsync(TimeSpan.FromSeconds(5)).Wait();

        scheduler.Info<Runlog>("Shutted down Undersoft.SDK.Service.Scheduler.SchedulerServiceHost ....");

        server?.Host.StopAsync(TimeSpan.FromSeconds(5)).Wait();

        server.Info<Runlog>("Shutted down Undersoft.SDK.Service.Server.Hosting.ServerHost ....");
    }

    private static async Task StartScheduler()
    {
        await Task.Delay(10000);

        scheduler = new SchedulerServiceHost();

        scheduler.Configure().AddWorkflowSchedule();

        await scheduler.RunAsync<SchedulerHostedService>(new[] { typeof(GCCServiceClient) });
    }

    private static void StartServer()
    {
        server = new ServerHost();
        server.Run<Setup>();
    }
}
