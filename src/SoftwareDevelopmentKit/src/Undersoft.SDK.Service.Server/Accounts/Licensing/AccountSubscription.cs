namespace Undersoft.SDK.Service.Server.Accounts.Licensing;

public class AccountSubscription : Subscription
{
    public long? SubscriptionId { get; set; }
    public virtual Subscription Subscription { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}


