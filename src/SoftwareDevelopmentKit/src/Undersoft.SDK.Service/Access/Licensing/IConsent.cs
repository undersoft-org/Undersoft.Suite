namespace Undersoft.SDK.Service.Access.Licensing
{
    public interface IConsent
    {
        string MarketingText { get; set; }
        string PersonalDataText { get; set; }
        string TermsText { get; set; }
        string ThirdPartyText { get; set; }
    }
}