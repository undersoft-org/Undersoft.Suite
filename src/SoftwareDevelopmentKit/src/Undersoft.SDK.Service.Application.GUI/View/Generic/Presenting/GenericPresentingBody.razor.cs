using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Presenting
{
    public partial class GenericPresentingBody<TMenu> : FluentComponentBase where TMenu : class, IOrigin, IInnerProxy
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
