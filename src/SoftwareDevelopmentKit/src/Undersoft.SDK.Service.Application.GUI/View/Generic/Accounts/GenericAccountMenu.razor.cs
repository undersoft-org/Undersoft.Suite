using FluentValidation;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Accounts
{
    public partial class GenericAccountMenu<TModel, TValidator> : ViewItem
        where TModel : class, IOrigin, IInnerProxy, IAuthorization
        where TValidator : class, IValidator<IViewData<TModel>>
    {
        [Inject]
        private AccessProvider<TModel> access { get; set; } = default!;

        [Inject]
        private IServicer servicer { get; set; } = default!;

        [Inject]
        private IDialogService DialogService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var state = await access.GetAuthenticationStateAsync();

            if (state != null && state.User.Identity != null)
            {
                var _rubrics = Data.Model.Proxy.Rubrics;

                if (!state.User.Identity.IsAuthenticated)
                {
                    _rubrics["Account"].Extended = false;
                    _rubrics["SignOut"].Extended = false;
                    _rubrics["SignIn"].Extended = true;
                    _rubrics["SignUp"].Extended = true;
                }
                else
                {
                    var _panel = servicer.Initialize<
                        ViewPanel<GenericAccountPanel<TModel, TValidator>, TModel>
                    >(DialogService);

                    var _account = await access.Registered(typeof(TModel).New<TModel>());
                    if (_account != null)
                        _panel.Content = new ViewData<TModel>(_account, OperationType.Any);

                    Data.Model.Proxy[_rubrics["Account"].RubricId] = _panel;

                    _rubrics["SignIn"].Extended = false;
                    _rubrics["SignUp"].Extended = false;
                    _rubrics["Account"].Extended = true;
                    _rubrics["SignOut"].Extended = true;
                }
            }
            Data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
            if (Parent == null)
                Root = this;
            base.OnInitialized();
        }

        public bool IsOpen { get; set; }

        [Parameter]
        public HorizontalPosition Position { get; set; } = HorizontalPosition.Left;

        [Parameter]
        public override string? Style { get; set; }

        [Parameter]
        public string AnchorId { get; set; } = default!;

        [Parameter]
        public bool Anchored { get; set; } = default!;

        [Parameter]
        public MouseButton Trigger { get; set; } = MouseButton.Left;

        public void OnMenuChange(MenuChangeEventArgs args) { }
    }
}
