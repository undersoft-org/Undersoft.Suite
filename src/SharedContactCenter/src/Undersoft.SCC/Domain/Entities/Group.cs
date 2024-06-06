// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

namespace Undersoft.SCC.Domain.Entities;

/// <summary>
/// The group.
/// </summary>
public partial class Group : Entity
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Name { get; set; }

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
    /// Gets or sets the contacts.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Contact>? Contacts { get; set; }
}
