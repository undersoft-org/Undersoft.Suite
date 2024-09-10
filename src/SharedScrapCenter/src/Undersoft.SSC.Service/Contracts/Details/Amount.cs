using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Amount : DataObject
{
    public Amount() { }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public AmountKind? Kind { get; set; }

    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public DateTime Deadline { get; set; }
    public TimeSpan Interval { get; set; }
    public double Duration { get; set; }

    public string? Unit { get; set; }

    public double Quantity { get; set; }

    public double Value { get; set; }
    public double Tax { get; set; }
    public double TaxValue { get; set; }
    public double NetValue { get; set; }

    public double Fraction { get; set; }
    public double Factor { get; set; }
    public double Bias { get; set; }

    public double GrossAmount { get; set; }
    public double TaxAmount { get; set; }
    public double NetAmount { get; set; }

    public double Share { get; set; }
    public double TaxShare { get; set; }
    public double NetShare { get; set; }
}

public enum AmountKind
{
    Cost,
    Price,
    Offer,
    Capacity,
    Ordered,
    Shipped,
    Due,
    Payed,
    Returned,
    Usage,
    Used,
    Required,
    Planned,
    Possibly,
    Potentially,
    Probablly,
    Forecast,
    Income,
    Outcome,
    Loss,
    Gain,
    Asset,
    Liability
}
