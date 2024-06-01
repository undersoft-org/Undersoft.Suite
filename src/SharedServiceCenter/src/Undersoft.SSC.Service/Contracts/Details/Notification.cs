using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Notification : DataObject
{
    public Notification() { }

    public string? Name { get; set; }

    public string? Description { get; set; }

}
