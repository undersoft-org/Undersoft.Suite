namespace Undersoft.SDK.Service.Server.Accounts;

public class Consent : DataObject
{
    public string TermsText { get; set; }

    public string PersonalDataText { get; set; }

    public string MarketingText { get; set; }

    public string ThirdPartyText { get; set; }

    public virtual EntitySet<AccountConsent> AccountConsents { get; set; }
}
