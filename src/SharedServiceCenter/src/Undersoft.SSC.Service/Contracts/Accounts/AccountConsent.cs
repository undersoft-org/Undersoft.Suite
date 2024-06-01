using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SSC.Service.Contracts.Accounts;

public class AccountConsent : DataObject
{
    [VisibleRubric]
    public string? TermsText { get; set; }

    [VisibleRubric]
    public bool TermsConsent { get; set; }

    [VisibleRubric]
    public string? PersonalDataText { get; set; }

    [VisibleRubric]
    public bool PersonalDataConsent { get; set; }

    [VisibleRubric]
    public string? MarketingText { get; set; }

    [VisibleRubric]
    public bool MarketingConsent { get; set; }

    [VisibleRubric]
    public string? ThirdPartyText { get; set; }

    [VisibleRubric]
    public bool ThirdPartyConsent { get; set; }

    public long? AccountId { get; set; }
}
