namespace Undersoft.SDK.Service.Server.Accounts.Licensing;

public class Consent : Access.Licensing.Consent
{
    public virtual EntitySet<AccountConsent> AccountConsents { get; set; }
}
