using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Model;

public class ViewDialog<TDialog, TModel> : IViewDialog<TModel>
    where TDialog : IDialogContentComponent<IViewData<TModel>>
    where TModel : class, IOrigin, IInnerProxy
{
    public ViewDialog(IDialogService dialogService, IViewDialogAnimations animations)
    {
        Service = dialogService;
        Animations = animations;
    }

    public IViewDialogAnimations Animations { get; set; }

    public IJSRuntime? JSRuntime { get; private set; }

    public IDialogService Service { get; private set; }

    public IViewData<TModel>? Content
    {
        get => Data != null ? (IViewData<TModel>)Data : null;
        set => Data = value;
    }

    public IViewData? Data { get; set; } = default!;

    public IDialogReference? Reference { get; set; }

    IViewData? IViewDialog.Content => Content;

    public async Task ProcessDialog()
    {
        var result = await Reference!.Result;
        if (!result.Cancelled && result.Data != null)
        {
            Content = (IViewData<TModel>)result.Data;
        }
    }

    public virtual async Task Show(IViewData<TModel> data)
    {
        if (Service != null)
        {
            Reference = await Service.ShowDialogAsync<TDialog>(
                data,
                new DialogParameters()
                {
                    Height = data.Height,
                    Width = data.Width,
                    Title = data.Title,
                    Id = data.Model.TypeId.ToString(),
                    PrimaryAction = "Submit",
                }
            );
            await ProcessDialog();
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
            parameters.Id = data.Model.TypeId.ToString();
            parameters.PrimaryAction = "Submit";
            setup.Invoke(parameters);
            Reference = await Service.ShowDialogAsync<TDialog>(data, parameters);
        }
        await ProcessDialog();
    }

    public virtual async Task Show(Action<DialogParameters<TModel>> setup)
    {
        if (Service != null)
        {
            var parameters = new DialogParameters<TModel>();
            parameters.PrimaryAction = "Submit";
            setup(parameters);
            Reference = await Service.ShowDialogAsync<TDialog>(parameters);
        }
        await ProcessDialog();
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
            Reference = await Service.ShowDialogAsync<TDialog>(parameters);
            await ProcessDialog();
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
            parameters.PrimaryAction = "Submit";
            parameters.Content = data;
            if (setup != null)
                setup(parameters);
            Reference = await Service.UpdateDialogAsync(id, parameters);
            await ProcessDialog();
        }
    }

    public void RenderView()
    {
        if (Content?.View != null)
            Content.View.RenderView();
    }

    void IView.RenderView()
    {
        throw new NotImplementedException();
    }
}
