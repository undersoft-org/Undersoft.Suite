// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts.Accounts;

public class Consent : DataObject, IContract
{
    [VisibleRubric]
    [RubricSize(128)]
    [DisplayRubric("Terms & Conditions")]
    public string? TermsText { get; set; }

    public string? PersonalDataText { get; set; }

    [VisibleRubric]
    [RubricSize(128)]
    [DisplayRubric("Marketing & Analitics")]
    public string? MarketingText { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Third party & Contributors")]
    public string? ThirdPartyText { get; set; }
}
