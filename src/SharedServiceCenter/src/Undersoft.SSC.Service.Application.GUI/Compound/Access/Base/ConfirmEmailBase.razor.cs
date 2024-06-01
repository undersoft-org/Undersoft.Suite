using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Updating;
using Undersoft.SSC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Access
{
    public partial class ConfirmEmailBase : ComponentBase
    {
        [Inject]
        private IAccountAccess _access { get; set; } = default!;

        [Inject]
        private IAuthorization _authorization { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private IServicer _servicer { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        [Parameter]
        public string? Email { get; set; }

        private IViewDialog<Credentials> _dialog = default!;

        protected override void OnInitialized()
        {
            _dialog = _servicer.Initialize<AccessDialog<ConfirmEmailDialog<Credentials, AccessValidator>, Credentials>>(DialogService);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await ConfirmingEmail("Confirm email", "Please check your e-mail");
        }

        private async Task ConfirmingEmail(string title, string description = "")
        {
            var data = new ViewData<Credentials>(_authorization.Credentials, OperationType.Setup, title);
            data.SetVisible("EmailConfirmationToken");
            data.Description = description;
            data.Width = "360px";
            data.Height = "360px";

            while (true)
            {
                await _dialog.Show(data);

                var content = _dialog.Content;

                if (content != null)
                {
                    var result = await _access.ConfirmEmail(new Account() { Credentials = content.Model });

                    if (result.Notes.Status == SigningStatus.EmailConfirmed)
                    {
                        _navigation.NavigateTo("access/register");
                        return;
                    }
                    data.ClearData();
                    result.Notes.PatchTo(data.Notes);
                }
                else
                {
                    _navigation.NavigateTo("");
                    return;
                }
            }
        }
    }
}