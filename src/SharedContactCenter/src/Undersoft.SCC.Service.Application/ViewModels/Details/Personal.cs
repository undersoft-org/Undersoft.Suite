// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Details;

/// <summary>
/// The contact personal.
/// </summary>
public class Personal : DataObject, IViewModel
{

    /// <summary>
    /// Gets or sets the personal image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Personal image")]
    [FileRubric(FileRubricType.Path, "PersonalImageData")]
    public string? PersonalImage { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("First name")]
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Last name")]
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [VisibleRubric]
    [RequiredRubric]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Phone number")]
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets the birthdate.
    /// </summary>
    /// <value>A <see cref="DateTime"/></value>
    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Day of birth")]
    public DateTime Birthdate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the personal image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? PersonalImageData { get; set; }

}
