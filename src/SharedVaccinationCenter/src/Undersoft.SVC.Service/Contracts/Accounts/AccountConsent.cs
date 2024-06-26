// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SVC.Service.Contracts.Accounts
{
    public class AccountConsent : DataObject
    {
        [VisibleRubric]
        [RubricSize(512)]
        [DisplayRubric("Terms & Conditions")]
        public string? TermsText { get; set; }

        [VisibleRubric]
        [RubricSize(4)]
        [DisplayRubric("Accept")]
        public bool TermsConsent { get; set; } = default!;

        public string? PersonalDataText { get; set; }

        public bool PersonalDataConsent { get; set; } = default!;

        [VisibleRubric]
        [RubricSize(512)]
        [DisplayRubric("Marketing & Analitics")]
        public string? MarketingText { get; set; }

        [VisibleRubric]
        [RubricSize(4)]
        [DisplayRubric("Accept")]
        public bool MarketingConsent { get; set; } = default!;

        [VisibleRubric]
        [RubricSize(512)]
        [DisplayRubric("Third party & Contributors")]
        public string? ThirdPartyText { get; set; }

        [VisibleRubric]
        [RubricSize(4)]
        [DisplayRubric("Accept")]
        public bool ThirdPartyConsent { get; set; } = default!;

        public long? AccountId { get; set; }

        public long? ConsentId { get; set; }

        public virtual Consent? Consent { get; set; }
    }
}