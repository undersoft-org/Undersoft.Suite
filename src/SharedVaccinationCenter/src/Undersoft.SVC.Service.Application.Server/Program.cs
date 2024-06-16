// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Application.Server
// ********************************************************

using Undersoft.SDK.Logging;
using Undersoft.SDK.Service.Application.Server.Hosting;

namespace Undersoft.SVC.Service.Application.Server;

/// <summary>
/// The program.
/// </summary>
public class Program
{
    /// <summary>
    /// The server.
    /// </summary>
    static IApplicationServerHost? server;
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
        Launch();
    }
    /// <summary>
    /// Launches this instance.
    /// </summary>
    public static void Launch()
    {
        try
        {
            Log.Info<Runlog>(null, "Starting Undersoft.SVC.Service.Application.Server ....");

            StartApplicationServer();
        }
        catch (Exception exception)
        {
            Log.Error<Runlog>(null, " Undersoft.SVC.Service.Application.Server terminated unexpectedly ....", exception);
        }
        finally
        {
            Log.Info<Runlog>(null, " Undersoft.SVC.Service.Application.Server shutted down ....");
        }
    }
    /// <summary>
    /// Restarts this instance.
    /// </summary>
    public static void Restart()
    {
        Log.Info<Runlog>(null, "Restarting  Undersoft.SVC.Service.Application.Server ....");

        Shutdown();
        Launch();
    }
    /// <summary>
    /// Shuts down this instance.
    /// </summary>
    public static void Shutdown()
    {
        Log.Info<Runlog>(null, "Shutting down  Undersoft.SVC.Service.Application.Server ....");

        server?.Host.StopAsync(TimeSpan.FromSeconds(5)).Wait();
    }
    /// <summary>
    /// Starts the application server.
    /// </summary>
    private static void StartApplicationServer()
    {
        server = new ApplicationServerHost();
        server.Run<Setup>();
    }
}
