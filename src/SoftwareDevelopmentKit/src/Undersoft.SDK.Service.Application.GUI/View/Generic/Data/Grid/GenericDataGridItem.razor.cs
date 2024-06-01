using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGridItem : ViewItem
    {
        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

        [CascadingParameter]
        public override FeatureFlags FeatureFlags { get => base.FeatureFlags; set => base.FeatureFlags = value; }

        [CascadingParameter]
        public override EditMode EditMode
        {
            get => base.EditMode;
            set => base.EditMode = value;
        }

        [CascadingParameter]
        public bool Checked { get => StateFlags.Checked; set => StateFlags.Checked = value; }

        [CascadingParameter]
        public string GridTemplateColumns { get; set; } = default!;

        [CascadingParameter]
        public int RubricOrdinalSeed { get; set; } = 1;

        private IViewData GetItemOperationData()
        {
            var data = typeof(ViewData<>).MakeGenericType(typeof(GenericDataGridItemOperation)).New<IViewData>(new GenericDataGridItemOperation(Data));
            data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
            return data;
        }
    }
}
