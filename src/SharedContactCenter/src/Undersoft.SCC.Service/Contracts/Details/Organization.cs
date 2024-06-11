// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

namespace Undersoft.SCC.Service.Application.ViewModels;

using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Contracts.Settings;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;

/// <summary>
/// The contact organization.
/// </summary>
[Detail]
public class Organization : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the organization image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? OrganizationImage { get; set; }

    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Setting]
    public virtual Industry? Industry { get; set; }

    /// <summary>
    /// Gets or sets the organization name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? OrganizationName { get; set; }

    /// <summary>
    /// Gets or sets the organization full name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? OrganizationFullName { get; set; }

    /// <summary>
    /// Gets or sets the position in organization.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? PositionInOrganization { get; set; }

    /// <summary>
    /// Gets or sets the organization websites.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? OrganizationWebsites { get; set; }

    /// <summary>
    /// Gets or sets the organization size.
    /// </summary>
    /// <value>An <see cref="OrganizationSize"/></value>
    [Identify]
    public OrganizationSize OrganizationSize { get; set; }

    /// <summary>
    /// Gets or sets the organization image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? OrganizationImageData { get; set; }
}
