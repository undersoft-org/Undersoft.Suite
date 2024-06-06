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
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts;

using Undersoft.SCC.Service.Contracts.Accounts;

/// <summary>
/// The account.
/// </summary>
[Validator("AccountValidator")]
[ViewSize("400px", "650px")]
public class Account : Authorization, IContract
{
    /// <summary>
    /// Name.
    /// </summary>
    private string? _name;

    /// <summary>
    /// The role string.
    /// </summary>
    private string? _roleString;

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class.
    /// </summary>
    public Account() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class.
    /// </summary>
    /// <param name="email">The email.</param>
    public Account(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException("email to account must be provided");
        Id = email.UniqueKey64();
        Email = email;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [JsonIgnore]
    [VisibleRubric]
    [RubricSize(64)]
    [DisplayRubric("Name")]
    public virtual string? Name
    {
        get => _name ??= $"{Personal?.FirstName} {Personal?.LastName}";
        set => _name = value;
    }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [RubricSize(64)]
    [DisplayRubric("Email")]
    public virtual string? Email
    {
        get => Personal?.Email;
        set => (Personal ??= new AccountPersonal()).Email = value!;
    }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [RubricSize(64)]
    [DisplayRubric("Phone number")]
    public virtual string? PhoneNumber
    {
        get => Personal?.PhoneNumber;
        set => (Personal ??= new AccountPersonal()).PhoneNumber = value!;
    }

    /// <summary>
    /// Gets or sets the role string.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [RubricSize(128)]
    [DisplayRubric("Assigned roles")]
    public virtual string? RoleString
    {
        get
        {
            if (_roleString != null)
                return _roleString;
            if (Roles != null && Roles.Any())
                return _roleString = Roles.Select(g => g.Name).Aggregate((a, b) => a + ", " + b);
            return null;
        }
        set => _roleString = value;
    }

    /// <summary>
    /// Gets or sets the image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Image")]
    [ViewImage(ViewImageMode.Persona, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "ImageData")]
    public string? Image
    {
        get => Personal?.Image;
        set => (Personal ??= new AccountPersonal()).Image = value!;
    }

    /// <summary>
    /// Gets or sets the image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    [JsonIgnore]
    [IgnoreDataMember]
    public byte[]? ImageData
    {
        get => Personal?.ImageData;
        set => (Personal ??= new AccountPersonal()).ImageData = value!;
    }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    /// <value>An <see cref="AccountUser? "/></value>
    public AccountUser? User { get; set; } = default!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    /// <value>A TODO: Add missing XML "/&gt;</value>
    [AutoExpand]
    public Listing<Role>? Roles { get; set; } = default!;

    /// <summary>
    /// Gets or sets the claims.
    /// </summary>
    /// <value>A TODO: Add missing XML "/&gt;</value>
    [AutoExpand]
    public Listing<Claim>? Claims { get; set; } = default!;

    /// <summary>
    /// Gets or sets the personal id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? PersonalId { get; set; }

    /// <summary>
    /// Gets or sets the personal.
    /// </summary>
    /// <value>An <see cref="AccountPersonal"/></value>
    [AutoExpand]
    [Extended]
    public virtual AccountPersonal Personal { get; set; } = default!;

    /// <summary>
    /// Gets or sets the address id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? AddressId { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>An <see cref="AccountAddress"/></value>
    [AutoExpand]
    [Extended]
    public virtual AccountAddress Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the professional id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? ProfessionalId { get; set; }

    /// <summary>
    /// Gets or sets the professional.
    /// </summary>
    /// <value>An <see cref="AccountProfessional"/></value>
    [AutoExpand]
    [Extended]
    public virtual AccountProfessional Professional { get; set; } = default!;

    /// <summary>
    /// Gets or sets the organization id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the organization.
    /// </summary>
    /// <value>An <see cref="AccountOrganization"/></value>
    [AutoExpand]
    [Extended]
    public virtual AccountOrganization Organization { get; set; } = default!;
}
