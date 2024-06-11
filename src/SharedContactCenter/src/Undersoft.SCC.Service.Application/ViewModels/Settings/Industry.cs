// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

namespace Undersoft.SCC.Service.Application.ViewModels.Settings;

/// <summary>
/// The contact professional.
/// </summary>
public class Industry : DataObject, IViewModel
{
    /// <summary>
    /// Gets or sets the profession.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Name { get; set; } = default!;

    public virtual string? Description { get; set; }

}
