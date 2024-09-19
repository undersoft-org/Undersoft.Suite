using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic;

namespace Undersoft.SDK.Service.Application.GUI;

public class ViewDialogAnimations : IViewDialogAnimations
{
    private const string JAVASCRIPT_FILE =
        "./_content/Undersoft.SDK.Service.Application.GUI/js/ViewDialogAnimations.js";

    private async Task InvokeFunction(string functionName, DialogInstance instance)
    {
       await (await (
            (IGenericDialog)((IViewData)instance.Content).View!
        ).JSRuntime!.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE)
        ).InvokeVoidAsync(functionName, instance.Id);
    }

    public EventCallback<DialogInstance> OpeningFromLeft()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("dialogOpeningFromLeft", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> ClosingToRight()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("dialogClosingToRight", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> OpeningFromBottom()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("dialogOpeningFromBottom", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> ClosingToTop()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("dialogClosingToTop", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> Opening()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("dialogOpening", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> Closing()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("dialogClosing", instance)
        );
        return a;
    }
}
