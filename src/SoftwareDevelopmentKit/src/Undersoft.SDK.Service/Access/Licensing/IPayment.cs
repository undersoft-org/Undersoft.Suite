namespace Undersoft.SDK.Service.Access.Licensing
{
    public interface IPayment
    {
        string CardCSV { get; set; }
        string CardExpirationDate { get; set; }
        string CardNumber { get; set; }
        string CardTitle { get; set; }
        string CardType { get; set; }
        string PaymentFirstName { get; set; }
        string PaymentImage { get; set; }
        byte[] PaymentImageData { get; set; }
        string PaymentLastName { get; set; }
        string PaymentPhoneNumber { get; set; }
        string PaymentProvider { get; set; }
        string PaymentStatus { get; set; }
        bool PaymentTermsConsent { get; set; }
        string PaymentType { get; set; }
        string PaymentWebsites { get; set; }
    }
}