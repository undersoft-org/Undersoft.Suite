using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Accounts;

public class AccountAddress : DataObject, IContract
{
    [VisibleRubric]
    public string Country { get; set; } = default!;

    [VisibleRubric]
    public string State { get; set; } = default!;

    [VisibleRubric]
    public string City { get; set; } = default!;

    [VisibleRubric]
    public string Postcode { get; set; } = default!;

    [VisibleRubric]
    public string Street { get; set; } = default!;

    [VisibleRubric]
    public string Building { get; set; } = default!;

    [VisibleRubric]
    [RequiredRubric]
    public string Apartment { get; set; } = default!;

    public long? AccountId { get; set; }
}
