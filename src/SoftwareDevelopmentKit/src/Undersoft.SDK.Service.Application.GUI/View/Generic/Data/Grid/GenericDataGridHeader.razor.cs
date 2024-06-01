using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGridHeader : ViewStore
    {
        [CascadingParameter]
        public int RubricOrdinalSeed { get; set; } = 1;

        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

        [CascadingParameter]
        public override IViewDataStore DataStore
        {
            get => base.DataStore;
            set => base.DataStore = value;
        }

        [Parameter]
        public bool Checked { get => Data.StateFlags.Checked; set => Data.StateFlags.Checked = value; }

        [Parameter]
        public bool Resizable { get => base.FeatureFlags.Resizable; set => base.FeatureFlags.Resizable = value; }

        [CascadingParameter]
        public override FeatureFlags FeatureFlags { get => base.FeatureFlags; set => base.FeatureFlags = value; }

        [CascadingParameter]
        public override EditMode EditMode { get => base.EditMode; set => base.EditMode = value; }

        private IViewData GetOperationData()
        {
            var data = typeof(ViewData<>).MakeGenericType(typeof(GenericDataGridOperation)).New<IViewData>(new GenericDataGridOperation(Data));
            data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
            return data;
        }

    }
}
