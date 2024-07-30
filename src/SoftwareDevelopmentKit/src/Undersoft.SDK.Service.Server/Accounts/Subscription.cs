namespace Undersoft.SDK.Service.Server.Accounts;

public class Subscription : Access.Licensing.Subscription
{
    public virtual EntitySet<AccountSubscription> AccountSubscriptions { get; set; }
}


