using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Identity;

public class Team : DataObject, ITeam
{
    public string TeamName { get; set; }

    public string TeamFullName { get; set; }

    public string TeamWebsites { get; set; }

    public string TeamImage { get; set; }

    public byte[] TeamImageData { get; set; }
}
