using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SSC.Domain.Entities.Enums;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Registration : DataObject
{
    public Registration() { }

    public RegistrationKind? Kind { get; set; }

    public bool? Completed { get; set; }

    public bool? Approved { get; set; }
}
