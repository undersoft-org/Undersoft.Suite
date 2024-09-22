using Microsoft.FluentUI.AspNetCore.Components;
// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Model;
using Undersoft.SVC.Service.Application.GUI.Compound.Graphic;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Access.Dialog;

/// <summary>
/// The access dialog.
/// </summary>
/// <typeparam name="TDialog"/>
/// <typeparam name="TModel"/>
public class AccessDialog<TDialog, TModel> : ViewDialog<TDialog, TModel> where TDialog : IDialogContentComponent<IViewData<TModel>> where TModel : class, IOrigin, IInnerProxy
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccessDialog"/> class.
    /// </summary>
    /// <param name="dialogService">The dialog service.</param>
    /// <param name="jS">The j S.</param>
    public AccessDialog(IDialogService dialogService, IViewAnimations animations) : base(dialogService, animations)
    {
    }

    public override async Task Show(IViewData<TModel> data)
    {
        if (Service != null)
        {
            data.Logo = new LogoSVC();
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
                PreventScroll = true,
                OnDialogClosing = Animations.ClosingCentral(),

            });
            await ProcessDialog();
        }
    }


}
