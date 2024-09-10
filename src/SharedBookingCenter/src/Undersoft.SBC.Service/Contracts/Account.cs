using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SBC.Service.Contracts
{
    using Undersoft.SBC.Service.Contracts.Accounts;
    using Undersoft.SDK.Service.Access;
    using Undersoft.SDK.Service.Access.Identity;

    [Validator("AccountValidator")]
    [ViewSize("380px", "650px")]
    public class Account : Authorization, IContract
    {
        private string? _name;

        private string? _roleString;

        public Account() { }

        public Account(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email to account must be provided");
            Id = email.UniqueKey64();
            Email = email;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(8)]
        [DisplayRubric("Image")]
        [ViewImage(ViewImageMode.Persona, "30px", "30px")]
        [FileRubric(FileRubricType.Property, "ImageData")]
        public string? Image
        {
            get => Personal?.Image;
            set => (Personal ??= new AccountPersonal()).Image = value!;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Name")]
        public virtual string? Name
        {
            get => _name ??= $"{Personal?.FirstName} {Personal?.LastName}";
            set => _name = value;
        }

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

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Phone")]
        public virtual string? PhoneNumber
        {
            get => Personal?.PhoneNumber;
            set => (Personal ??= new AccountPersonal()).PhoneNumber = value!;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Roles")]
        public virtual string? RoleString
        {
            get
            {
                if (_roleString != null)
                    return _roleString;
                if (Roles != null && Roles.Any())
                    return _roleString = Roles
                        .Select(g => g.Name)
                        .Aggregate((a, b) => a + ", " + b);
                return null;
            }
            set => _roleString = value;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public byte[]? ImageData
        {
            get => Personal?.ImageData;
            set => (Personal ??= new AccountPersonal()).ImageData = value!;
        }

        public long? UserId { get; set; }

        public AccountUser? User { get; set; } = default!;

        [AutoExpand]
        public Listing<Role>? Roles { get; set; } = default!;

        [AutoExpand]
        public Listing<Claim>? Claims { get; set; } = default!;

        public long? PersonalId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountPersonal? Personal { get; set; } = default!;

        public long? AddressId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountAddress? Address { get; set; } = default!;

        public long? ProfessionalId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountProfessional? Professional { get; set; } = default!;

        public long? OrganizationId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountOrganization? Organization { get; set; } = default!;

        public long? ConsentId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountConsent? Consent { get; set; } = default!;

        public long? SubscriptionId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountSubscription? Subscription { get; set; } = default!;

        public long? PaymentId { get; set; }

        public virtual AccountPayment? Payment { get; set; } = default!;

        public long? TenantId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountTenant? Tenant { get; set; } = default!;
    }
}
