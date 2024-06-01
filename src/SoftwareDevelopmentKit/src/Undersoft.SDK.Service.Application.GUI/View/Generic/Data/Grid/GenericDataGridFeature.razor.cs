using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGridFeature : ViewItem
    {
        [Parameter]
        public int Ordinal { get; set; } = 1;

        [CascadingParameter]
        public bool Checked { get => StateFlags.Checked; set => StateFlags.Checked = value; }

        [CascadingParameter]
        public override IViewData Data { get; set; } = default!;
    }
}
