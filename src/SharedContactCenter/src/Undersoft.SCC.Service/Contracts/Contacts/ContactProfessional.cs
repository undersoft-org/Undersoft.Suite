using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Contacts;

public class ContactProfessional : DataObject, IContract
{
    public string? ProfessionIndustry { get; set; }

    public string? Profession { get; set; } = default!;

    public string? ProfessionalEmail { get; set; }

    public string? ProfessionalPhoneNumber { get; set; }

    public string? ProfessionalSocialMedia { get; set; }

    public string? ProfessionalWebsites { get; set; }

    public float? ProfessionalExperience { get; set; }

    public long? ContactId { get; set; }

}
