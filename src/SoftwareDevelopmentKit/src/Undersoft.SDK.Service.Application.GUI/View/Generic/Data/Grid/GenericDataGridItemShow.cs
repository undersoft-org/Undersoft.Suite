using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public class GenericDataGridItemShow : GenericDataGridOperationBase
    {
        public GenericDataGridItemShow() : base() { }

        public GenericDataGridItemShow(IViewData item) : base(item) { }

        [MenuItem]
        [Extended]
        [Invoke(typeof(GenericDataGridItemShow), "ShowItem")]
        public IViewData Show
        {
            get => _item;
            set => _item = value;
        }

        public async Task ShowItem(IViewData item)
        {
            var _dialog = GetDialog(item, typeof(GenericDataGridDialogEdit<,>));
            if (_dialog != null)
            {
                item.Title = $"{item.ModelType.Name} details";
                item.Height = "auto";

                await _dialog.ShowPreview(item);
            }
        }
    }
}
