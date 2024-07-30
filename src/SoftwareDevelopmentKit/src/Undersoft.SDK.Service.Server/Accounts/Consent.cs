namespace Undersoft.SDK.Service.Server.Accounts;

public class Consent : Access.Licensing.Consent
{
    public virtual EntitySet<AccountConsent> AccountConsents { get; set; }
}
