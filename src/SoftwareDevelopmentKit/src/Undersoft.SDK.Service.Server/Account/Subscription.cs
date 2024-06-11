namespace Undersoft.SDK.Service.Server.Accounts;

public class Subscription : DataObject
{
    public string SubscriptionName { get; set; }

    public string SubscriptionDescription { get; set; }

    public DateTime SubscriptionExpireDate { get; set; }

    public double SubscriptionQuantity { get; set; }

    public double SubscriptionValue { get; set; }

    public double SubscriptionPeriod { get; set; }

    public string SubscriptionCurrency { get; set; }

    public string SubscriptionStatus { get; set; }

    public string SubscriptionToken { get; set; }

    public virtual EntitySet<AccountSubscription> AccountSubscriptions { get; set; }
}


