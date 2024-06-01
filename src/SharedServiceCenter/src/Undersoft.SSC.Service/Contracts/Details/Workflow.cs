using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class LambdaQuery : DataObject
{
    public LambdaQuery() { }

    public QueryParameters? Query { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

}
