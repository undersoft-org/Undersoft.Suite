using System.Reflection;

namespace Undersoft.SDK.Service.Server.Hosting;

public class PowerService
{
    Type entryType;

    public PowerService()
    {
        entryType = Assembly.GetEntryAssembly().EntryPoint.DeclaringType;
    }

    public void Shutdown()
    {
        if (entryType != null)
        {
            var mi = entryType.GetMethod("Shutdown");
            if (mi != null)
            {
                mi.Invoke(null, null);
                return;
            }
        }
        this.Error<Runlog>("Unable to invoke shutdown method");
    }

    public void Restart()
    {
        if (entryType != null)
        {
            var mi = entryType.GetMethod("Restart");
            if (mi != null)
            {
                mi.Invoke(null, null);
                return;
            }
        }
        this.Error<Runlog>("Unable to invoke reset method");
    }
}
