using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Form.Dialog;
using Undersoft.SDK.Updating;
using Undersoft.GDC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.GDC.Service.Contracts;

namespace Undersoft.GDC.Service.Application.GUI.Compound.Access
{
    public partial class ResetPasswordBase : ComponentBase
    {
        [Inject]
        private IAccess _access { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private IServicer _servicer { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IViewDialog<Credentials> _dialog = default!;

        protected override void OnInitialized()
        {
            _dialog = _servicer.Initialize<AccessDialog<GenericFormDialog<Credentials, AccessValidator>, Credentials>>(DialogService);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await ResettingPassword("Password recovery", "Please provide account e-mail address");
        }

        private async Task ResettingPassword(string title, string description = "")
        {
            var data = new ViewData<Credentials>(new Credentials(), OperationType.Update, title);
            data.SetVisible("Email");
            data.Description = description;
            data.Height = "360px";
            data.Width = "360px";

            while (true)
            {
                await _dialog.Show(data);

                var content = _dialog.Content;

                if (content != null)
                {
                    var result = await _access.ResetPassword(new Account() { Credentials = content.Model });

                    if (result.Notes.Status != AccessStatus.InvalidEmail && result.Notes.Status == AccessStatus.ResetPasswordNotConfirmed)
                    {
                        _navigation.NavigateTo($"access/confirm_password_reset/{result.Credentials.Email}");
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