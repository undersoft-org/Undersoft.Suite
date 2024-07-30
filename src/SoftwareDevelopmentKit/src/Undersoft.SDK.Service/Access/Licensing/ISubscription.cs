
namespace Undersoft.SDK.Service.Access.Licensing
{
    public interface ISubscription
    {
        string SubscriptionCurrency { get; set; }
        string SubscriptionDescription { get; set; }
        DateTime SubscriptionExpireDate { get; set; }
        string SubscriptionName { get; set; }
        double SubscriptionPeriod { get; set; }
        double SubscriptionQuantity { get; set; }
        string SubscriptionStatus { get; set; }
        string SubscriptionToken { get; set; }
        double SubscriptionValue { get; set; }
    }
}