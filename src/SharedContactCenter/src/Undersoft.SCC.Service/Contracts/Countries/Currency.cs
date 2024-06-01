using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Countries
{
    public partial class Currency : DataObject, IContract
    {
        [VisibleRubric]
        public string? Name { get; set; }

        [VisibleRubric]
        [DisplayRubric("Code")]
        public string? CurrencyCode { get; set; }
    }
}
