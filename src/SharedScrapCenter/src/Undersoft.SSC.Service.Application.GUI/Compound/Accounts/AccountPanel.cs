using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Accounts;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Access;

public class AccountPanel
{
    public AccountPanel() { }

    public async Task Open(IViewPanel<Account> _panel)
    {
        IViewData<Account> data;
        if (_panel.Content != null)
            data = _panel.Content;
        else
        {
            var account = new Account()
            {
                Personal = new AccountPersonal(),
                Address = new AccountAddress(),
                Professional = new AccountProfessional(),
                Organization = new AccountOrganization()
            };
            data = new ViewData<Account>(account, OperationType.Any);
        }
        data.SetVisible(
            nameof(Account.Personal),
            nameof(Account.Address),
            nameof(Account.Professional),
            nameof(Account.Organization)
        );
        data.Width = "400px";

        await _panel.Show(
            data,
            (p) =>
            {
                p.Alignment = HorizontalAlignment.Right;
                p.Title = $"Account";
                p.PrimaryAction = "Ok";
                p.SecondaryAction = null;
                p.ShowDismiss = true;
            }
        );

        HandlePanel(_panel.Content);
    }

    public void HandlePanel(IViewData<Account>? result) { }
}
