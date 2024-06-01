using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SSC.Service.Contracts.Accounts;

public class AccountSubscription : DataObject
{
    [VisibleRubric]
    [DisplayRubric("Name")]
    public string? SubscriptionName { get; set; }

    [VisibleRubric]
    [DisplayRubric("Description")]
    public string? SubscriptionDescription { get; set; }

    [VisibleRubric]
    [DisplayRubric("Expiration date")]
    public DateTime SubscriptionExpireDate { get; set; }

    [VisibleRubric]
    [DisplayRubric("Number of accounts")]
    public double SubscriptionQuantity { get; set; }

    public double SubscriptionValue { get; set; }

    [VisibleRubric]
    [DisplayRubric("Period type")]
    public double SubscriptionPeriod { get; set; }

    public string? SubscriptionCurrency { get; set; }

    [VisibleRubric]
    [DisplayRubric("Status")]
    public string? SubscriptionStatus { get; set; }

    public string? SubscriptionToken { get; set; }

    public long? AccountId { get; set; }
}


