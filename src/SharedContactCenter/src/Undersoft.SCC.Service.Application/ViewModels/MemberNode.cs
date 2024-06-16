// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
// *************************************************

namespace Undersoft.SCC.Service.Application.ViewModels;
using Undersoft.SDK.Service.Data.Entity;

/// <summary>
/// The contact.
/// </summary>
public class MemberNode : Member
{
    /// <summary>
    /// Gets or sets the related from.
    /// </summary>
    public virtual EntitySet<Member>? RelatedFrom { get; set; }

    /// <summary>
    /// Gets or sets the related converts to.
    /// </summary>
    public virtual EntitySet<Member>? RelatedTo { get; set; }
}
