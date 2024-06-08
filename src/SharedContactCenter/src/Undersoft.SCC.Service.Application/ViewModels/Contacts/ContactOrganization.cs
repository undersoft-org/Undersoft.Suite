// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Contacts;

using Undersoft.SCC.Domain.Entities.Enums;

/// <summary>
/// The contact organization.
/// </summary>
public class ContactOrganization : DataObject, IViewModel
{
    /// <summary>
    /// Gets or sets the organization image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Organization logo")]
    [ViewImage(ViewImageMode.Regular, "20px", "00px")]
    [FileRubric(FileRubricType.Property, "OrganizationImageData")]
    public string? OrganizationImage { get; set; }

    /// <summary>
    /// Gets or sets the organization industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Industry")]
    public string? OrganizationIndustry { get; set; }

    /// <summary>
    /// Gets or sets the organization name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Short name")]
    public string? OrganizationName { get; set; }

    /// <summary>
    /// Gets or sets the organization full name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Full name")]
    public string? OrganizationFullName { get; set; }

    /// <summary>
    /// Gets or sets the position in organization.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Position")]
    public string? PositionInOrganization { get; set; }

    /// <summary>
    /// Gets or sets the organization websites.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Websites")]
    public string? OrganizationWebsites { get; set; }

    /// <summary>
    /// Gets or sets the organization size.
    /// </summary>
    /// <value>An <see cref="OrganizationSize"/></value>
    [DisplayRubric("Size")]
    public OrganizationSize OrganizationSize { get; set; }

    /// <summary>
    /// Gets or sets the organization image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? OrganizationImageData { get; set; }

    /// <summary>
    /// Gets or sets the contact id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? ContactId { get; set; }
}
