using Microsoft.OData.ModelBuilder;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Contracts.Contacts;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts
{
    [Validator("ContactValidator")]
    [ViewSize(width: "400px", height: "650px")]
    public class Contact : DataObject, IContract
    {
        private string? _name;
        private string? _groupString;

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Name")]
        public virtual string? Name { get => _name ??= $"{Personal?.FirstName} {Personal?.LastName}"; set => _name = value; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Email")]
        public virtual string? Email { get => Personal?.Email; set => (Personal ??= new ContactPersonal()).Email = value!; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Phone number")]
        public virtual string? PhoneNumber { get => Personal?.PhoneNumber; set => (Personal ??= new ContactPersonal()).PhoneNumber = value!; }

        [VisibleRubric]
        [RubricSize(16)]
        [DisplayRubric("Contact type")]
        public virtual ContactType Type { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(128)]
        [DisplayRubric("Assigned groups")]
        public virtual string? GroupString
        {
            get
            {
                if (_groupString != null)
                    return _groupString;
                if (Groups != null && Groups.Any())
                    return _groupString = Groups.Select(g => g.Name).Aggregate((a, b) => a + ", " + b);
                return null;
            }
            set => _groupString = value;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Image")]
        [ViewImage(ViewImageMode.Persona, "30px", "30px")]
        [FileRubric(FileRubricType.Property, "PersonaImageData")]
        public string? PersonalImage { get => Personal?.PersonalImage; set => (Personal ??= new ContactPersonal()).PersonalImage = value!; }

        [JsonIgnore]
        [IgnoreDataMember]
        public byte[]? PersonaImageData { get => Personal?.PersonalImageData; set => (Personal ??= new ContactPersonal()).PersonalImageData = value!; }

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
