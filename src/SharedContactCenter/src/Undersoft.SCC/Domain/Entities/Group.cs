// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SDK.Service.Data.Object.Group;

namespace Undersoft.SCC.Domain.Entities;

/// <summary>
/// The group.
/// </summary>
public partial class Group : ObjectGroup<Group>, IGroup, IEntity
{

    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets or sets the group image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? GroupImage { get; set; } = default!;

    /// <summary>
    /// Gets or sets the group image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? GroupImageData { get; set; } = default!;

    /// <summary>
    /// Gets or sets the related from.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Group>? RelatedFrom { get; set; }

    /// <summary>
    /// Gets or sets the related converts to.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Group>? RelatedTo { get; set; }

    /// <summary>
    /// Gets or sets the contacts.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Contact>? Contacts { get; set; }
}
