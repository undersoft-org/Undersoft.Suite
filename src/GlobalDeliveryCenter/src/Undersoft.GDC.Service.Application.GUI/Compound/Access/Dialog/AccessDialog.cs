using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Model;

namespace Undersoft.GDC.Service.Application.GUI.Compound.Access.Dialog;

public class AccessDialog<TDialog, TModel> : ViewDialog<TDialog, TModel> where TDialog : IDialogContentComponent<IViewData<TModel>> where TModel : class, IOrigin, IInnerProxy
{
    public AccessDialog(IDialogService dialogService, IJSRuntime jS, IViewDialogAnimations animations) : base(dialogService, animations)
    {
        JS = jS;
    }

    public IJSRuntime JS { get; private set; }

    public override async Task Show(IViewData<TModel> data)
    {
        if (Service != null)
        {
            data.Logo = new ViewGraphic.Logo.ColorHorizontal();
            Reference = await Service.ShowDialogAsync<TDialog>(data, new DialogParameters<IViewData<TModel>>()
            {
                Height = data.Height,
                Width = data.Width,
                Title = data.Title,
                PrimaryAction = "Submit",
                Content = data,
                PreventDismissOnOverlayClick = true,
                ShowDismiss = false,
                Modal = false,
                PreventScroll = true
            });

            var result = await Reference.Result;
            if (!result.Cancelled && result.Data != null)
            {
                Content = (IViewData<TModel>)result.Data;
            }
        }
    }
}
