using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body
{
    public partial class GenericDataGridBodyItemFeature : ViewItem
    {
        [Parameter]
        public int Ordinal { get; set; } = 1;

        public bool Checked { get => Data.StateFlags.Checked; set => Data.StateFlags.Checked = value; }

        [CascadingParameter]
        public override IViewData Data { get; set; } = default!;

        public override string ViewId => Data.ViewId + "feature" + Ordinal.ToString();
    }
}
