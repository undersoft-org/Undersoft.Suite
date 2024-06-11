// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

namespace Undersoft.SCC.Domain.Entities.Contacts;

/// <summary>
/// The contact professional.
/// </summary>
public class ContactProfessional : Entity
{
    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? ProfessionIndustry { get; set; }

    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public long? IndustryId { get; set; }

    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual Industry? Industry { get; set; }

    /// <summary>
    /// Gets or sets the profession.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? ProfessionName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public long? ProfessionId { get; set; }

    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual Profession? Profession { get; set; }

    /// <summary>
    /// Gets or sets the professional email.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? ProfessionalEmail { get; set; }

    /// <summary>
    /// Gets or sets the professional phone number.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? ProfessionalPhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the professional social media.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? ProfessionalSocialMedia { get; set; }

    /// <summary>
    /// Gets or sets the professional websites.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? ProfessionalWebsites { get; set; }

    /// <summary>
    /// Gets or sets the professional experience.
    /// </summary>
    /// <value>A <see cref="float? "/></value>
    public float? ProfessionalExperience { get; set; }

    /// <summary>
    /// Gets or sets the contact id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? ContactId { get; set; }
    /// <summary>
    /// Gets or sets the contact.
    /// </summary>
    /// <value>A <see cref="Contact? "/></value>
    public virtual Contact Contact { get; set; } = default!;

}
