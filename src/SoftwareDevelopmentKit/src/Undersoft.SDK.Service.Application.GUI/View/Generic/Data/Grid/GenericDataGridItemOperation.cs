using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public class GenericDataGridItemOperation : GenericDataGridOperationBase
    {
        public GenericDataGridItemOperation() : base() { }

        public GenericDataGridItemOperation(IViewData item) : base(item) { }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridItemOperation), "OpenItem")]
        public IViewData Open
        {
            get => _item;
            set => _item = value;
        }

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

        public async Task OpenItem(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogEdit<,>));
            if (_dialog != null)
            {
                item.Title = $"{item.ModelType.Name} details";
                item.Height = "650px";

                await _dialog.ShowPreview(item);
            }
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
                data.Height = "650px";

                await _dialog.ShowEdit(data);

                if (_dialog.Content != null)
                {
                    if (item.Parent != null)
                    {
                        item.Parent.Put(_dialog.Content);
                        _dialog.Content.StateFlags.Added = true;
                        await SaveAsync(item);
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
                item.Model.PatchTo(tempModel);
                var tempData = typeof(ViewData<>)
                    .MakeGenericType(modelType)
                    .New<IViewData>(tempModel);
                tempData.Title = $"Edit {modelType.Name.ToLower()}";
                tempData.Height = "650px";

                await _dialog.ShowEdit(tempData);

                if (_dialog.Content != null)
                {
                    item.Model = _dialog.Content.Model;
                    item.StateFlags.Changed = true;
                    await SaveAsync(item);
                }
            }
        }

        public async Task DeleteItem(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogDelete<,>));
            if (_dialog != null)
            {
                item.Title = $"Delete {item.ModelType.Name.ToLower()}";
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
