using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SCC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Updating;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Access
{
    public partial class SignUpBase : ComponentBase
    {
        [Inject]
        private IAccountAccess _access { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private IServicer _servicer { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IViewModelDialog<Credentials> _dialog = default!;

        protected override void OnInitialized()
        {
            _dialog = _servicer.Initialize<AccessDialog<SignUpDialog<Credentials, AccessValidator>, Credentials>>(DialogService);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await SigningUp("Sign up");
        }

        private async Task SigningUp(string title, string description = "")
        {
            var data = new ViewData<Credentials>(new Credentials(), OperationType.Create, title);
            data.SetVisible("FirstName", "LastName", "Email", "Password", "RetypedPassword");
            data.Description = description;
            data.Height = "600px";
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
                    content.Model.UserName = $"{content.Model.FirstName}__"
                                           + $"{content.Model.LastName}__{content.Model.Email.ToLowerInvariant().Replace("@", ".")}";

                    var result = await _access.SignUp(new Account() { Credentials = content.Model });

                    if (result.Notes.Status != SigningStatus.Failure && result.Notes.Errors == null)
                    {
                        _navigation.NavigateTo($"access/confirm_email/{result.Credentials.Email}");
                        return;
                    }
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