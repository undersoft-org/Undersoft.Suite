// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts.Accounts;

/// <summary>
/// The role.
/// </summary>
public class Role : InnerProxy, IRole, IContract
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets or sets the normalized name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual string? NormalizedName { get; set; }

    /// <summary>
    /// Gets or sets the claims.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public ObjectSet<Claim>? Claims { get; set; }
}
