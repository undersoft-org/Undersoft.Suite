using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Expression : DataObject
{
    public Expression() { }

    public string? Name { get; set; }

    public string? ShortName { get; set; }

    public string? Description { get; set; }

    public string? TaxId { get; set; }

    public string? CompanySize { get; set; }

    public string? Revenue { get; set; }

    public string? Website { get; set; }

}
