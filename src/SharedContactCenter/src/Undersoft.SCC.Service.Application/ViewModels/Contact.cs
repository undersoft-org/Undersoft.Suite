using System.Runtime.Serialization;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Contracts.Contacts;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels
{
    [DataContract]
    public class Contact : DataObject, IViewModel
    {
        private string? _name;

        [VisibleRubric]
        public virtual string? Name { get => _name ??= $"{Personal?.FirstName} {Personal?.LastName}"; set => _name = value; }

        [VisibleRubric]
        public virtual string? Email { get => Personal?.Email; set => Personal!.Email = value!; }

        [VisibleRubric]
        [DisplayRubric("Phone number")]
        public virtual string? PhoneNumber { get => Personal?.PhoneNumber; set => Personal!.PhoneNumber = value!; }

        [VisibleRubric]
        public virtual string? Notes { get; set; }

        [VisibleRubric]
        public virtual ContactType Type { get; set; }

        [VisibleRubric]
        [DisplayRubric("Image")]
        [FileRubric(FileRubricType.Path, "PersonaImageData")]
        public string? PersonalImage { get => (Personal ??= new ContactPersonal()).PersonalImage; set => (Personal ??= new ContactPersonal()).PersonalImage = value!; }

        public byte[]? PersonaImageData { get => (Personal ??= new ContactPersonal()).PersonalImageData; set => (Personal ??= new ContactPersonal()).PersonalImageData = value!; }

        [DataMember(Order = 16)]
        public long? PersonalId { get; set; }

        [DataMember(Order = 17)]
        public virtual ContactPersonal? Personal { get; set; } = default!;

        [DataMember(Order = 18)]
        public long? AddressId { get; set; }

        [DataMember(Order = 19)]
        public virtual ContactAddress? Address { get; set; } = default!;

        [DataMember(Order = 20)]
        public long? ProfessionalId { get; set; }

        [DataMember(Order = 21)]
        public virtual ContactProfessional? Professional { get; set; } = default!;

        [DataMember(Order = 22)]
        public long? OrganizationId { get; set; }

        [DataMember(Order = 23)]
        public virtual ContactOrganization? Organization { get; set; } = default!;

        [DataMember(Order = 24)]
        public virtual Listing<Group>? Groups { get; set; } = default!;
    }

}
