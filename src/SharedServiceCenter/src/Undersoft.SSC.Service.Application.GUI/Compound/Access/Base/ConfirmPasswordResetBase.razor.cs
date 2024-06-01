using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Form.Dialog;
using Undersoft.SDK.Updating;
using Undersoft.SSC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Access
{
    public partial class ConfirmPasswordResetBase : ComponentBase
    {
        [Inject]
        private IAccountAccess _access { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private IServicer _servicer { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IViewDialog<Credentials> _dialog = default!;

        [Parameter]
        public string? Email { get; set; }

        protected override void OnInitialized()
        {
            _dialog = _servicer.Initialize<AccessDialog<GenericFormDialog<Credentials, AccessValidator>, Credentials>>(DialogService);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await ConfirmingPasswordReset("Password recovery confirmation", "Please check your e-mail");
        }

        private async Task ConfirmingPasswordReset(string title, string description = "")
        {
            var data = new ViewData<Credentials>(new Credentials() { Email = Email }, OperationType.Delete, title);
            data.SetVisible("PasswordResetToken");
            data.Description = description;
            data.Width = "360px";
            data.Height = "360px";

            while (true)
            {
                await _dialog.Show(data);

                var content = _dialog.Content;

                if (content != null)
                {
                    var result = await _access.ResetPassword(new Account() { Credentials = content.Model });

                    if (result.Notes.Status == SigningStatus.ResetPasswordConfirmed)
                    {
                        _navigation.NavigateTo($"");
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