using Microsoft.FluentUI.AspNetCore.Components;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewAnimations
    {
        EventCallback<DialogInstance> ClosingCentral();
        EventCallback<DialogInstance> ClosingToRight();
        EventCallback<DialogInstance> ClosingToTop();
        EventCallback<DialogInstance> OpeningCentral();
        EventCallback<DialogInstance> OpeningFromBottom();
        EventCallback<DialogInstance> OpeningFromLeft();
    }
}