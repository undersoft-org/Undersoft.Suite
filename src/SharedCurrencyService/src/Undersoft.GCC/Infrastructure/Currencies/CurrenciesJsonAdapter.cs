using System.Text.Json;
using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Logging;

namespace Undersoft.GCC.Infrastructure.Currencies;

public abstract class CurrenciesJsonAdapter : CurrenciesAdapter
{
    protected CurrencyProvider _provider = default!;

    public CurrenciesJsonAdapter() { }

    public CurrenciesJsonAdapter(CurrencyProvider provider)
    {
        SetProvider(provider);
    }

    public override void SetProvider(CurrencyProvider provider)
    {
        _provider = provider;
    }

    public IList<JsonDocument> GetJsonDocuments(
        MemoryStream data,
        IEnumerable<int> sizes
    )
    {
        int offset = 0;
        IList<JsonDocument> jsonDocuments = new List<JsonDocument>();
        var mem = new Memory<byte>(data.GetAllBytes());
        try
        {
            foreach (var size in sizes)
            {
                jsonDocuments.Add(
                    JsonDocument.Parse(
                        mem.Slice(offset, size),
                        new JsonDocumentOptions() { AllowTrailingCommas = true }
                    )
                );

                offset += size;
            }
        }
        catch (Exception ex)
        {
            this.Failure<Datalog>("Parsing to JSON Document failed", null, ex);
            jsonDocuments.Clear();
        }
        data.Dispose();
        return jsonDocuments;
    }
}
