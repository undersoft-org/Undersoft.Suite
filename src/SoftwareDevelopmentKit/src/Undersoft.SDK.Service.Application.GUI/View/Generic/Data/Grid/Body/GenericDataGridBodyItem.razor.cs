using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body
{
    public partial class GenericDataGridBodyItem : ViewItem
    {
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

        [CascadingParameter]
        public ViewRubrics MenuShowRubrics { get; set; } = default!;

        [CascadingParameter]
        public ViewRubrics MenuEditRubrics { get; set; } = default!;

        private IViewData? GetItemOperationsData()
        {
            Data.EntryMode = EntryMode;
            if (FeatureFlags.Editable && EditMode != EditMode.None)
            {
                var data = typeof(ViewData<>)
                    .MakeGenericType(typeof(GenericDataGridBodyItemMenuEdit))
                    .New<IViewData>(new GenericDataGridBodyItemMenuEdit(Data));
                data.ExtendedRubrics = MenuEditRubrics;
                return data;
            }
            else if (FeatureFlags.Showable)
                return GetItemShowData();
            return null;
        }

        private IViewData GetItemShowData()
        {
            var data = typeof(ViewData<>)
                .MakeGenericType(typeof(GenericDataGridBodyItemMenuShow))
                .New<IViewData>(new GenericDataGridBodyItemMenuShow(Data));
            data.ExtendedRubrics = MenuShowRubrics;
            return data;
        }
    }
}
