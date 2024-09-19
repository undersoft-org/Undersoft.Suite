using Microsoft.AspNetCore.Components.Web;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body
{
    public partial class GenericDataGridBodyItem : ViewItem
    {
        private IViewData? _operations;

        public override string ViewId => Data.ViewId;

        [CascadingParameter]
        public string? GridTemplateColumns { get; set; }

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
        public int RubricOrdinalSeed { get; set; } = 1;      

        [Parameter]
        public Type? ValidatorType { get; set; }

        [Parameter]
        public IViewRubrics? MenuRubrics { get; set; }

        private IViewData? GetOperationsData()
        {
            if (_operations != null)
                return _operations;

            Data.EntryMode = EntryMode;
            if (FeatureFlags.Editable && EditMode != EditMode.None)
                _operations = typeof(ViewData<>)
                    .MakeGenericType(typeof(GenericDataGridBodyItemMenuEdit))
                    .New<IViewData>(new GenericDataGridBodyItemMenuEdit(Data));
            else if (FeatureFlags.Showable)
                _operations = typeof(ViewData<>)
                    .MakeGenericType(typeof(GenericDataGridBodyItemMenuShow))
                    .New<IViewData>(new GenericDataGridBodyItemMenuShow(Data));
            if (_operations != null)
            {
                if (MenuRubrics != null)
                    _operations.ExtendedRubrics = MenuRubrics;
                else
                    _operations.MapRubrics(r => r.ExtendedRubrics, r => r.Visible, false);
            }
            return _operations;
        }
    }
}
