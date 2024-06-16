using System.Runtime.Serialization;
using System.Text.Json.Serialization;

// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Application.ViewModels;

using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Application.ViewModels.Details;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Query;

/// <summary>
/// The contact.
/// </summary>
[Validator("MemberValidator")]
[ViewSize(width: "400px", height: "650px")]
public class Member : OpenModel<Member, Detail, Setting, Group>, IViewModel
{
    private string? _name;
    private string? _address;

    /// <summary>
    /// Gets or sets the personal image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [RubricSize(3)]
    [DisplayRubric("Image")]
    [ViewImage(ViewImageMode.Persona, "20px", "20px")]
    [FileRubric(FileRubricType.Property, "PersonalImageData")]
    public string? PersonalImage
    {
        get => Personal?.PersonalImage;
        set => (Personal ??= new Personal()).PersonalImage = value!;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [JsonIgnore]
    [VisibleRubric]
    [RubricSize(64)]
    [Filterable]
    [Sortable]
    [Identify]
    [OpenQuery("Personal.LastName", "Personal.FirstName")]
    [DisplayRubric("Name")]
    public virtual string? Name
    {
        get => _name ??= $"{Personal?.FirstName} {Personal?.LastName}";
        set => _name = value;
    }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(64)]
    [Identify]
    [OpenQuery("Personal.PhoneNumber")]
    [DisplayRubric("Phone")]
    public virtual string? PhoneNumber
    {
        get => Personal?.PhoneNumber;
        set => (Personal ??= new Personal()).PhoneNumber = value!;
    }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(64)]
    [Identify]
    [OpenQuery("Personal.Email")]
    [DisplayRubric("Email")]
    public virtual string? Email
    {
        get => Personal?.Email;
        set => (Personal ??= new Personal()).Email = value!;
    }

    /// <summary>
    /// Gets or sets the contact address.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [Filterable]
    [Sortable]
    [VisibleRubric]
    [Identify]
    [RubricSize(128)]
    [OpenQuery(
        new string[]
        {
            "Address.Country.Name",
            "Address.City",
        },
        "Address.Country.Name",
        "Address.Postcode",
        "Address.City",
        "Address.Street",
        "Address.Building",
        "Address.Apartment"
    )]
    [DisplayRubric("Address")]
    public virtual string? ContactAddress
    {
        get
        {
            if (_address != null)
                return _address;
            if (Address != null)
                return _address = string.Join(
                    " ",
                    Address.CountryName,
                    (string?)Address.Postcode,
                    (string?)Address.City,
                    (string?)Address.Street,
                    (string?)Address.Building,
                    (string?)Address.Apartment
                );
            Address = new Address();
            return null;
        }
        set => _address = value;
    }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>A <see cref="MemberKind"/></value>
    [VisibleRubric]
    [Filterable]
    [RubricSize(16)]
    [DisplayRubric("Kind")]
    [OpenQuery("Kind")]
    public virtual MemberKind Kind { get; set; }

    /// <summary>
    /// Gets or sets the persona image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    [JsonIgnore]
    [IgnoreDataMember]
    public byte[]? PersonalImageData
    {
        get => Personal?.PersonalImageData;
        set => (Personal ??= new Personal()).PersonalImageData = value!;
    }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the personal.
    /// </summary>
    /// <value>A <see cref="Contacts.Personal? "/></value>
    [Extended]
    [Detail]
    public virtual Personal? Personal { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>A <see cref="ContactAddress? "/></value>
    [Extended]
    [Detail]
    public virtual Address? Address { get; set; }

    /// <summary>
    /// Gets or sets the professional.
    /// </summary>
    /// <value>A <see cref="Contacts.Professional? "/></value>
    [Extended]
    [Detail]
    public virtual Professional? Professional { get; set; }

    /// <summary>
    /// Gets or sets the organization.
    /// </summary>
    /// <value>A <see cref="ViewModels.Organization? "/></value>
    [Extended]
    [Detail]
    public virtual Organization? Organization { get; set; }
}
