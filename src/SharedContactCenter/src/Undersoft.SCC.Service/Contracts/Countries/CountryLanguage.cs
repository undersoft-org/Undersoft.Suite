using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Countries
{
    public class CountryLanguage : DataObject, IContract
    {
        [VisibleRubric]
        public string? Name { get; set; }

        [VisibleRubric]
        [DisplayRubric("Code")]
        public string? LanguageCode { get; set; }
    }
}
