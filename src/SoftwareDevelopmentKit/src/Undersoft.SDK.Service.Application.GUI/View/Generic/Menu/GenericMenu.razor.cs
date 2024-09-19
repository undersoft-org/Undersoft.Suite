using Microsoft.FluentUI.AspNetCore.Components;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Menu
{
    public partial class GenericMenu : ViewItem
    {
        private bool _isOpen;

        protected override void OnInitialized()
        {
            Data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
            if (Parent == null)
                Root = this;

            base.OnInitialized();
        }

        public virtual int Openings { get; set; }

        public virtual bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                if (value)
                    Openings++;
            }
        }

        [Parameter]
        public NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public bool ShowIcons { get; set; } = true;

        [Parameter]
        public HorizontalPosition Position { get; set; } = HorizontalPosition.Right;

        [Parameter]
        public VerticalPosition VerticalPosition { get; set; } = VerticalPosition.Bottom;

        [Parameter]
        public override string? Style { get; set; }

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
