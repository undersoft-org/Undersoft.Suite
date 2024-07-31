namespace Undersoft.SDK.Service.Server.Accounts.Licensing;

public class AccountConsent : Access.Licensing.Consent
{
    public long? ConsentId { get; set; }
    public virtual Consent Consent { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
