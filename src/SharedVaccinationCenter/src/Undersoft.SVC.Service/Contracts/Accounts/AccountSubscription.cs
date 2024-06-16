// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

namespace Undersoft.SVC.Service.Contracts.Accounts;

public class AccountSubscription : DataObject
{
    public string? SubscriptionName { get; set; }

    public string? SubscriptionDescription { get; set; }

    public DateTime SubscriptionExpireDate { get; set; } = DateTime.Parse("01.01.1990");

    public double SubscriptionQuantity { get; set; }

    public double SubscriptionValue { get; set; }

    public double SubscriptionPeriod { get; set; }

    public string? SubscriptionCurrency { get; set; }

    public string? SubscriptionStatus { get; set; }

    public string? SubscriptionToken { get; set; }

    public long? SubscriptionId { get; set; }

    public virtual Subscription? Subscription { get; set; }
}


