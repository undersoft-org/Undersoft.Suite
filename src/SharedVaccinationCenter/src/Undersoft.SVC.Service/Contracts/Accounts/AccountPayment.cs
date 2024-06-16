// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************


// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SVC.Service.Contracts.Accounts;

public class AccountPayment : DataObject
{
    [VisibleRubric]
    [DisplayRubric("Card title")]
    public string? CardTitle { get; set; }

    [VisibleRubric]
    [DisplayRubric("Card number")]
    public string? CardNumber { get; set; }

    public string? CardType { get; set; }

    [VisibleRubric]
    [DisplayRubric("Expiration date")]
    public string? CardExpirationDate { get; set; }

    [VisibleRubric]
    [DisplayRubric("CSV")]
    public string? CardCSV { get; set; }

    [VisibleRubric]
    [DisplayRubric("First name")]
    public string? PaymentFirstName { get; set; }

    [VisibleRubric]
    [DisplayRubric("Last name")]
    public string? PaymentLastName { get; set; }

    public bool PaymentTermsConsent { get; set; }

    public string? PaymentType { get; set; }

    public string? PaymentPhoneNumber { get; set; }

    public string? PaymentImage { get; set; }

    public byte[]? PaymentImageData { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PaymentProvider { get; set; }

    public string? PaymentWebsites { get; set; }

    public long? AccountId { get; set; }
}
