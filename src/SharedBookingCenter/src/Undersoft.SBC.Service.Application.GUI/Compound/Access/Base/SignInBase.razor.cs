using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Updating;
using Undersoft.SBC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.SBC.Service.Contracts;

namespace Undersoft.SBC.Service.Application.GUI.Compound.Access
{
    public partial class SignInBase : ComponentBase
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
            _dialog = _servicer.Initialize<AccessDialog<SignInDialog<Credentials, AccessValidator>, Credentials>>(DialogService);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await SigningIn("Sign in");
        }

        private async Task SigningIn(string title, string description = "")
        {
            var data = new ViewData<Credentials>(new Credentials(), OperationType.Access, title);
            data.SetRequired(nameof(Credentials.Email), nameof(Credentials.Password));
            data.SetVisible(nameof(Credentials.SaveAccountInCookies));
            data.Description = description;
            data.Height = "400px";
            data.Width = "360px";

            while (true)
            {
                await _dialog.Show(data);

                var content = _dialog.Content;

                if (content != null)
                {
                    if (content.StateFlags.HaveNext && content.NextHref != null)
                    {
                        _navigation.NavigateTo(content.NextHref);
                        return;
                    }

                    var result = await _access.SignIn(new Account() { Credentials = content.Model });

                    if (result.Credentials.Authenticated)
                    {
                        if (result.Credentials.EmailConfirmed)
                            if (result.Credentials.RegistrationCompleted)
                                _navigation.NavigateTo("/presenting/dashboard", true);
                            else
                                _navigation.NavigateTo("/access/register");
                        else
                            _navigation.NavigateTo($"/access/confirm_email/{result.Credentials.Email}");
                        return;
                    }

                    data.ClearData();
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