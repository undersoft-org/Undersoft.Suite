using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Dialog;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body
{
    public class GenericDataGridBodyItemMenuShow : DataObject
    {
        protected virtual IViewData _item { get; set; } = default!;

        public GenericDataGridBodyItemMenuShow() { }

        public GenericDataGridBodyItemMenuShow(IViewData item)
        {
            _item = item;
        }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridDialogOperations), "ShowItem")]
        public IViewData Show
        {
            get => _item;
            set => _item = value;
        }
    }
}
