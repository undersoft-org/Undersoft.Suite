namespace Undersoft.SDK.Tests.Instant.Math;

using Undersoft.SDK.Instant.Attributes;
using Undersoft.SDK.Uniques;
using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class InstantMathTestDataModel : Identifiable
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
    public string Alias = "StockAlias";

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    public string Name = "StockFullName";
    public int Quantity = 86;

    public double BuyFeeRate { get; set; } = 8;

    public double BuyNetCost { get; set; } = 0.85;

    public double BuyNetTotal { get; set; } = 0.85;

    [InstantAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public byte[] ByteArray { get; set; } = new byte[10];

    public double CurrencyRate { get; set; } = 1.23;

    public double CurrentMarkupRate { get; set; } = 0;

    public Guid GlobalId { get; set; } = new Guid();

    public double NetCost { get; set; } = 1.00;

    public double NetPrice { get; set; } = 1.00;

    public double SellFeeRate { get; set; } = 8;

    public double SellGrossPrice { get; set; } = 0;

    public double SellGrossTotal { get; set; } = 0;

    public double SellNetPrice { get; set; } = 1.00;

    public double SellNetTotal { get; set; } = 1.00;

    public bool Status { get; set; }

    public double TargetMarkupRate { get; set; } = 0;

    public double TaxRate { get; set; } = 1.23;
}
