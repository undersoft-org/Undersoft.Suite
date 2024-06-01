using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.Models;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic;

public partial class GenericGoTopButton : FluentComponentBase, IAsyncDisposable
{
    private IJSObjectReference _jsModule = default!;

    private Anchor[]? _anchors;

    private record Anchor(string Level, string Text, string Href, Anchor[] Anchors)
    {
        public virtual bool Equals(Anchor? other)
        {
            if (other is null)
            {
                return false;
            }

            if (Level != other.Level ||
                Text != other.Text ||
                Href != other.Href ||
                (Anchors?.Length ?? 0) != (other.Anchors?.Length ?? 0))
            {
                return false;
            }

            if (Anchors is not null &&
                Anchors.Length > 0)
            {
                for (var i = 0; i < Anchors.Length; i++)
                {
                    if (!Anchors[i].Equals(other.Anchors![i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
             => HashCode.Combine(Level, Text, Href);
    }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public bool ShowBackButton { get; set; } = true;

    [CascadingParameter]
    public AppearanceState AppearanceState { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
            "./_content/Undersoft.SDK.Service.Application.GUI/View/Generic/GenericGoTopButton.razor.js");
            var mobile = AppearanceState.IsDevice;

            await BackToTopAsync();
        }
    }

    private async Task BackToTopAsync()
    {
        if (_jsModule is null)
        {
            return;
        }
        await _jsModule.InvokeAsync<Anchor[]?>("backToTop");
    }

    private async Task QueryDomAsync()
    {
        if (_jsModule is null)
        {
            return;
        }

        Anchor[]? foundAnchors = await _jsModule.InvokeAsync<Anchor[]?>("queryDomForTocEntries");

        if (AnchorsEqual(_anchors, foundAnchors))
        {
            return;
        }

        _anchors = foundAnchors;
        StateHasChanged();
    }

    private bool AnchorsEqual(Anchor[]? firstSet, Anchor[]? secondSet)
    {
        return (firstSet ?? [])
            .SequenceEqual(secondSet ?? []);
    }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += LocationChanged;
    }

    private async void LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        try
        {
            await BackToTopAsync();
        }
        catch (Exception)
        {
            // Already disposed
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            // Unsubscribe from the event when our component is disposed
            NavigationManager.LocationChanged -= LocationChanged;

            if (_jsModule is not null)
            {
                await _jsModule.DisposeAsync();
            }
        }
        catch (JSDisconnectedException)
        {
            // The JSRuntime side may routinely be gone already if the reason we're disposing is that
            // the client disconnected. This is not an error.
        }
    }
}
