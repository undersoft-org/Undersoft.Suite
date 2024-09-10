using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Sql : DataObject
{
    public Sql() { }

    public string? Name { get; set; }

    public string? Description { get; set; }

}
