using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body
{
    public partial class GenericDataGridBodyItem : ViewItem
    {
        public override string ViewId => Data.ViewId;

        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

        [CascadingParameter]
        public override FeatureFlags FeatureFlags
        {
            get => base.FeatureFlags;
            set => base.FeatureFlags = value;
        }

        [CascadingParameter]
        public override EditMode EditMode
        {
            get => base.EditMode;
            set => base.EditMode = value;
        }

        [CascadingParameter]
        public override EntryMode EntryMode
        {
            get => base.EntryMode;
            set => base.EntryMode = value;
        }

        public bool Checked
        {
            get => Data.StateFlags.Checked;
            set => Data.StateFlags.Checked = value;
        }

        [CascadingParameter]
        public string GridTemplateColumns { get; set; } = default!;

        [CascadingParameter]
        public int RubricOrdinalSeed { get; set; } = 1;

        public void OnItemClick()
        {
            if (Checked)
                Checked = false;
            else
                Checked = true;
        }

        [Parameter]
        public Type? ValidatorType { get; set; }

        [Parameter]
        public IViewRubrics? MenuRubrics { get; set; }

        private IViewData? GetItemOperationsData()
        {
            Data.EntryMode = EntryMode;
            IViewData? data = null;
            if (FeatureFlags.Editable && EditMode != EditMode.None)
                data = typeof(ViewData<>)
                    .MakeGenericType(typeof(GenericDataGridBodyItemMenuEdit))
                    .New<IViewData>(new GenericDataGridBodyItemMenuEdit(Data));
            else if (FeatureFlags.Showable)
                data = typeof(ViewData<>)
                    .MakeGenericType(typeof(GenericDataGridBodyItemMenuShow))
                    .New<IViewData>(new GenericDataGridBodyItemMenuShow(Data));
            if (data != null)
            {
                if (MenuRubrics != null)
                    data.ExtendedRubrics = MenuRubrics;
                else
                    data.MapRubrics(r => r.ExtendedRubrics, r => r.Visible, false);
            }
            return data;
        }
    }
}
