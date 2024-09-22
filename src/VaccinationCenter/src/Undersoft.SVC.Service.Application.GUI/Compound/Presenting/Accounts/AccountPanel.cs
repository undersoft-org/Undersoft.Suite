using Microsoft.FluentUI.AspNetCore.Components;
// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Access;

using Undersoft.SDK.Service.Application.GUI.View.Generic.Accounts;
using Undersoft.SVC.Service.Contracts;

public class AccountPanel
{
    public AccountPanel() { }

    public async Task Open(IViewPanel<Account> panel)
    {
        IViewData<Account> data;
        if (panel.Content != null)
            data = panel.Content;
        else
            data = new ViewData<Account>(new Account(), OperationType.Any);

        data.EntryMode = EntryMode.Tabs;
        data.Width = "390px";

        await panel.Show(
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

        HandlePanel(panel);
    }

    /// <summary>
    /// </summary>
    /// <param name="result"></param>
    /// <TODO> Handle saving account panel</TODO>
    public void HandlePanel(IViewPanel<Account> panel)
    {
        if (panel.Content != null && panel.Reference != null)
            ((GenericAccountPanel<Account, AccountValidator>)panel.Reference).Access.Register(
                panel.Content.Model
            );
    }
}
