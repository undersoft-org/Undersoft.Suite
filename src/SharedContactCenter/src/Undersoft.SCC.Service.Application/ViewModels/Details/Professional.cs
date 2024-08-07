﻿// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

using Microsoft.OData.ModelBuilder;
using Undersoft.SCC.Service.Application.ViewModels.Settings;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Details;

/// <summary>
/// The contact professional.
/// </summary>
public class Professional : DataObject, IViewModel
{
    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Industry")]
    public string? ProfessionIndustry { get; set; }

    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Extended]
    [AutoExpand]
    public virtual Industry? Industry { get; set; }

    /// <summary>
    /// Gets or sets the profession.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Profession")]
    public string? ProfessionName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Extended]
    [AutoExpand]
    public virtual Profession? Profession { get; set; }

    /// <summary>
    /// Gets or sets the professional email.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Email")]
    public string? ProfessionalEmail { get; set; }

    /// <summary>
    /// Gets or sets the professional phone number.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Phone number")]
    public string? ProfessionalPhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the professional social media.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Social media")]
    public string? ProfessionalSocialMedia { get; set; }

    /// <summary>
    /// Gets or sets the professional websites.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Websites")]
    public string? ProfessionalWebsites { get; set; }

    /// <summary>
    /// Gets or sets the professional experience.
    /// </summary>
    /// <value>A <see cref="float? "/></value>
    [VisibleRubric]
    [DisplayRubric("Experience in years")]
    public float? ProfessionalExperience { get; set; }

}
