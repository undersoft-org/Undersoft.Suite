using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGridBody : ViewStore
    {
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
        public bool Multiline { get; set; } = false;

        private IViewRubrics? _menuShowRubrics;
        public IViewRubrics? MenuShowRubrics => _menuShowRubrics ??= GetMenuShowRubrics();

        private IViewRubrics? _menuEditRubrics;
        public IViewRubrics? MenuEditRubrics => _menuEditRubrics ??= GetMenuEditRubrics();

        private IViewRubrics GetMenuEditRubrics()
        {
            var data = typeof(ViewData<>)
                  .MakeGenericType(typeof(GenericDataGridBodyItemMenuEdit))
                  .New<IViewData>();
            return data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
        }

        private IViewRubrics GetMenuShowRubrics()
        {
            var data = typeof(ViewData<>)
                .MakeGenericType(typeof(GenericDataGridBodyItemMenuShow))
                .New<IViewData>();
            return data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
        }
    }
}
