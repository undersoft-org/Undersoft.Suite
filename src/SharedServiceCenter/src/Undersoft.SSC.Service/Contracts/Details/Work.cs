using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Work : DataObject
{
    public Work() { }

    public string? Name { get; set; }

    public string? Description { get; set; }

}
