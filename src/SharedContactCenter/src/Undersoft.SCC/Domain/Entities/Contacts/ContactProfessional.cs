namespace Undersoft.SCC.Domain.Entities.Contacts;

public class ContactProfessional : Entity
{
    public string? ProfessionIndustry { get; set; }

    public string? Profession { get; set; } = default!;

    public string? ProfessionalEmail { get; set; }

    public string? ProfessionalPhoneNumber { get; set; }

    public string? ProfessionalSocialMedia { get; set; }

    public string? ProfessionalWebsites { get; set; }

    public float? ProfessionalExperience { get; set; }

    public long? ContactId { get; set; }
    public virtual Contact? Contact { get; set; }

}
