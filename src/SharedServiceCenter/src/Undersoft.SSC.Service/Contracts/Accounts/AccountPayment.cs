using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SSC.Service.Contracts.Accounts;

public class AccountPayment : DataObject
{
    [VisibleRubric]
    public string? CardTitle { get; set; }

    [VisibleRubric]
    public string? CardNumber { get; set; }

    [VisibleRubric]
    public string? CardType { get; set; }

    [VisibleRubric]
    public string? CardExpirationDate { get; set; }

    [VisibleRubric]
    public string? CardCSV { get; set; }

    [VisibleRubric]
    public string? PaymentFirstName { get; set; }

    [VisibleRubric]
    public string? PaymentLastName { get; set; }

    public bool PaymentTermsConsent { get; set; }

    [VisibleRubric]
    public string? PaymentType { get; set; }

    public string? PaymentPhoneNumber { get; set; }

    public string? PaymentImage { get; set; }

    public byte[]? PaymentImageData { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PaymentProvider { get; set; }

    public string? PaymentWebsites { get; set; }

    public long? AccountId { get; set; }
}
