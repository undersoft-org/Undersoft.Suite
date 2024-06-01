namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountConsent : DataObject
{
    public string TermsText { get; set; }

    public bool TermsConsent { get; set; }

    public string PersonalDataText { get; set; }

    public bool PersonalDataConsent { get; set; }

    public string MarketingText { get; set; }

    public bool MarketingConsent { get; set; }

    public string ThirdPartyText { get; set; }

    public bool ThirdPartyConsent { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
