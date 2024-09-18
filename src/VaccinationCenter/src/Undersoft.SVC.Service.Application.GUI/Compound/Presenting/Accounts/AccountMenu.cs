// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Presenting.Accounts;

/// <summary>
/// The account menu.
/// </summary>
public class AccountMenu : DataObject
{
    /// <summary>
    /// Gets or sets the account.
    /// </summary>
    /// <value>An <see cref="AccountMenuItems"/></value>
    [MenuGroup]
    [Extended]
    public AccountMenuItems Account { get; set; } = new AccountMenuItems();
}

