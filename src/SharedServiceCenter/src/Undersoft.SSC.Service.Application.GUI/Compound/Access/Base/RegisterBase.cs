using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Updating;
using Undersoft.SSC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Accounts;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Access
{
    public partial class RegisterBase : ComponentBase
    {
        [Inject]
        private IAccessService<Account> _access { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private IServicer _servicer { get; set; } = default!;

        [Inject]
        private IAuthorization _authorization { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IViewDialog<Account> _dialog = default!;

        protected override void OnInitialized()
        {
            _dialog = _servicer.Initialize<
                AccessDialog<RegisterDialog<Account, AccountValidator>, Account>
            >(DialogService);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await Registering("Registration");
        }

        private async Task Registering(string title, string description = "")
        {
            var account = new Account(_authorization.Credentials.Email)
            {
                Personal = new AccountPersonal(),
                Address = new AccountAddress(),
                Professional = new AccountProfessional(),
                Organization = new AccountOrganization()
            };

            account.Credentials = _authorization.Credentials;
            _authorization.Credentials.PatchTo(account.Personal);

            var data = new ViewData<Account>(account, OperationType.Any, title);
            data.SetVisible(
                nameof(Account.Personal),
                nameof(Account.Address),
                nameof(Account.Professional),
                nameof(Account.Organization)
            );
            data.Description = description;
            data.Height = "700px";
            data.Width = "400px";

            while (true)
            {
                await _dialog.Show(data);

                var content = _dialog.Content;

                if (content != null)
                {
                    if (content.StateFlags.Canceled)
                        content.Model.Notes.Status = AccessStatus.RegistrationNotCompleted;

                    var result = await _access.Register(content.Model);

                    if (
                        result.Notes.Status != AccessStatus.InvalidEmail
                        && result.Notes.Status == AccessStatus.RegistrationCompleted
                    )
                    {
                        _navigation.NavigateTo("access/sign_in");
                        return;
                    }
                    result.Notes.PatchTo(data.Notes);
                }
                else
                {
                    _navigation.NavigateTo("", true);
                    return;
                }
            }
        }
    }
}
