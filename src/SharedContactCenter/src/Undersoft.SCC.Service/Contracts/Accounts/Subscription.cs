// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Accounts;

public class Subscription : DataObject, IContract
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
}


