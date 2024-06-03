using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public abstract class GenericDataGridOperationBase : DataObject
    {
        protected IViewData _item = default!;

        public GenericDataGridOperationBase() { }

        public GenericDataGridOperationBase(IViewData item)
        {
            _item = item;
        }

        protected virtual IGenericDataGridDialog? GetDialog(IViewData item, Type dialogGenericType)
        {
            if (item.Root != null)
            {
                if (item.ValidatorType != null)
                {
                    var vs = ((IViewDataStore)item.Root).ViewStore;
                    var dialogViewType = dialogGenericType.MakeGenericType(
                         item.ModelType, item.ValidatorType
                    );
                    var dialogType = typeof(GenericDataGridDialog<,>).MakeGenericType(
                        dialogViewType, item.ModelType
                    );
                    var _dialogObj = vs.Servicer.Initialize(dialogType, vs.DialogService);
                    if (_dialogObj != null)
                    {
                        return (IGenericDataGridDialog)_dialogObj;
                    }
                }
            }
            return null;
        }

        protected virtual async Task SaveAsync(IViewData item, bool changesets = false)
        {
            var vs = ((IViewDataStore)item.Root!).ViewStore;
            await vs.DataStore.SaveAsync(changesets);
        }
    }
}
