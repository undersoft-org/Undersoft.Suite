// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK.Service.Access;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Access
{
    /// <summary>
    /// The sign out base.
    /// </summary>
    public partial class SignOutBase : ComponentBase
    {
        /// <summary>
        /// Gets or sets the auth.
        /// </summary>
        /// <value>An <see cref="IAuthorization"/></value>
        [Inject]
        private IAuthorization _auth { get; set; } = default!;

        /// <summary>
        /// Gets or sets the access.
        /// </summary>
        /// <value>An <see cref="IAccess"/></value>
        [Inject] IAccess _access { get; set; } = default!;

        /// <summary>
        /// Gets or sets the navigation.
        /// </summary>
        /// <value>A <see cref="NavigationManager"/></value>
        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        /// <summary>
        /// On initialized.
        /// </summary>
        /// <returns>A <see cref="Task"/></returns>
        protected async override Task OnInitializedAsync()
        {
            await _access.SignOut(_auth);
            _navigation.NavigateTo("", true);
        }
    }
}