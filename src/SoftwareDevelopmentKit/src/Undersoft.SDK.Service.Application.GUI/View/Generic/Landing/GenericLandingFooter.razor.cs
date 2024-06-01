using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.Models;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Landing
{
    public partial class GenericLandingFooter : FluentComponentBase
    {
        [CascadingParameter]
        public AppearanceState? AppearanceState { get; set; } = default!;
    }
}
