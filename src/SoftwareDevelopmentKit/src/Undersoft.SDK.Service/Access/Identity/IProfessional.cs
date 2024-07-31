namespace Undersoft.SDK.Service.Access.Identity
{
    public interface IProfessional
    {
        string Profession { get; set; }
        string ProfessionalEmail { get; set; }
        float ProfessionalExperience { get; set; }
        string ProfessionalPhoneNumber { get; set; }
        string ProfessionalSocialMedia { get; set; }
        string ProfessionalWebsites { get; set; }
        string ProfessionIndustry { get; set; }
    }
}