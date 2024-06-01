using Undersoft.SDK.Invoking;
using Undersoft.SDK.Series.Base;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewInvokers : ListingBase<Invoker>
{
    public static async Task WaitAsync(int milliseconds, Invoker invoker)
    {
        var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(milliseconds));
        while (await timer.WaitForNextTickAsync())
        {
            timer.Dispose();
            invoker.Invoke();
        };
    }

    public static async Task WaitAsync(int milliseconds, Action invoker)
    {
        var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(milliseconds));
        while (await timer.WaitForNextTickAsync())
        {
            timer.Dispose();
            invoker.Invoke();
        };
    }
}
