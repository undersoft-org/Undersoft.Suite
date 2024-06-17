// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Domain.Entities;
/// <summary>
/// The contact organization.
/// </summary>
public class Organization : Entity
{
    public IdentifierType OrganizatioIdentifierType { get; set; }

    public string? OrganizatioIdentifier { get; set; }

    public string? OrganizatioIndustry { get; set; }

    public string? OrganizationName { get; set; }

    public string? OrganizationFullName { get; set; }

    public string? OrganizationEmail { get; set; }

    public string? OrganizationPhoneNumber { get; set; }

    public string? OrganizationWebsites { get; set; }

    public OrganizationSize OrganizationSize { get; set; }

    public string? OrganizationImage { get; set; }

    public byte[]? OrganizationImageData { get; set; }

    public long? SupplierId { get; set; }

    public virtual Supplier? Supplier { get; set; }

}
