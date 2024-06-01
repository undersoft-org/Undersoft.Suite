using Undersoft.SDK.Logging;
using Undersoft.SDK.Service.Application.Server.Hosting;

namespace Undersoft.SSC.Service.Application.Server;

public class Program
{
    static string[] _args = new string[0];
    static IApplicationServerHost? server;

    public static void Main(string[] args)
    {
        _args = args;

        Launch();
    }

    public static void Launch()
    {
        try
        {
            Log.Info<Runlog>(null, "Starting Undersoft.SSC.Service.Application.Server ....");

            StartApplicationServer();
        }
        catch (Exception exception)
        {
            Log.Error<Runlog>(null, " Undersoft.SSC.Service.Application.Server terminated unexpectedly ....", exception);
        }
        finally
        {
            Log.Info<Runlog>(null, " Undersoft.SSC.Service.Application.Server shutted down ....");
        }
    }

    public static void Restart()
    {
        Log.Info<Runlog>(null, "Restarting  Undersoft.SSC.Service.Application.Server ....");

        Shutdown();
        Launch();
    }

    public static void Shutdown()
    {
        Log.Info<Runlog>(null, "Shutting down  Undersoft.SSC.Service.Application.Server ....");

        server?.Host.StopAsync(TimeSpan.FromSeconds(5)).Wait();
    }

    private static void StartApplicationServer()
    {
        server = new ApplicationServerHost();
        server.Run<Setup>();
    }
}
