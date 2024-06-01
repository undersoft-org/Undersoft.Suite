using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Validator : Amount
{
    public Validator() { }

    public string? Name { get; set; }

    public string? Description { get; set; }

}
