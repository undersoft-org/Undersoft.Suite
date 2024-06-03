using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

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
    }
}
