using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Header
{
    public partial class GenericDataGridHeaderRubric : ViewItem, IViewLoadable
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

        public void OnFilterDismiss(MouseEventArgs args)
        {
            ((IViewFilter)Rubric.ViewFilter).IsOpen = false;
            Rubric.Filters.Clear();
            StateHasChanged();
        }

        public async Task OnClickSort(MouseEventArgs args)
        {
            if (!Rubric.Sorted)
            {
                Rubric.SortBy = SDK.SortDirection.Ascending;
            }
            else
            {
                if (Rubric.SortBy == SDK.SortDirection.Descending)
                    Rubric.SortBy = SDK.SortDirection.None;
                else
                    Rubric.SortBy = SDK.SortDirection.Descending;
            }
            StateHasChanged();
            await LoadViewAsync();
        }

        public async Task LoadViewAsync()
        {
            await ((IViewStore)Parent!).LoadViewAsync();
        }


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
