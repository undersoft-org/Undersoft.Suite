using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.GCC.Service.Contracts;

public class CurrencyProvider : DataObject, IContract
{
    public string? Name { get => Label; set => Label = value; }

    public string? FullName { get; set; }

    public CurrencyProviderType Type { get; set; }

    public long? BaseCurrencyId { get; set; }
    public Currency? BaseCurrency { get; set; }

    public string? BaseUri { get; set; }

    public int UpdateHour { get; set; }

    public int UpdateMinute { get; set; }

    public DateTime HistorySince { get; set; }
}
