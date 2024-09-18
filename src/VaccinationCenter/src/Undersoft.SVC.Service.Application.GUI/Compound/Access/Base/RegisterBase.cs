using Microsoft.FluentUI.AspNetCore.Components;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Updating;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Access;

using Undersoft.SVC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.SVC.Service.Contracts;

/// <summary>
/// The register base.
/// </summary>
public partial class RegisterBase : ComponentBase
{
    /// <summary>
    /// Gets or sets the access.
    /// </summary>
    /// <value>An IAccountService</value>
    [Inject]
    private IAccessService<Account> _access { get; set; } = default!;

    /// <summary>
    /// Gets or sets the navigation.
    /// </summary>
    /// <value>A <see cref="NavigationManager"/></value>
    [Inject]
    private NavigationManager _navigation { get; set; } = default!;

    /// <summary>
    /// Gets or sets the servicer.
    /// </summary>
    /// <value>An <see cref="IServicer"/></value>
    [Inject]
    private IServicer _servicer { get; set; } = default!;

    /// <summary>
    /// Gets or sets the authorization.
    /// </summary>
    /// <value>An <see cref="IAuthorization"/></value>
    [Inject]
    private IAuthorization _authorization { get; set; } = default!;

    /// <summary>
    /// Gets or sets the dialog service.
    /// </summary>
    /// <value>An <see cref="IDialogService"/></value>
    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    /// <summary>
    /// The dialog.
    /// </summary>
    private IViewDialog<Account> _dialog = default!;

    /// <summary>
    /// On initialized.
    /// </summary>
    protected override void OnInitialized()
    {
        _dialog = _servicer.Initialize<AccessDialog<RegisterDialog<Account, AccountValidator>, Account>>(DialogService);
    }

    /// <summary>
    /// On after render.
    /// </summary>
    /// <param name="firstRender">If true, first render.</param>
    /// <returns>A <see cref="Task"/></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await Registering("Registration");
    }

    private async Task Registering(string title, string description = "")
    {
        var account = new Account(_authorization.Credentials.Email ?? "your@personal.email");

        account.Credentials = _authorization.Credentials;
        _authorization.Credentials.PatchTo(account.Personal);

        var data = new ViewData<Account>(account, OperationType.Any, title);

        data.Description = description;

        while (true)
        {
            await _dialog.Show(data);
            var result = await HandleDialog(_dialog.Content);
            if (result == null)
                break;
            result.Notes.PatchTo(data.Notes);
        }
    }

    private async Task<IAuthorization?> HandleDialog(IViewData<Account>? content)
    {
        if (content == null)
        {
            _navigation.NavigateTo("", true);
            return null;
        }

        if (content!.StateFlags.Canceled)
            content.Model.Notes.Status = AccessStatus.RegistrationNotCompleted;

        var result = await _access.Register(content.Model);

        if (
            result.Notes.Status != AccessStatus.InvalidEmail
            && result.Notes.Status == AccessStatus.RegistrationCompleted
        )
        {
            _navigation.NavigateTo("access/sign_in");
            return null;
        }

        return result;
    }
}
