using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Header;
using Undersoft.SDK.Utilities;

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

        [CascadingParameter]
        public override EntryMode EntryMode
        {
            get => base.EntryMode;
            set => base.EntryMode = value;
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
            Data.EntryMode = EntryMode;
            var data = typeof(ViewData<>).MakeGenericType(typeof(GenericDataGridHeaderMenuEdit)).New<IViewData>(new GenericDataGridHeaderMenuEdit(Data));
            data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
            return data;
        }

        private void OnCheckStateChanged(bool? state)
        {
            if (state != null)
            {
                DataStore.Items.ForEach(item => item.StateFlags.Checked = (bool)state).Commit();
                DataStore.ViewItem?.RenderView();
            }
        }

    }
}
