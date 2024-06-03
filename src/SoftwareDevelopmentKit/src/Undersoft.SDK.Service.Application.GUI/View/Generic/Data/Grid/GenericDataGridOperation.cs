using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public class GenericDataGridOperation : GenericDataGridOperationBase
    {
        private IViewData _item = default!;

        public GenericDataGridOperation() { }

        public GenericDataGridOperation(IViewData item)
        {
            _item = item;
        }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridOperation), "AddItem")]
        public IViewData Add
        {
            get => _item;
            set => _item = value;
        }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridOperation), "DeleteItems")]
        public IViewData Delete
        {
            get => _item;
            set => _item = value;
        }

        public async Task AddItem(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogEdit<,>));
            if (_dialog != null)
            {
                var modelType = item.ModelType;
                var newData = typeof(ViewData<>)
                    .MakeGenericType(modelType)
                    .New<IViewData>(modelType.New());
                newData.Title = $"Add {modelType.Name.ToLower()}";
                newData.Height = "auto";
                newData.EntryMode = item.EntryMode;

                await _dialog.ShowEdit(newData);

                if (_dialog.Content != null)
                {
                    var newItem = ((IViewDataStore)item).Attach(_dialog.Content);
                    newItem.StateFlags.Added = true;
                    await SaveAsync(item, true);
                }
            }
        }

        public async Task DeleteItems(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogDelete<,>));
            if (_dialog != null)
            {
                item.Title = $"Delete {item.ModelType.Name.ToLower()} selection";
                item.Height = "auto";
                item.Width = "auto";

                await _dialog.ShowDelete(item);

                if (_dialog.Content != null)
                {
                    _dialog.Content
                        .Where(item => item.StateFlags.Checked)
                        .ForEach(item => item.StateFlags.Deleted = true)
                        .Commit();
                    await SaveAsync(item, true);
                }
            }
        }
    }
}
