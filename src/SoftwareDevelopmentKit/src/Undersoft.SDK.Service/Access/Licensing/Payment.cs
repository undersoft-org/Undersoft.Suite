using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Licensing;

public class Payment : DataObject, IPayment
{
    [VisibleRubric]
    [DisplayRubric("Card title")]
    public string CardTitle { get; set; }

    [VisibleRubric]
    [DisplayRubric("Card number")]
    public string CardNumber { get; set; }

    public string CardType { get; set; }

    [VisibleRubric]
    [DisplayRubric("Expiration date")]
    public string CardExpirationDate { get; set; }

    [VisibleRubric]
    [DisplayRubric("CSV")]
    public string CardCSV { get; set; }

    [VisibleRubric]
    [DisplayRubric("First name")]
    public string PaymentFirstName { get; set; }

    [VisibleRubric]
    [DisplayRubric("Last name")]
    public string PaymentLastName { get; set; }

    public bool PaymentTermsConsent { get; set; }

    public string PaymentType { get; set; }

    public string PaymentPhoneNumber { get; set; }

    public string PaymentImage { get; set; }

    public byte[] PaymentImageData { get; set; }

    public string PaymentStatus { get; set; }

    public string PaymentProvider { get; set; }

    public string PaymentWebsites { get; set; }
}
