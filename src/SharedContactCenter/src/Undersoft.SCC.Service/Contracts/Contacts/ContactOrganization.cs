using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Contacts;

public class ContactOrganization : DataObject, IContract
{
    public string? OrganizationIndustry { get; set; }

    public string? OrganizationName { get; set; }

    public string? OrganizationFullName { get; set; }

    public string? PositionInOrganization { get; set; }

    public string? OrganizationWebsites { get; set; }

    public OrganizationSize OrganizationSize { get; set; }

    public string? OrganizationImage { get; set; }

    public byte[]? OrganizationImageData { get; set; }

    public long? ContactId { get; set; }
}
