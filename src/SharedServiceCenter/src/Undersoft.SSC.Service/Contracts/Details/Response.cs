using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Response : DataObject
{
    public Response() { }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? IssuerName { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ExpirationDate { get; set; }
}
