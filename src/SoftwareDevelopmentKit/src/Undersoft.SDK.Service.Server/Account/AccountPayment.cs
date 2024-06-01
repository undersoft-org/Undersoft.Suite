namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountPayment : DataObject
{
    public string CardTitle { get; set; }

    public string CardNumber { get; set; }

    public string CardType { get; set; }

    public string CardExpirationDate { get; set; }

    public string CardCSV { get; set; }

    public string PaymentFirstName { get; set; }

    public string PaymentLastName { get; set; }

    public bool PaymentTermsConsent { get; set; }

    public string PaymentType { get; set; }

    public string PaymentPhoneNumber { get; set; }

    public string PaymentImage { get; set; }

    public byte[] PaymentImageData { get; set; }

    public string PaymentStatus { get; set; }

    public string PaymentProvider { get; set; }

    public string PaymentWebsites { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
