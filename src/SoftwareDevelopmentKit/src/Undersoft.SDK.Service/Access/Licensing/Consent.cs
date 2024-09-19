using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Licensing;

public class Consent : DataObject, IConsent
{
    [VisibleRubric]
    [RubricSize(512)]
    [DisplayRubric("Terms & Conditions")]
    public string TermsText { get; set; }

    [VisibleRubric]
    [DisplayRubric("Accept")]
    public bool TermsConsent { get; set; }

    public string PersonalDataText { get; set; }

    public bool PersonalDataConsent { get; set; }

    [VisibleRubric]
    [RubricSize(512)]
    [DisplayRubric("Marketing & Analitics")]
    public string MarketingText { get; set; }

    [VisibleRubric]
    [DisplayRubric("Accept")]
    public bool MarketingConsent { get; set; }

    [VisibleRubric]
    [RubricSize(512)]
    [DisplayRubric("Third party & Contributors")]
    public string ThirdPartyText { get; set; }

    [VisibleRubric]
    [DisplayRubric("Accept")]
    public bool ThirdPartyConsent { get; set; }
}
