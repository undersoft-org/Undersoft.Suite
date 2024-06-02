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
        public IViewData Add { get => _item; set => _item = value; }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridOperation), "DeleteItems")]
        public IViewData Delete { get => _item; set => _item = value; }


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
                newData.Height = "650px";

                await _dialog.ShowEdit(newData);

                if (_dialog.Content != null)
                {
                    item.Put(_dialog.Content);
                    _dialog.Content.StateFlags.Added = true;
                    await SaveAsync(item);
                }
            }
        }
        public async Task DeleteItems(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogDelete<,>));
            if (_dialog != null)
            {
                item.Title = $"Delete {item.ModelType.Name.ToLower()} selection";
                item.Height = "250px";

                await _dialog.ShowDelete(item);

                if (_dialog.Content != null)
                {
                    if (item.Parent != null)
                    {
                        if (item.Parent.TryRemove(_dialog.Content))
                        {
                            _dialog.Content.StateFlags.Deleted = true;
                            await SaveAsync(item);
                        }
                    }
                }
            }
        }
    }
}
