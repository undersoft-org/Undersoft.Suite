using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI;

public class ViewDialogAnimations : IViewDialogAnimations
{
    private const string JAVASCRIPT_FILE =
        "./_content/Undersoft.SDK.Service.Application.GUI/js/ViewDialogAnimations.js";

    private IJSRuntime? _js = null;
    private IJSObjectReference? _script = null;

    public ViewDialogAnimations()
    {
    }

    public EventCallback<DialogInstance> OpeningFromLeft()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) =>
            {
                _js ??= ((IViewItem)((IViewData)instance.Content).View!).JSRuntime;
                _script ??= await _js.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE);
                await _script.InvokeVoidAsync("dialogOpeningFromLeft", instance.Id);
            }
        );
        return a;
    }

    public EventCallback<DialogInstance> ClosingToRight()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) =>
            {
                _js ??= ((IViewItem)((IViewData)instance.Content).View!).JSRuntime;
                _script ??= await _js.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE);
                await _script.InvokeVoidAsync("dialogClosingToRight", instance.Id);
            }
        );
        return a;
    }

    public EventCallback<DialogInstance> OpeningFromBottom()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) =>
            {
                _js ??= ((IViewItem)((IViewData)instance.Content).View!).JSRuntime;
                _script ??= await _js.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE);
                await _script.InvokeVoidAsync("dialogOpeningFromBottom", instance.Id);
            }
        );
        return a;
    }

    public EventCallback<DialogInstance> ClosingToTop()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) =>
            {
                _js ??= ((IViewItem)((IViewData)instance.Content).View!).JSRuntime;
                _script ??= await _js.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE);
                await _script.InvokeVoidAsync("dialogClosingToTop", instance.Id);
            }
        );
        return a;
    }

    public EventCallback<DialogInstance> Opening()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) =>
            {
                _js ??= ((IViewItem)((IViewData)instance.Content).View!).JSRuntime;
                _script ??= await _js.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE);
                await _script.InvokeVoidAsync("dialogOpening", instance.Id);
            }
        );
        return a;
    }

    public EventCallback<DialogInstance> Closing()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) =>
            {
                _js ??= ((IViewItem)((IViewData)instance.Content).View!).JSRuntime;
                _script ??= await _js.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE);
                await _script.InvokeVoidAsync("dialogClosing", instance.Id);
            }
        );
        return a;
    }
}
