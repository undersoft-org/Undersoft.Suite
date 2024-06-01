using Microsoft.FluentUI.AspNetCore.Components;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Landing
{
    public partial class GenericLandingBody : FluentComponentBase
    {
        private GenericPageContents? _toc;

        [CascadingParameter]
        public RenderFragment? Body { get; set; }

        public EventCallback OnRefreshTableOfContents => EventCallback.Factory.Create(this, RefreshTableOfContentsAsync);

        private async Task RefreshTableOfContentsAsync()
        {
            await _toc!.RefreshAsync();
        }
    }
}
