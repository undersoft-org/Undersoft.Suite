// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Countries;

/// <summary>
/// The country language.
/// </summary>
public class CountryLanguage : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the language code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? LanguageCode { get; set; }
}
