// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SVC.Service.Contracts.Accounts;

public class AccountSubscription : DataObject
{
    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Name")]
    public string? SubscriptionName { get; set; }

    [VisibleRubric]
    [RubricSize(64)]
    [DisplayRubric("Description")]
    public string? SubscriptionDescription { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Period")]
    public double SubscriptionPeriod { get; set; }

    [VisibleRubric]
    [RubricSize(8)]
    [DisplayRubric("Expiration date")]
    public DateTime SubscriptionExpireDate { get; set; } = DateTime.Parse("01.01.1990");

    [VisibleRubric]
    [RubricSize(8)]
    [DisplayRubric("Quantity")]
    public double SubscriptionQuantity { get; set; }

    [VisibleRubric]
    [RubricSize(8)]
    [DisplayRubric("Amount")]
    public double SubscriptionValue { get; set; }

    [VisibleRubric]
    [RubricSize(4)]
    [DisplayRubric("Currency")]
    public string? SubscriptionCurrency { get; set; }

    [VisibleRubric]
    [RubricSize(16)]
    [DisplayRubric("Status")]
    public string? SubscriptionStatus { get; set; }

    public string? SubscriptionToken { get; set; }

    public long? SubscriptionId { get; set; }

    public virtual Subscription? Subscription { get; set; }
}


