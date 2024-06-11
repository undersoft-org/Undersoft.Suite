// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

namespace Undersoft.SCC.Service.Contracts.Accounts;

public class AccountConsent : DataObject
{
    public string? TermsText { get; set; } = default!;

    public bool TermsConsent { get; set; } = default!;

    public string? PersonalDataText { get; set; } = default!;

    public bool PersonalDataConsent { get; set; } = default!;

    public string? MarketingText { get; set; } = default!;

    public bool MarketingConsent { get; set; } = default!;

    public string? ThirdPartyText { get; set; } = default!;

    public bool ThirdPartyConsent { get; set; } = default!;

    public long? AccountId { get; set; }

    public long? ConsentId { get; set; }

    public virtual Consent? Consent { get; set; }
}
