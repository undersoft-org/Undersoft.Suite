using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Dialog
{
    public interface IGenericDataGridDialog : IViewDialog
    {
        IJSRuntime? JSRuntime { get; }

        Task ShowDelete(IViewData data);
        Task ShowEdit(IViewData data);
        Task ShowPreview(IViewData data);
    }
}