// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Access.Identity;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Application.ViewModels;
/// <summary>
/// The contact organization.
/// </summary>
public class Organization : DataObject, IViewModel
{
    [VisibleRubric]
    [RubricSize(16)]
    [DisplayRubric("Image")]
    [ViewImage(ViewImageMode.Regular, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "CompanyImageData")]
    public string? OrganizationImage { get; set; }

    [VisibleRubric]
    [RubricSize(8)]
    [DisplayRubric("Identifier type")]
    public IdentifierType OrganizatioIdentifierType { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Identifier")]
    public string? OrganizatioIdentifier { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Industry")]
    public string? OrganizatioIndustry { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Name")]
    public string? OrganizationName { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Full name")]
    public string? OrganizationFullName { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Email")]
    public string? OrganizationEmail { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Phone")]
    public string? OrganizationPhoneNumber { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Websites")]
    public string? OrganizationWebsites { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Size")]
    public OrganizationSize OrganizationSize { get; set; }

    public byte[]? OrganizationImageData { get; set; }

    public long? SupplierId { get; set; }
}
