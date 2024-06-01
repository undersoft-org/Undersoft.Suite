using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Uniques;

namespace Undersoft.GCC.Service.Contracts;

public class Currency : DataObject, IContract
{
    private string? _currencyCode;

    public string? CurrencyCode
    {
        get => _currencyCode;
        set => Id = (_currencyCode = value).UniqueKey64();
    }

    public string? Symbol { get; set; }

    public string? Name
    {
        get => Label;
        set => Label = value;
    }

    public bool IsDecimal { get; set; }
}
