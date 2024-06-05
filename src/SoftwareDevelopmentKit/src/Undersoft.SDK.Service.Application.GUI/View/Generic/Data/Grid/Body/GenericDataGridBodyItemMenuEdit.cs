using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Dialog;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body
{
    public class GenericDataGridBodyItemMenuEdit : GenericDataGridBodyItemMenuShow
    {
        protected override IViewData _item { get; set; } = default!;

        public GenericDataGridBodyItemMenuEdit() { }

        public GenericDataGridBodyItemMenuEdit(IViewData item)
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
        [Invoke(typeof(GenericDataGridDialogOperations), "EditItem")]
        public IViewData Edit
        {
            get => _item;
            set => _item = value;
        }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridDialogOperations), "DeleteItem")]
        public IViewData Delete
        {
            get => _item;
            set => _item = value;
        }


    }
}
