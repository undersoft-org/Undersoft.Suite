using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace Undersoft.SDK.Service.Application.Extensions;

[ExcludeFromCodeCoverage]
public static class JSRuntimeExtensions
{
    public static async ValueTask InvokeVoidAsync(this IJSRuntime jsRuntime, object? el = null, string? func = null, params object[] args)
    {
        var paras = new List<object>();
        if (el != null)
        {
            paras.Add(el);
        }

        if (args != null)
        {
            paras.AddRange(args);
        }

        try
        {
            await jsRuntime.InvokeVoidAsync($"$.{func}", paras.ToArray()).ConfigureAwait(false);
        }
#if NET6_0_OR_GREATER
        catch (JSDisconnectedException) { }
#endif
        catch (JSException) { }
        catch (AggregateException) { }
        catch (InvalidOperationException) { }
        catch (TaskCanceledException) { }
    }

    public static async ValueTask InitializeInactivityTimer<T>(
           this IJSRuntime js,
           DotNetObjectReference<T> dotNetObjectReference
       ) where T : class
    {
        await js.InvokeVoidAsync("initializeInactivityTimer", dotNetObjectReference);
    }

    public static async ValueTask<bool> Confirm(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("console.log", "example message");
        return await js.InvokeAsync<bool>("confirm", message);
    }

    public static async ValueTask MyFunction(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("my_function", message);
    }

    public static ValueTask<object> SetInLocalStorage(
        this IJSRuntime js,
        string key,
        string content
    ) => js.InvokeAsync<object>("localStorage.setItem", key, content);

    public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key) =>
        js.InvokeAsync<string>("localStorage.getItem", key);

    public static ValueTask<object> RemoveItem(this IJSRuntime js, string key) =>
        js.InvokeAsync<object>("localStorage.removeItem", key);
}
