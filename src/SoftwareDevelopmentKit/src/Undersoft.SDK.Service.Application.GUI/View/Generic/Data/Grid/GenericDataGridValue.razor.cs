using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGridValue : ViewItem
    {
        [CascadingParameter]
        public int RubricOrdinalSeed { get; set; } = 1;

        private int _ordinal => Rubric.RubricOrdinal + RubricOrdinalSeed;

        [CascadingParameter]
        public override IViewData Data { get; set; } = default!;
    }
}
