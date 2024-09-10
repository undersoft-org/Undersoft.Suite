using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Updating;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Dialog
{
    public class GenericDataGridDialogOperations
    {
        protected IViewData _item = default!;

        public GenericDataGridDialogOperations() { }

        public GenericDataGridDialogOperations(IViewData item)
        {
            _item = item;
        }

        public async Task ShowItem(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogEdit<,>));
            if (_dialog != null)
            {
                item.Title = $"{item.ModelType.Name} details";

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
                    .New<IViewData>(modelType.New(), item);
                data.Title = $"Add {modelType.Name.ToLower()}";
                data.Height = item.Height;
                data.Width = item.Width;
                data.EntryMode = item.EntryMode;

                await _dialog.ShowEdit(data);

                if (_dialog.Content != null)
                {
                    var store = item;
                    if (item.Parent != null)
                        store = item.Parent;

                    var newItem = ((IViewDataStore)store).Attach(_dialog.Content);
                    newItem.StateFlags.Added = true;
                    await SaveAsync(item, true);
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
                    .New<IViewData>(tempModel, item);
                item.Model.PutTo(tempData.Model);
                tempData.Model.Id = item.Model.Id;               
                tempData.Title = $"Edit {modelType.Name.ToLower()}";
                tempData.Height = item.Height;
                tempData.Width = item.Width;
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
                    var _dialogObj = vs!.Servicer!.Initialize(dialogType, vs.DialogService);
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

        protected virtual async Task StageAsync(IViewData item, bool changesets = false)
        {
            var vs = ((IViewDataStore)item.Root!).ViewStore;
            await vs.DataStore.StageAsync(changesets);
        }
    }
}
