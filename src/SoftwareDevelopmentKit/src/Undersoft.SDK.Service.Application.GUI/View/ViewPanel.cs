using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewPanel<TPanel, TModel> : ComponentBase, IViewPanel<TModel>
    where TPanel : IDialogContentComponent<IViewData<TModel>>
    where TModel : class, IOrigin, IInnerProxy
{
    public ViewPanel() { }

    public ViewPanel(IDialogService dialogService)
    {
        Service = dialogService;
    }

    public IDialogService Service { get; private set; }

    public IViewData<TModel>? Content { get => Data != null ? (IViewData<TModel>)Data : null; set => Data = value; }

    IViewData? IViewDialog.Content => Content;

    public IViewData? Data { get; set; } = default!;

    public IDialogReference? Reference { get; set; }

    public virtual async Task Show(IViewData<TModel> data)
    {
        if (Service != null)
        {
            var dialog = await Service.ShowPanelAsync<TPanel>(
                data,
                new DialogParameters<TModel>()
                {
                    Height = data.Height,
                    Width = data.Width,
                    Title = data.Title,
                    ShowTitle = true,
                    Alignment = data.HorizontalAlignment,
                    SecondaryActionEnabled = false,
                    ShowDismiss = true,
                    PrimaryAction = "Submit"
                }
            );

            var result = await dialog.Result;
            if (!result.Cancelled && result.Data != null)
            {
                Content = (IViewData<TModel>)result.Data;
            }
        }
    }

    public virtual async Task Show(IViewData<TModel> data, Action<DialogParameters> setup)
    {
        if (Service != null)
        {
            var parameters = new DialogParameters();
            parameters.Height = data.Height;
            parameters.Width = data.Width;
            parameters.Title = data.Title;
            setup.Invoke(parameters);
            if (parameters.PrimaryAction == "Ok")
            {
                parameters.PrimaryAction = "Submit";
                parameters.SecondaryActionEnabled = false;
            }
            Reference = await Service.ShowPanelAsync<TPanel>(data, parameters);

            var result = await Reference.Result;
            if (!result.Cancelled && result.Data != null)
            {
                Content = (IViewData<TModel>)result.Data;
            }
        }
    }

    public virtual async Task Show(Action<DialogParameters<TModel>> setup)
    {
        if (Service != null)
        {
            var parameters = new DialogParameters<TModel>();
            parameters.PrimaryAction = "Submit";
            setup(parameters);
            Reference = await Service.ShowPanelAsync<TPanel>(parameters);

            var result = await Reference.Result;
            if (!result.Cancelled && result.Data != null && result.Data is IViewData)
            {
                Content = (IViewData<TModel>)result.Data;
            }
        }
    }

    public virtual async Task Show(IViewData data)
    {
        await Show((IViewData<TModel>)data);
    }

    public virtual async Task Show(IViewData data, Action<DialogParameters> setup)
    {
        await Show((IViewData<TModel>)data, setup);
    }

    public virtual async Task Show(Action<DialogParameters> setup)
    {
        if (Service != null)
        {
            var parameters = new DialogParameters<TModel>();
            parameters.PrimaryAction = "Submit";
            setup(parameters);
            Reference = await Service.ShowPanelAsync<TPanel>(parameters);

            var result = await Reference.Result;
            if (!result.Cancelled && result.Data != null && result.Data is IViewData)
            {
                Content = (IViewData<TModel>)result.Data;
            }
        }
    }

    public virtual async Task Update(
        string id,
        IViewData<TModel> data,
        Action<DialogParameters>? setup = null
    )
    {
        if (Service != null)
        {
            var parameters = new DialogParameters<IViewData<TModel>>();
            parameters.Height = data.Height;
            parameters.Width = data.Width;
            parameters.Title = data.Title;
            parameters.Id = data.Model.TypeId.ToString();
            parameters.Content = data;
            if (setup != null)
                setup(parameters);
            Reference = await Service.UpdateDialogAsync(id, parameters);
        }
    }

    public void RenderView()
    {
        if (Content?.View != null)
            Content.View.RenderView();
    }
}
