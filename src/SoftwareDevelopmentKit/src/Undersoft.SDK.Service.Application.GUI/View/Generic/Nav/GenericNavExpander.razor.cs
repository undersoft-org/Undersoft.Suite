using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Nav
{
    public partial class GenericNavExpander<TMenu> : ViewItem<TMenu> where TMenu : class, IOrigin, IInnerProxy
    {
        [Parameter]
        public bool ShowLabel { get; set; }

        [Parameter]
        public override string? Label { get; set; } = typeof(TMenu).Name;

        [Parameter]
        public Icon? ExpandedIcon { get; set; } = new Icons.Regular.Size20.MoreHorizontal();

        [Parameter]
        public Icon? CollapsedIcon { get; set; } = new Icons.Regular.Size20.MoreHorizontal();

        [Parameter]
        public Color IconColor { get; set; } = Color.FillInverse;

        [Parameter]
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Right;

        [CascadingParameter]
        public bool Expanded { get => StateFlags.Expanded; set => StateFlags.Expanded = value; }

    }
}