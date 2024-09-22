using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Menu;

public partial class GenericFieldMenu : FluentComponentBase, IDisposable
{
    private DotNetObjectReference<GenericFieldMenu>? _dotNetHelper = null;
    private Point _clickedPoint = default;
    private bool _contextMenu = false;
    private readonly Dictionary<string, GenericFieldMenuItem> items = [];
    private IJSObjectReference _jsModule = default!;

    /// <summary/>
    internal string? ClassValue => new CssBuilder(Class)
        .Build();

    /// <summary/>
    internal string? StyleValue => new StyleBuilder(Style)
        .AddStyle("min-width: min-content")
        .AddStyle("width", Width, () => !string.IsNullOrEmpty(Width))
        .AddStyle("border-radius: calc(var(--layer-corner-radius) * 1px)")
            .AddStyle(!NoPadding ? "padding: 10px 10px 4px 10px" : "")
        .AddStyle("background: var(--fill-color)")
        .AddStyle(!NoShadow ? "box-shadow: var(--elevation-shadow-flyout)" : "")

        // For Anchored == false
        .AddStyle("z-index", $"{ZIndex.Menu}", () => !Anchored)
        .AddStyle("position", "fixed", () => !Anchored && !string.IsNullOrEmpty(Anchor))
        .AddStyle("width", "unset", () => !Anchored)
        .AddStyle("height", "unset", () => !Anchored)
        .AddStyle("left", $"{_clickedPoint.X}px", () => !Anchored && _clickedPoint.X != 0)
        .AddStyle("top", $"{_clickedPoint.Y}px", () => !Anchored && _clickedPoint.Y != 0)
        .Build();

    /// <summary/>
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    /// <summary>
    /// Gets or sets the identifier of the source component clickable by the end
    /// user.
    /// </summary>
    [Parameter]
    public string Anchor { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the automatic trigger. See
    /// <seealso cref="MouseButton"/>
    /// Possible values are None (default), Left, Middle, Right, Back, Forward
    /// </summary>
    [Parameter]
    public MouseButton Trigger { get; set; } = MouseButton.None;

    /// <summary>
    /// Gets or sets the Menu status.
    /// </summary>
    [Parameter]
    public bool Open { get; set; }

    /// <summary>
    /// Gets or sets the Menu status.
    /// </summary>
    [Parameter]
    public bool NoPadding { get; set; }


    /// <summary>
    /// Gets or sets the Menu status.
    /// </summary>
    [Parameter]
    public bool NoShadow { get; set; }

    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the menu position (left or right).
    /// </summary>
    [Parameter]
    public HorizontalPosition HorizontalPosition { get; set; } = HorizontalPosition.Unset;

    /// <summary>
    /// Gets or sets the menu position (left or right).
    /// </summary>
    [Parameter]
    public VerticalPosition VerticalPosition { get; set; } = VerticalPosition.Bottom;


    /// <summary>
    /// Gets or sets the width of this menu.
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// Raised when the <see cref="Open"/> property changed.
    /// </summary>
    [Parameter]
    public EventCallback<bool> OpenChanged { get; set; }

    /// <summary>
    /// Draw the menu below the component clicked (true) or using the mouse
    /// coordinates (false).
    /// </summary>
    [Parameter]
    public bool Anchored { get; set; } = true;

    /// <summary>
    /// Gets or sets how short the space allocated to the default position has
    /// to be before the tallest area is selected for layout.
    /// </summary>
    [Parameter]
    public int VerticalThreshold { get; set; } = 0;

    /// <summary>
    /// Gets or sets how narrow the space allocated to the default position has
    /// to be before the widest area is selected for layout.
    /// </summary>
    [Parameter]
    public int HorizontalThreshold { get; set; } = 200;

    protected override void OnInitialized()
    {
        if (Anchored && string.IsNullOrEmpty(Anchor))
        {
            Anchored = false;
        }
        base.OnInitialized();
    }

    /// <summary/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {

            if (Trigger != MouseButton.None)
            {
                _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                    "./_content/Undersoft.SDK.Service.Application.GUI/View/Generic/Menu/GenericFieldMenu.razor.js");

                _dotNetHelper = DotNetObjectReference.Create(this);

                if (Anchor is not null)
                {
                    // Add LeftClick event
                    if (Trigger == MouseButton.Left)
                    {
                        await _jsModule.InvokeVoidAsync("addEventLeftClick", Anchor, _dotNetHelper);
                    }

                    // Add RightClick event
                    if (Trigger == MouseButton.Right)
                    {
                        _contextMenu = true;
                        await _jsModule.InvokeVoidAsync("addEventRightClick", Anchor, _dotNetHelper);
                    }
                }
            }
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MenuChangeEventArgs))]
    public GenericFieldMenu()
    {
        Id = Identifier.NewId();
    }

    /// <summary>
    /// Close the menu.
    /// </summary>
    /// <returns></returns>
    public async Task CloseAsync()
    {
        Open = false;

        await OpenChanged.InvokeAsync(Open);

    }

    /// <summary>
    /// Method called from JavaScript to get the current mouse ccordinates.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OpenAsync(int x, int y)
    {
        _clickedPoint = new Point(x, y);
        Open = true;

        if (OpenChanged.HasDelegate)
        {
            await OpenChanged.InvokeAsync(Open);
        }

        StateHasChanged();
    }

    internal void Register(GenericFieldMenuItem item)
    {
        items.Add(item.Id!, item);
    }

    internal void Unregister(GenericFieldMenuItem item)
    {
        items.Remove(item.Id!);
    }

    /// <summary>
    /// Dispose this menu.
    /// </summary>
    public void Dispose()
    {
        //_dotNetHelper?.Dispose();
    }
}
