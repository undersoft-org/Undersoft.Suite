namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountProfessional : DataObject
{
    public string ProfessionalEmail { get; set; }

    public string ProfessionalPhoneNumber { get; set; }

    public string Profession { get; set; }

    public string ProfessionIndustry { get; set; }

    public string ProfessionalSocialMedia { get; set; }

    public string ProfessionalWebsites { get; set; }

    public float ProfessionalExperience { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
