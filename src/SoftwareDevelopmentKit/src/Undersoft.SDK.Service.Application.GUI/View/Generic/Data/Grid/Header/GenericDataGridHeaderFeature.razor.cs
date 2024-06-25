using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Header
{
    public partial class GenericDataGridHeaderFeature : ViewItem
    {
        [Parameter]
        public int Ordinal { get; set; } = 1;

        [CascadingParameter]
        public bool Resizable { get; set; }

        private static string? RubricClass(IViewRubric rubric) => rubric.Align switch
        {
            Align.Start => $"col-justify-start {rubric.Class}",
            Align.Center => $"col-justify-center {rubric.Class}",
            Align.End => $"col-justify-end {rubric.Class}",
            _ => rubric.Class,
        };

        private void CloseColumnOptions()
        {
            StateHasChanged();
        }
    }
}
