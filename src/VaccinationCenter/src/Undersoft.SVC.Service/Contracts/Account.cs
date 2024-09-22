using Microsoft.OData.ModelBuilder;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SVC.Service.Contracts
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Service.Access;
    using Undersoft.SDK.Service.Access.Identity;
    using Undersoft.SVC.Service.Contracts.Accounts;

    [Validator("AccountValidator")]
    [ViewSize("380px", "650px")]
    [StructLayout(LayoutKind.Sequential)]
    public class Account : Authorization, IContract
    {
        private string? _name;

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

        [JsonIgnore]
        [IgnoreDataMember]
        public byte[]? ImageData
        {
            get => Personal?.ImageData;
            set => (Personal ??= new AccountPersonal()).ImageData = value!;
        }

        public long? UserId { get; set; }

        public virtual AccountUser? User { get; set; }

        [AutoExpand]
        public Listing<Role>? Roles { get; set; } 

        public Listing<Claim>? Claims { get; set; }

        public long? PersonalId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountPersonal? Personal { get; set; }

        public long? AddressId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountAddress? Address { get; set; } 

        public long? ProfessionalId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountProfessional? Professional { get; set; } 

        public long? OrganizationId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountOrganization? Organization { get; set; }

        public long? ConsentId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountConsent? Consent { get; set; } 

        public long? SubscriptionId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountSubscription? Subscription { get; set; }

        public long? PaymentId { get; set; }

        public virtual AccountPayment? Payment { get; set; }

        public long? TenantId { get; set; }

        [AutoExpand]
        [Extended]
        public virtual AccountTenant? Tenant { get; set; }
    }
}
