// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Application.ViewModels.Settings;

/// <summary>
/// The country state.
/// </summary>
public class CountryState : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the state code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Code")]
    public string? StateCode { get; set; }

    /// <summary>
    /// Gets or sets the time zone.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Time zone UTC")]
    public string? TimeZone { get; set; }
}
