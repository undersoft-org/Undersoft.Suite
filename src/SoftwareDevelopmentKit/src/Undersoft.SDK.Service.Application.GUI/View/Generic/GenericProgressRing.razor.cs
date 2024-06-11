using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic;

public partial class GenericProgressRing : ViewItem
{
    [Parameter]
    public IViewProgress View { get; set; } = default!;

    [Parameter]
    public JustifyContent Justification { get; set; } = JustifyContent.Center;

    [Parameter]
    public Align Alignment { get; set; } = Align.Center;

    [Parameter]
    public bool Fullscreen { get; set; }

    protected void HandleOnClose() { }
}
