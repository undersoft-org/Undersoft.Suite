using System.Globalization;
using System.Xml;
using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Uniques;

namespace Undersoft.GCC.Infrastructure.Currencies.ECB;

public class ECBCurrenciesAdapter : CurrenciesXmlAdapter
{
    public ECBCurrenciesAdapter(CurrencyProvider provider) : base(provider) { }

    public override IEnumerable<Currency> ReadCurrencies(MemoryStream stream, IEnumerable<int> sizes)
    {
        var documents = GetXmlDocuments(stream, sizes);

        foreach (var document in documents)
        {
            var nodes = document.ChildNodes;
            var envelope = nodes.Cast<XmlNode>().Where(n => n.LocalName == "Envelope").First();
            var cube = envelope.ChildNodes.Cast<XmlNode>().Where(n => n.Name == "Cube").First().FirstChild;
            foreach (XmlElement item in cube!.ChildNodes)
            {
                var currencies = item.Attributes.Cast<XmlAttribute>();
                yield return new Currency()
                {
                    CurrencyCode = currencies.Where(c => c.Name == "currency").First().Value
                };
            }
        }
    }

    public override IEnumerable<CurrencyRate> ReadCurrencyRates(MemoryStream stream, IEnumerable<int> sizes)
    {
        var documents = GetXmlDocuments(stream, sizes);

        foreach (var document in documents)
        {
            var nodes = document.ChildNodes;
            var envelope = nodes.Cast<XmlNode>().Where(n => n.LocalName == "Envelope").First();
            var cube = envelope.ChildNodes.Cast<XmlNode>().Where(n => n.Name == "Cube").First().FirstChild;
            var publishTimeString = cube.Attributes!.Cast<XmlAttribute>().Where(a => a.Name == "time").FirstOrDefault();
            DateTime publishTime;
            if (publishTimeString != null && DateTime.TryParse(publishTimeString.Value, out publishTime))
            {
                foreach (XmlElement item in cube!.ChildNodes)
                {
                    var currencies = item.Attributes.Cast<XmlAttribute>();
                    yield return new CurrencyRate()
                    {
                        ProviderId = _provider.Id,
                        Label = publishTime.ToString(),
                        SourceCurrencyId = _provider.BaseCurrencyId,
                        SourceRate = 1.00D,
                        TargetCurrencyId = currencies.Where(c => c.Name == "currency").First().Value.UniqueKey64(),
                        TargetRate = Double.Parse(currencies.Where(c => c.Name == "rate").First().Value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture),
                        PublishDate = publishTime,
                        Type = CurrencyRateType.Mid
                    };
                }
            }
        }
    }
}
