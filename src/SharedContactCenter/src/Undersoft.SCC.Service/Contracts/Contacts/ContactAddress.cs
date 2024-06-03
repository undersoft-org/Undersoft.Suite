using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Contacts;

public class ContactAddress : DataObject, IContract
{
    [VisibleRubric]
    public string? Country { get; set; }

    [VisibleRubric]
    public string? State { get; set; }

    [VisibleRubric]
    public string? City { get; set; }

    [VisibleRubric]
    public string? Postcode { get; set; }

    [VisibleRubric]
    public string? Street { get; set; }

    [VisibleRubric]
    public string? Building { get; set; }

    [VisibleRubric]
    public string? Apartment { get; set; }

    public string? Notes { get; set; }

    public long? ContactId { get; set; }
}