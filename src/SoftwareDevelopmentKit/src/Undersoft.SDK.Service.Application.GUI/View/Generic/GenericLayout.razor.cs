using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Utilities;
using System.Reflection;
using System.Text.Json;
using Undersoft.SDK.Service.Application.Extensions;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Updating;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic;

public partial class GenericLayout : LayoutComponentBase
{
    private const string JAVASCRIPT_FILE =
        "./_content/Undersoft.SDK.Service.Application.GUI/View/Generic/GenericLayout.razor.js";
    public string? Version;
    public bool IsDarkMode;
    public bool IsDevice;
    protected bool _mobile;
    protected string? _prevUri;
    private GenericPageContents? _toc;
    private bool _menuChecked = true;
    private readonly string APPEARANCEKEY = "APPEARANCEKEY";

    protected string? ClassValue => new CssBuilder(Class)
       .AddClass("layout")
       .Build();

    protected string? StyleValue => new StyleBuilder(Style).Build();

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Color { get; set; } = default!;

    public DesignThemeModes Mode { get; set; }

    [Parameter]
    public int? BaseHorizontalSpacingMultiplier { get; set; } = 3;    

    [Parameter]
    public int? Density { get; set; } = 0;

    [Parameter]
    public int? FontWeight { get; set; } = 300;

    [Parameter]
    public int? FocusStrokeWidth { get; set; } = 1;

    [Parameter]
    public string? TypeRampBaseFontSize { get; set; } = "13px";

    [Parameter]
    public string? TypeRampBaseLineHeight { get; set; } = "18px";

    [Parameter]
    public int? BaseHeight { get; set; } = 7;

    [Parameter]
    public int? ControlCornerRadius { get; set; } = 0;

    [Parameter]
    public int? LayerCornerRadius { get; set; } = 0;

    [Parameter]
    public float? DisabledOpacity { get; set; } = 1F;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    [Inject]
    public IAccessProvider Access { get; set; } = default!;

    [Inject]
    private AppearanceState AppearanceState { get; set; } = default!;

    [Inject]
    private GlobalState GlobalState { get; set; } = default!;

    public object? Reference { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public virtual IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!AppearanceState.IsLoaded)
        {
            var appearanceState = await JS.GetFromLocalStorage(APPEARANCEKEY);
            if (appearanceState != null)
                AppearanceState.PatchFromJson<AppearanceState, AppearanceState>(appearanceState);
            else
                await JS.SetInLocalStorage(APPEARANCEKEY, this.PatchTo(AppearanceState).ToJsonString());
        }
        AppearanceState.IsLoaded = true;
        Mode = AppearanceState.IsDarkMode ? DesignThemeModes.Dark : DesignThemeModes.Light;
        Color = AppearanceState.Color;
        Density = AppearanceState.Density;
        ControlCornerRadius = AppearanceState.ControlCornerRadius;
        LayerCornerRadius = AppearanceState.LayerCornerRadius;

        var versionAttribute = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>();

        if (versionAttribute != null)
        {
            var version = versionAttribute.InformationalVersion.Split('+')[0];
            AppearanceState.Version = version;
        }

        _prevUri = NavigationManager.Uri;
        NavigationManager.LocationChanged += LocationChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var jsModule = await JS.InvokeAsync<IJSObjectReference>(
                "import",
                JAVASCRIPT_FILE
            );
            AppearanceState.IsDevice = _mobile = await jsModule.InvokeAsync<bool>("isDevice");
            await jsModule.DisposeAsync();
        }
    }

    public EventCallback OnRefreshTableOfContents =>
        EventCallback.Factory.Create(this, RefreshTableOfContentsAsync);

    private async Task RefreshTableOfContentsAsync()
    {
        await _toc!.RefreshAsync();
    }

    private void HandleChecked()
    {
        _menuChecked = !_menuChecked;
    }

    private void LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        if (
            !e.IsNavigationIntercepted
            && new Uri(_prevUri!).AbsolutePath != new Uri(e.Location).AbsolutePath
        )
        {
            if (Access.AccessExpiration != null)
            {
                if(DateTime.Now.AddMinutes(5) > Access.AccessExpiration)
                {
                    if (DateTime.Now.AddSeconds(30) > Access.AccessExpiration)
                        NavigationManager.NavigateTo("", true);
                    else
                        Access.RefreshState();
                }
            }

            _prevUri = e.Location;
            if (_mobile && _menuChecked == true)
            {
                _menuChecked = false;
                StateHasChanged();
            }
        }
    }
}
