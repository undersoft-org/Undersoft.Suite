using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Search
{
    public partial class GenericDataSearch : ViewStore
    {
        private bool _isAddable = false;
        private bool _isOpen;

        protected override void OnInitialized()
        {
            if (Parent == null)
                Root = this;

            if (Data.Searchable)
            {
                Data.SearchMembers.ForEach(m =>
                {
                    FilterEntries.Put(new Filter(m, null, CompareOperand.Contains, LinkOperand.Or));
                });
            }
            base.OnInitialized();
        }

        public ISeries<Filter> FilterEntries { get; set; } = new Listing<Filter>();

        [CascadingParameter]
        public override IViewDataStore DataStore
        {
            get => base.DataStore;
            set => base.DataStore = value;
        }

        public virtual bool IsOpen { get; set; }

        [Parameter]
        public bool ShowIcons { get; set; } = true;

        [Parameter]
        public HorizontalPosition Position { get; set; } = HorizontalPosition.Right;

        [Parameter]
        public VerticalPosition VerticalPosition { get; set; } = VerticalPosition.Bottom;

        [Parameter]
        public override string? Style { get; set; }

        [Parameter]
        public virtual string? Width { get; set; }

        [Parameter]
        public string AnchorId { get; set; } = default!;

        [Parameter]
        public bool Anchored { get; set; } = default!;

        [Parameter]
        public MouseButton Trigger { get; set; } = MouseButton.Left;

        private event EventHandler<object> _onMenuItemChange = default!;

        [Parameter]
        public virtual EventHandler<object> OnMenuItemChange
        {
            get => _onMenuItemChange;
            set
            {
                if (value != null)
                    _onMenuItemChange += value;
            }
        }
    }
}
