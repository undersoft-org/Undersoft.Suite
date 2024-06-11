// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Application.ViewModels.Countries;

/// <summary>
/// The currency.
/// </summary>
public partial class Currency : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the currency code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [DisplayRubric("Code")]
    public string? CurrencyCode { get; set; }
}
