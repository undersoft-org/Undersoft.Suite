namespace Undersoft.SDK.Service.Server.Accounts.Licensing;

public class Subscription : Access.Licensing.Subscription
{
    public virtual EntitySet<AccountSubscription> AccountSubscriptions { get; set; }
}


