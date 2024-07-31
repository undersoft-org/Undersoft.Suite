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
using Undersoft.SDK.Service.Application.GUI.View.Generic.Form.Dialog;
using Undersoft.SDK.Updating;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Access;

using Undersoft.SVC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.SVC.Service.Contracts;

/// <summary>
/// The confirm password reset base.
/// </summary>
public partial class ConfirmPasswordResetBase : ComponentBase
{
    /// <summary>
    /// Gets or sets the access.
    /// </summary>
    /// <value>An <see cref="IAccess"/></value>
    [Inject]
    private IAccess _access { get; set; } = default!;

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
    /// Gets or sets the dialog service.
    /// </summary>
    /// <value>An <see cref="IDialogService"/></value>
    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    /// <summary>
    /// The dialog.
    /// </summary>
    private IViewDialog<Credentials> _dialog = default!;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Parameter]
    public string? Email { get; set; }

    /// <summary>
    /// On initialized.
    /// </summary>
    protected override void OnInitialized()
    {
        _dialog = _servicer.Initialize<
            AccessDialog<GenericFormDialog<Credentials, AccessValidator>, Credentials>
        >(DialogService);
    }

    /// <summary>
    /// On after render.
    /// </summary>
    /// <param name="firstRender">If true, first render.</param>
    /// <returns>A <see cref="Task"/></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await ConfirmingPasswordReset(
                "Password recovery confirmation",
                "Please check your e-mail"
            );
    }

    /// <summary>
    /// Confirming password reset.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="description">The description.</param>
    /// <returns>A <see cref="Task"/></returns>
    private async Task ConfirmingPasswordReset(string title, string description = "")
    {
        var data = new ViewData<Credentials>(
            new Credentials() { Email = Email },
            OperationType.Delete,
            title
        );
        data.SetVisible("PasswordResetToken");
        data.Description = description;

        while (true)
        {
            await _dialog.Show(data);
            var result = await HandleDialog(_dialog.Content);
            if (result == null)
                break;
            data.ClearData();
            result.Notes.PatchTo(data.Notes);
        }
    }

    /// <summary>
    /// Handle the dialog.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns>A <see cref="Task"/> of type <see cref="IAuthorization"/></returns>
    private async Task<IAuthorization?> HandleDialog(IViewData<Credentials>? content)
    {
        if (content == null)
        {
            _navigation.NavigateTo("");
            return null;
        }

        var result = await _access.ResetPassword(new Account() { Credentials = content!.Model });

        if (result.Notes.Status == AccessStatus.ResetPasswordConfirmed)
        {
            _navigation.NavigateTo("");
            return null;
        }

        return result;
    }
}
