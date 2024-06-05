using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Header
{
    public partial class GenericDataGridHeaderRubric : ViewItem
    {
        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

        [CascadingParameter]
        public bool Resizable { get; set; }

        [CascadingParameter]
        public int RubricOrdinalSeed { get; set; } = 1;

        private int _ordinal => Rubric.RubricOrdinal + RubricOrdinalSeed;

        public void OnFilterDefine(MouseEventArgs args) { }

        public void OnFilterDismiss(MouseEventArgs args) { }

        public Task OnClickSort(MouseEventArgs args)
        {
            if (!Rubric.Sorted)
            {
                Rubric.SortBy = System.Linq.SortDirection.Ascending;
            }
            else
            {
                if (Rubric.SortBy == System.Linq.SortDirection.Descending)
                    Rubric.SortBy = System.Linq.SortDirection.None;
                else
                    Rubric.SortBy = System.Linq.SortDirection.Descending;
            }
            StateHasChanged();
            return ((IViewStore)Parent!).DataStore.LoadAsync();
        }


        private string? RubricHeaderClass(IViewRubric rubric) =>
            rubric.Sorted
                ? $"{RubricClass(rubric)} {(rubric.SortBy == System.Linq.SortDirection.Ascending ? "col-sort-asc" : "col-sort-desc")}"
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
                    rubric.SortBy == System.Linq.SortDirection.Ascending
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
