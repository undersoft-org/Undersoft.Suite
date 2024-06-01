using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public interface IGenericDataGridDialog : IViewDialog
    {
        IJSRuntime JS { get; }

        Task ShowDelete(IViewData data);
        Task ShowEdit(IViewData data);
        Task ShowPreview(IViewData data);
    }
}