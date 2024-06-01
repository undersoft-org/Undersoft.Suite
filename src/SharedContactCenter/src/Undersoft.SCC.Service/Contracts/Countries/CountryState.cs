using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Countries
{
    public class CountryState : DataObject, IContract
    {
        [VisibleRubric]
        public string? Name { get; set; }

        [VisibleRubric]
        [DisplayRubric("Code")]
        public string? StateCode { get; set; }

        [VisibleRubric]
        [DisplayRubric("Time zone UTC")]
        public string? TimeZone { get; set; }

        public long? CountryId { get; set; }
    }
}
