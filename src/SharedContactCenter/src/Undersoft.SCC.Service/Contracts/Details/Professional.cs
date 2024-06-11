// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

using Undersoft.SCC.Service.Contracts.Settings;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SCC.Service.Contracts.Detailsl;

/// <summary>
/// The contact professional.
/// </summary>
[Detail]
public class Professional : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Setting]
    public virtual Industry? Industry { get; set; }

    /// <summary>
    /// Gets or sets the profession.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? ProfessionName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the profession industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Setting]
    public virtual Profession? Profession { get; set; }

    /// <summary>
    /// Gets or sets the professional email.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? ProfessionalEmail { get; set; }

    /// <summary>
    /// Gets or sets the professional phone number.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
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
    [Identify]
    public float? ProfessionalExperience { get; set; }

}
