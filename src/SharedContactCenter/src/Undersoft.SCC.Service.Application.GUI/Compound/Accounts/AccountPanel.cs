using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Access;

public class AccountPanel
{
    public AccountPanel() { }

    public async Task Open(IViewPanel<Account> _panel)
    {
        IViewData<Account> data;
        if (_panel.Content != null)
            data = _panel.Content;
        else
            data = new ViewData<Account>(new Account(), OperationType.Any);

        data.EntryMode = EntryMode.Tabs;
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
