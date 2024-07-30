using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGridBody : ViewStore
    {
        protected override void OnInitialized()
        {
            Initialize();
            base.OnInitialized();
        }

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
        public override FeatureFlags FeatureFlags
        {
            get => base.FeatureFlags;
            set => base.FeatureFlags = value;
        }

        [CascadingParameter]
        public override EditMode EditMode
        {
            get => base.EditMode;
            set => base.EditMode = value;
        }

        [CascadingParameter]
        public override EntryMode EntryMode
        {
            get => base.EntryMode;
            set => base.EntryMode = value;
        }

        [Parameter]
        public bool Multiline { get; set; } = false;

        public IViewRubrics? MenuRubrics { get; set; }

        private void Initialize()
        {
            IViewData? data = null;
            if (FeatureFlags.Editable && EditMode != EditMode.None)
                data = typeof(ViewData<>)
                    .MakeGenericType(typeof(GenericDataGridBodyItemMenuEdit))
                    .New<IViewData>();
            else if (FeatureFlags.Showable)
                data = typeof(ViewData<>)
                    .MakeGenericType(typeof(GenericDataGridBodyItemMenuShow))
                    .New<IViewData>();
            if (data != null)
            {
                MenuRubrics = data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
            }
        }
    }
}
