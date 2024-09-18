using System.Text;
using System.Xml;
using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Logging;

namespace Undersoft.GCC.Infrastructure.Currencies
{
    public abstract class CurrenciesXmlAdapter : CurrenciesAdapter
    {
        protected CurrencyProvider _provider = default!;

        public CurrenciesXmlAdapter() { }

        public CurrenciesXmlAdapter(CurrencyProvider provider)
        {
            SetProvider(provider);
        }

        public override void SetProvider(CurrencyProvider provider) { _provider = provider; }

        public IList<XmlDocument> GetXmlDocuments(
            MemoryStream data,
            IEnumerable<int> sizes
        )
        {
            int offset = 0;
            IList<XmlDocument> xmlDocuments = new List<XmlDocument>();
            var mem = new Memory<byte>(data.GetAllBytes());
            try
            {
                foreach (var size in sizes.Where(s => s > 0))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    var xml = Encoding.UTF8.GetString(mem.Slice(offset, size).ToArray());
                    if (string.IsNullOrEmpty(xml))
                        continue;
                    xmlDocument.LoadXml(xml);
                    xmlDocuments.Add(xmlDocument);
                    offset += size;
                }
            }
            catch (Exception ex)
            {
                this.Failure<Datalog>("Parsing to Xml Document failed", null, ex);
                xmlDocuments.Clear();
            }
            data.Dispose();
            return xmlDocuments;
        }
    }
}
