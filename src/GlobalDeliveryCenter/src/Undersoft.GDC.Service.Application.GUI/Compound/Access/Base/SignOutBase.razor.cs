using Undersoft.SDK.Service.Access;

namespace Undersoft.GDC.Service.Application.GUI.Compound.Access
{
    public partial class SignOutBase : ComponentBase
    {
        [Inject]
        private IAuthorization _auth { get; set; } = default!;

        [Inject] IAccess _access { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            await _access.SignOut(_auth);

            _navigation.NavigateTo("", true);
        }
    }
}