using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.GDC.Service.Server;

public class Program
{
    static string[] _args = new string[0];

    static IServerHost? server;

    public static void Main(string[] args)
    {
        _args = args;

        Launch();
    }

    public static void Launch()
    {
        try
        {
            Log.Info<Runlog>(null, "Starting Undersoft.GDC.Service.Server ....");

            StartServer();
        }
        catch (Exception exception)
        {
            Log.Error<Runlog>(null, "Undersoft.GDC.Service.Server terminated unexpectedly ....", exception);
        }
        finally
        {
            Log.Info<Runlog>(null, "Undersoft.GDC.Service.Server shutted down ....");
        }
    }

    public static void Restart()
    {
        Log.Info<Runlog>(null, "Restarting Undersoft.GDC.Service.Server ....");

        Shutdown();
        Launch();
    }

    public static void Shutdown()
    {
        Log.Info<Runlog>(null, "Shutting down Undersoft.GDC.Service.Server ....");

        server?.Host.StopAsync(TimeSpan.FromSeconds(5)).Wait();
    }

    private static void StartServer()
    {
        server = new ServerHost();
        server.Run<Setup>();
    }
}
