using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGridRubric : ViewItem
    {
        [CascadingParameter]
        public bool Resizable { get; set; }

        [CascadingParameter]
        public int RubricOrdinalSeed { get; set; } = 1;

        private int _ordinal => Rubric.RubricOrdinal + RubricOrdinalSeed;

        private string? RubricHeaderClass(IViewRubric rubric)
       => rubric.Sorted
       ? $"{RubricClass(rubric)} {(rubric.SortBy == System.Linq.SortDirection.Ascending ? "col-sort-asc" : "col-sort-desc")}"
       : RubricClass(rubric);

        private static string? RubricClass(IViewRubric rubric) => rubric.Align switch
        {
            Align.Start => $"col-justify-start {rubric.Class}",
            Align.Center => $"col-justify-center {rubric.Class}",
            Align.End => $"col-justify-end {rubric.Class}",
            _ => rubric.Class,
        };

        private string AriaSortValue(IViewRubric rubric)
       => rubric.Sorted
           ? (rubric.SortBy == System.Linq.SortDirection.Ascending ? "ascending" : "descending")
           : "none";

        private void CloseColumnOptions()
        {
            StateHasChanged();
        }
    }
}
