using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public class GenericDataGridItemOperation : GenericDataGridItemShow
    {
        public GenericDataGridItemOperation() : base() { }

        public GenericDataGridItemOperation(IViewData item) : base(item) { }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridItemOperation), "AddItem")]
        public IViewData Add
        {
            get => _item;
            set => _item = value;
        }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridItemOperation), "EditItem")]
        public IViewData Edit
        {
            get => _item;
            set => _item = value;
        }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridItemOperation), "DeleteItem")]
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
                var data = typeof(ViewData<>)
                    .MakeGenericType(modelType)
                    .New<IViewData>(modelType.New());
                data.Title = $"Add {modelType.Name.ToLower()}";
                data.Height = "auto";
                data.EntryMode = item.EntryMode;

                await _dialog.ShowEdit(data);

                if (_dialog.Content != null)
                {
                    if (item.Parent != null)
                    {
                        var newItem = ((IViewDataStore)item.Parent).Attach(_dialog.Content);
                        newItem.StateFlags.Added = true;
                        await SaveAsync(item, true);
                    }
                }
            }
        }

        public async Task EditItem(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogEdit<,>));
            if (_dialog != null)
            {
                var modelType = item.ModelType;
                var tempModel = modelType.New();
                var tempData = typeof(ViewData<>)
                    .MakeGenericType(modelType)
                    .New<IViewData>(tempModel);
                item.Model.PutTo(tempData.Model);
                tempData.Title = $"Edit {modelType.Name.ToLower()}";
                tempData.Height = "auto";
                tempData.EntryMode = item.EntryMode;

                await _dialog.ShowEdit(tempData);

                if (_dialog.Content != null)
                {
                    _dialog.Content.Model.PatchTo(item.Model);
                    item.StateFlags.Changed = true;
                    await SaveAsync(item, true);
                }
            }
        }

        public async Task DeleteItem(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogDelete<,>));
            if (_dialog != null)
            {
                item.Title = $"Delete {item.ModelType.Name.ToLower()}";
                item.Height = "auto";
                item.Width = "auto";

                await _dialog.ShowDelete(item);

                if (_dialog.Content != null)
                {
                    _dialog.Content.StateFlags.Deleted = true;
                    await SaveAsync(item, true);
                }
            }
        }
    }
}
