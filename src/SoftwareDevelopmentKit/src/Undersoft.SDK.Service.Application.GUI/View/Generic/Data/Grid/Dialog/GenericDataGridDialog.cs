using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Dialog;
using Undersoft.SDK.Service.Application.GUI.View.Model;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid;

public class GenericDataGridDialog<TDialog, TModel> : ViewDialog<TDialog, TModel>, IGenericDataGridDialog where TDialog : IDialogContentComponent<IViewData<TModel>> where TModel : class, IOrigin, IInnerProxy
{
    public GenericDataGridDialog(IDialogService dialogService, IViewDialogAnimations animations) : base(dialogService, animations)
    {
    }

    public virtual async Task ShowPreview(IViewData data)
    {
        if (Service != null)
        {
            data.Rubrics.ForEach(r => r.Disabled = true).Commit();
            data.ExtendedRubrics.ForEach(r => r.Disabled = true).Commit();

            Reference = await Service.ShowDialogAsync<TDialog>(data, new DialogParameters<IViewData<TModel>>()
            {
                Height = data.Height,
                Width = data.Width,
                Title = data.Title,
                PrimaryActionEnabled = false,
                SecondaryAction = " Close ",
                SecondaryActionEnabled = true,
                Content = (IViewData<TModel>)data,
                PreventDismissOnOverlayClick = false,
                ShowDismiss = false,
                Modal = true,
                PreventScroll = true,
                OnDialogClosing = Animations.Closing()
            });

            await ProcessDialog();
        }
    }

    public virtual async Task ShowEdit(IViewData data)
    {
        if (Service != null)
        {
            Reference = await Service.ShowDialogAsync<TDialog>(data, new DialogParameters<IViewData<TModel>>()
            {
                Height = data.Height,
                Width = data.Width,
                Title = data.Title,
                PrimaryAction = " Save ",
                SecondaryAction = "Cancel",
                SecondaryActionEnabled = true,
                Content = (IViewData<TModel>)data,
                PreventDismissOnOverlayClick = false,
                ShowDismiss = false,
                Modal = true,
                PreventScroll = true,
                OnDialogClosing = Animations.Closing()
            });

            await ProcessDialog();
        }
    }

    public virtual async Task ShowDelete(IViewData data)
    {
        if (Service != null)
        {
            Reference = await Service.ShowDialogAsync<TDialog>(data, new DialogParameters<IViewData<TModel>>()
            {
                Height = data.Height,
                Width = data.Width,
                Title = data.Title,
                PrimaryAction = "Confirm",
                SecondaryAction = "Cancel",
                SecondaryActionEnabled = true,
                Content = (IViewData<TModel>)data,
                PreventDismissOnOverlayClick = false,
                ShowDismiss = false,
                Modal = true,
                PreventScroll = true,
                OnDialogClosing = Animations.Closing()
            });

            await ProcessDialog();
        }
    }
}
