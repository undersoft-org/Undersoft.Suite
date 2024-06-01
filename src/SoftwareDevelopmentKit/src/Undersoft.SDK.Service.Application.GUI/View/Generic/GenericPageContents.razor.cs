using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.Models;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic;

public partial class GenericPageContents : FluentComponentBase, IAsyncDisposable
{
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

    private Anchor[]? _anchors;
    private bool _expanded = true;

    private IJSObjectReference _jsModule = default!;

    [Inject]
    protected IJSRuntime JSRuntime { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public string Heading { get; set; } = "In this article";

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter]
    public AppearanceState AppearanceState { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
            "./_content/Undersoft.SDK.Service.Application.GUI/View/Generic/GenericPageContents.razor.js");
            var mobile = AppearanceState.IsDevice;

            await QueryDomAsync();
        }
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
        // Subscribe to the event
        NavigationManager.LocationChanged += LocationChanged;
    }

    private async void LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        try
        {
            await QueryDomAsync();
        }
        catch (Exception)
        {
            // Already disposed
        }
    }

    public async Task RefreshAsync()
    {
        await QueryDomAsync();
    }

    private RenderFragment? GetItems(IEnumerable<Anchor>? items)
    {
        if (items is not null)
        {
            return new RenderFragment(builder =>
            {
                var i = 0;

                builder.OpenElement(i++, "ul");
                foreach (Anchor item in items)
                {
                    builder.OpenElement(i++, "li");
                    builder.OpenComponent<FluentAnchor>(i++);
                    builder.AddAttribute(i++, "Href", item.Href);
                    builder.AddAttribute(i++, "Appearance", Microsoft.FluentUI.AspNetCore.Components.Appearance.Hypertext);
                    builder.AddAttribute(i++, "ChildContent", (RenderFragment)(content =>
                    {
                        content.AddContent(i++, item.Text);
                    }));
                    builder.CloseComponent();
                    if (item.Anchors is not null)
                    {
                        builder.AddContent(i++, GetItems(item.Anchors));
                    }
                    builder.CloseElement();
                }
                builder.CloseElement();
            });
        }
        else
        {
            return new RenderFragment(builder =>
            {
                builder.AddContent(0, ChildContent);
            });
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
