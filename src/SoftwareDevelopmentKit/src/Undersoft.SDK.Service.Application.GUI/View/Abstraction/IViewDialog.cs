using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewDialog<TModel> : IViewDialog where TModel : class, IOrigin, IInnerProxy
    {
        new IViewData<TModel>? Content { get; }

        Task Show(IViewData<TModel> data);

        Task Show(IViewData<TModel> data, Action<DialogParameters> setup);

        Task Show(Action<DialogParameters<TModel>> setup);
    }

    public interface IViewDialog : IView
    {
        IViewData? Data { get; }
        IViewData? Content { get; }
        IDialogReference? Reference { get; }
        IDialogService Service { get; }

        Task Show(IViewData data);

        Task Show(IViewData data, Action<DialogParameters> setup);

        Task Show(Action<DialogParameters> setup);
    }
}