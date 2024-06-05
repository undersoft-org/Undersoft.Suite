using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Dialog;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Header
{
    public class GenericDataGridHeaderMenuEdit : DataObject
    {
        private IViewData _item = default!;

        public GenericDataGridHeaderMenuEdit() { }

        public GenericDataGridHeaderMenuEdit(IViewData item)
        {
            _item = item;
        }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridDialogOperations), "AddItem")]
        public IViewData Add
        {
            get => _item;
            set => _item = value;
        }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridDialogOperations), "DeleteItems")]
        public IViewData Delete
        {
            get => _item;
            set => _item = value;
        }
    }
}
