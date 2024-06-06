using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Header
{
    public partial class GenericDataGridHeaderRubricFilter : ViewItem
    {
        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

        public void OnFilterDefine(MouseEventArgs args) { }

        public void OnFilterDismiss(MouseEventArgs args) { }

        private string? RubricHeaderClass(IViewRubric rubric) =>
            rubric.Sorted
                ? $"{RubricClass(rubric)} {(rubric.SortBy == SDK.SortDirection.Ascending ? "col-sort-asc" : "col-sort-desc")}"
                : RubricClass(rubric);

        private static string? RubricClass(IViewRubric rubric) =>
            rubric.Align switch
            {
                Align.Start => $"col-justify-start {rubric.Class}",
                Align.Center => $"col-justify-center {rubric.Class}",
                Align.End => $"col-justify-end {rubric.Class}",
                _ => rubric.Class,
            };

        private string AriaSortValue(IViewRubric rubric) =>
            rubric.Sorted
                ? (
                    rubric.SortBy == SDK.SortDirection.Ascending
                        ? "ascending"
                        : "descending"
                )
                : "none";

        private void CloseColumnOptions()
        {
            StateHasChanged();
        }

    }
}
