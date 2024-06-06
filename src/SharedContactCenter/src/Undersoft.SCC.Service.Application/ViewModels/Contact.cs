using Microsoft.OData.ModelBuilder;
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
using Undersoft.SCC.Service.Application.ViewModels.Contacts;

/// <summary>
/// The contact.
/// </summary>
[Validator("ContactValidator")]
[ViewSize(width: "400px", height: "650px")]
public class Contact : DataObject, IViewModel
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
    [FileRubric(FileRubricType.Property, "PersonaImageData")]
    public string? PersonalImage
    {
        get => Personal?.PersonalImage;
        set => (Personal ??= new ContactPersonal()).PersonalImage = value!;
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
    [ViewQuery("Personal.FirstName", "Personal.LastName")]
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
    [ViewQuery("Personal.PhoneNumber")]
    [DisplayRubric("Phone number")]
    public virtual string? PhoneNumber
    {
        get => Personal?.PhoneNumber;
        set => (Personal ??= new ContactPersonal()).PhoneNumber = value!;
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
    [ViewQuery("Personal.Email")]
    [DisplayRubric("Email address")]
    public virtual string? Email
    {
        get => Personal?.Email;
        set => (Personal ??= new ContactPersonal()).Email = value!;
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
    [RubricSize(128)]
    [ViewQuery(
        "Address.Country",
        "Address.Postcode",
        "Address.City",
        "Address.Street",
        "Address.Country",
        "Address.Country"
    )]
    [DisplayRubric("Contact address")]
    public virtual string? ContactAddress
    {
        get
        {
            if (_address != null)
                return _address;
            if (Address != null)
                return _address = string.Join(
                    " ",
                    Address.Country,
                    Address.Postcode,
                    Address.City,
                    Address.Street,
                    Address.Building,
                    Address.Apartment
                );
            Address = new ContactAddress();
            return null;
        }
        set => _address = value;
    }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>A <see cref="ContactType"/></value>
    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(16)]
    [DisplayRubric("Contact type")]
    public virtual ContactType Type { get; set; }

    /// <summary>
    /// Gets or sets the persona image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    [JsonIgnore]
    [IgnoreDataMember]
    public byte[]? PersonaImageData
    {
        get => Personal?.PersonalImageData;
        set => (Personal ??= new ContactPersonal()).PersonalImageData = value!;
    }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the personal id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? PersonalId { get; set; }

    /// <summary>
    /// Gets or sets the personal.
    /// </summary>
    /// <value>A <see cref="ContactPersonal? "/></value>
    [Extended]
    [AutoExpand]
    public virtual ContactPersonal? Personal { get; set; } = default!;

    /// <summary>
    /// Gets or sets the address id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? AddressId { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>A <see cref="ContactAddress? "/></value>
    [Extended]
    [AutoExpand]
    public virtual ContactAddress? Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the professional id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? ProfessionalId { get; set; }

    /// <summary>
    /// Gets or sets the professional.
    /// </summary>
    /// <value>A <see cref="ContactProfessional? "/></value>
    [Extended]
    [AutoExpand]
    public virtual ContactProfessional? Professional { get; set; } = default!;

    /// <summary>
    /// Gets or sets the organization id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the organization.
    /// </summary>
    /// <value>A <see cref="ContactOrganization? "/></value>
    [Extended]
    [AutoExpand]
    public virtual ContactOrganization? Organization { get; set; } = default!;

    /// <summary>
    /// Gets or sets the groups.
    /// </summary>
    /// <value>A TODO: Add missing XML "/&gt;</value>
    [AutoExpand]
    public virtual Listing<Group>? Groups { get; set; } = default!;
}
