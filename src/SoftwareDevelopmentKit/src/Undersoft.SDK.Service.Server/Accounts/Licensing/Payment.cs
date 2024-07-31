namespace Undersoft.SDK.Service.Server.Accounts.Licensing;

public class Payment : Access.Licensing.Payment
{
    public virtual EntitySet<AccountPayment> AccountPayments { get; set; }
}
