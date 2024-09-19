using Microsoft.AspNetCore.Components.Web;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Menu;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body
{
    public partial class GenericDataGridBodyItemValue : ViewItem
    {
        [Parameter]
        public IViewData? OperationsData { get; set; }

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

        [CascadingParameter]
        public int RubricOrdinalSeed { get; set; } = 1;

        private int _ordinal => Rubric.RubricOrdinal + RubricOrdinalSeed;

        [CascadingParameter]
        public override IViewData Data { get; set; } = default!;

        [CascadingParameter]
        public bool Multiline { get; set; }

        public override string ViewId => Data.ViewId + Rubric.CodeNo;

        public GenericMenu? Menu { get; set; }

        public void OnCellClick(MouseEventArgs args)
        {
            if (Menu != null && Menu.Openings > 0)
                Menu.Openings--;
            else
            {
                IViewRubric? actionRubric = null;
                if (FeatureFlags.Editable && EditMode != EditMode.None)
                    actionRubric = OperationsData?.ExtendedRubrics["Edit"];
                else if (FeatureFlags.Showable)
                    actionRubric = OperationsData?.ExtendedRubrics["Show"];
                if (actionRubric != null && OperationsData != null)
                    actionRubric.Invoker.Invoke(OperationsData.Model.Proxy[actionRubric.RubricId]);
            }
        }

    }
}
