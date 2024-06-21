using Microsoft.FluentUI.AspNetCore.Components;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewDialogAnimations
    {
        EventCallback<DialogInstance> Closing();
        EventCallback<DialogInstance> ClosingToRight();
        EventCallback<DialogInstance> ClosingToTop();
        EventCallback<DialogInstance> Opening();
        EventCallback<DialogInstance> OpeningFromBottom();
        EventCallback<DialogInstance> OpeningFromLeft();
    }
}