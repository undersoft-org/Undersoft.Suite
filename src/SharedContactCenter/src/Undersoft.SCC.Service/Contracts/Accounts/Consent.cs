// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Accounts;

public class Consent : DataObject, IContract
{
    public string? TermsText { get; set; }

    public string? PersonalDataText { get; set; }

    public string? MarketingText { get; set; }

    public string? ThirdPartyText { get; set; }
}
