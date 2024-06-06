using Microsoft.OData.ModelBuilder;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts;

using Undersoft.SCC.Service.Contracts.Countries;

/// <summary>
/// The country.
/// </summary>
[Validator("CountryValidator")]
[ViewSize(width: "400px", height: "700px")]
public partial class Country : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the country image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [RubricSize(3)]
    [DisplayRubric("Flag")]
    [ViewImage(ViewImageMode.Regular, "30px", "20px")]
    [FileRubric(FileRubricType.Property, "CountryImageData")]
    public string? CountryImage { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [RubricSize(64)]
    [Filterable]
    [Sortable]
    [DisplayRubric("Country Name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the country code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [RubricSize(8)]
    [Filterable]
    [Sortable]
    [DisplayRubric("Country code")]
    public string? CountryCode { get; set; }

    /// <summary>
    /// Gets or sets the continent.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [RubricSize(64)]
    [Filterable]
    [Sortable]
    [DisplayRubric("Continent")]
    public string? Continent { get; set; }

    /// <summary>
    /// Gets or sets the time zone.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    [RubricSize(8)]
    [Filterable]
    [Sortable]
    [DisplayRubric("Time zone UTC")]
    public string? TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the currency code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(32)]
    [DisplayRubric("Currency code")]
    public string? CurrencyCode { get => Currency?.CurrencyCode; set => (Currency ??= new Currency()).CurrencyCode = value!; }

    /// <summary>
    /// Gets or sets the language name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(32)]
    [DisplayRubric("Language")]
    public string? LanguageName { get => Language?.Name; set => (Language ??= new CountryLanguage()).Name = value!; }

    /// <summary>
    /// Gets or sets the country image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? CountryImageData { get; set; } = default!;

    /// <summary>
    /// Gets or sets the currency id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? CurrencyId { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    /// <value>A <see cref="Currency? "/></value>
    [Extended]
    [AutoExpand]
    public virtual Currency? Currency { get; set; }

    /// <summary>
    /// Gets or sets the language id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? LanguageId { get; set; }

    /// <summary>
    /// Gets or sets the language.
    /// </summary>
    /// <value>A <see cref="CountryLanguage? "/></value>
    [Extended]
    [AutoExpand]
    public virtual CountryLanguage? Language { get; set; }

    /// <summary>
    /// Gets or sets the states.
    /// </summary>
    /// <value>A TODO: Add missing XML "/&gt;</value>
    [Extended]
    [AutoExpand]
    public virtual Listing<CountryState>? States { get; set; }

}
