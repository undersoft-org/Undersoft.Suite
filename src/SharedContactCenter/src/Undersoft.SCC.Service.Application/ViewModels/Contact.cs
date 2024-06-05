using Microsoft.OData.ModelBuilder;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Application.ViewModels.Contacts;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Application.ViewModels
{
    [Validator("ContactValidator")]
    [ViewSize(width: "400px", height: "650px")]
    public class Contact : DataObject, IViewModel
    {
        private string? _name;
        private string? _address;

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

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [DisplayRubric("Name")]
        public virtual string? Name
        {
            get => _name ??= $"{Personal?.FirstName} {Personal?.LastName}";
            set => _name = value;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(64)]
        [DisplayRubric("Phone number")]
        public virtual string? PhoneNumber
        {
            get => Personal?.PhoneNumber;
            set => (Personal ??= new ContactPersonal()).PhoneNumber = value!;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(64)]
        [DisplayRubric("Email address")]
        public virtual string? Email
        {
            get => Personal?.Email;
            set => (Personal ??= new ContactPersonal()).Email = value!;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [Filterable]
        [Sortable]
        [VisibleRubric]
        [RubricSize(128)]
        [DisplayRubric("Contact address")]
        public virtual string? ContactAddress
        {
            get
            {
                if (_address != null)
                    return _address;
                if (Address != null)
                    return _address = string.Join(
                        '/',
                        (
                            string.Join(
                                ' ',
                                Address.Country,
                                Address.Postcode,
                                Address.City,
                                Address.Street,
                                Address.Building
                            ),
                            Address.Apartment
                        )
                    );
                Address = new ContactAddress();
                return null;
            }
            set => _address = value;
        }

        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(16)]
        [DisplayRubric("Contact type")]
        public virtual ContactType Type { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public byte[]? PersonaImageData
        {
            get => Personal?.PersonalImageData;
            set => (Personal ??= new ContactPersonal()).PersonalImageData = value!;
        }

        public virtual string? Notes { get; set; }

        public long? PersonalId { get; set; }

        [Extended]
        [AutoExpand]
        public virtual ContactPersonal? Personal { get; set; } = default!;

        public long? AddressId { get; set; }

        [Extended]
        [AutoExpand]
        public virtual ContactAddress? Address { get; set; } = default!;

        public long? ProfessionalId { get; set; }

        [Extended]
        [AutoExpand]
        public virtual ContactProfessional? Professional { get; set; } = default!;

        public long? OrganizationId { get; set; }

        [Extended]
        [AutoExpand]
        public virtual ContactOrganization? Organization { get; set; } = default!;

        [AutoExpand]
        public virtual Listing<Group>? Groups { get; set; } = default!;
    }
}
