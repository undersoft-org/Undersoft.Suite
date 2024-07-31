namespace Undersoft.SDK.Service.Server.Accounts.Licensing;

public class AccountPayment : Access.Licensing.Payment
{
    public long? PaymentId { get; set; }
    public virtual Payment Payment { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
