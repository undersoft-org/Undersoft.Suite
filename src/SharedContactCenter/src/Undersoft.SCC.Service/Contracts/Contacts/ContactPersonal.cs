// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Contacts;

/// <summary>
/// The contact personal.
/// </summary>
public class ContactPersonal : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets the birthdate.
    /// </summary>
    /// <value>A <see cref="DateTime"/></value>
    public DateTime Birthdate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the personal image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? PersonalImage { get; set; }

    /// <summary>
    /// Gets or sets the personal image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? PersonalImageData { get; set; }

    /// <summary>
    /// Gets or sets the contact id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? ContactId { get; set; }

}
