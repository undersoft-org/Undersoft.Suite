// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Accounts;

/// <summary>
/// The claim.
/// </summary>
public class Claim : InnerProxy, IClaim, IContract
{
    /// <summary>
    /// Gets or sets the claim type.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual string? ClaimType { get; set; }

    /// <summary>
    /// Gets or sets the claim value.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual string? ClaimValue { get; set; }
}
