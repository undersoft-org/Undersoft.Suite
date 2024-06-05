using FluentValidation;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Presenting
{
    public partial class GenericPresentingHeader<TMenu, TAccount, TModel, TValidator> : FluentComponentBase where TMenu : class, IOrigin, IInnerProxy where TAccount : class, IOrigin, IInnerProxy where TModel : class, IOrigin, IInnerProxy, IAuthorization where TValidator : class, IValidator<IViewData<TModel>>
    {
        [Parameter]
        public Icon? Logo { get; set; }

        [CascadingParameter]
        public AppearanceState? AppearanceState { get; set; } = default!;
    }
}
