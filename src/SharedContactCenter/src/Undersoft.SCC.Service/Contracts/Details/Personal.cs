// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SCC.Service.Contracts.Details;

/// <summary>
/// The contact personal.
/// </summary>
[Detail]
public class Personal : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the personal image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? PersonalImage { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Identify]
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Identify]
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Identify]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Identify]
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets the birthdate.
    /// </summary>
    /// <value>A <see cref="DateTime"/></value>
    [Identify]
    public DateTime Birthdate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the personal image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? PersonalImageData { get; set; }
}
