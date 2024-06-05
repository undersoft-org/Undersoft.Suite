using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Contacts;

public class ContactAddress : DataObject, IViewModel
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