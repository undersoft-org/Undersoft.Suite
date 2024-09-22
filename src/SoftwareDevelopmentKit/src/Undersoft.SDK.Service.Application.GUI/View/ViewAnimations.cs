using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic;

namespace Undersoft.SDK.Service.Application.GUI;

public class ViewAnimations : IViewAnimations
{
    private async Task InvokeFunction(string functionName, DialogInstance instance)
    {
       await (
            (IGenericDialog)((IViewData)instance.Content).View!
        ).JSRuntime!.InvokeVoidAsync(functionName, instance.Id);
    }

    public EventCallback<DialogInstance> OpeningFromLeft()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("GenericAnimations.openingFromLeft", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> ClosingToRight()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("GenericAnimations.closingToRight", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> OpeningFromBottom()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("GenericAnimations.openingFromBottom", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> ClosingToTop()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("GenericAnimations.closingToTop", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> OpeningCentral()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("GenericAnimations.openingCentral", instance)
        );
        return a;
    }

    public EventCallback<DialogInstance> ClosingCentral()
    {
        var a = EventCallback.Factory.Create<DialogInstance>(
            this,
            async (instance) => await InvokeFunction("GenericAnimations.closingCentral", instance)
        );
        return a;
    }
}
