namespace Undersoft.SDK.Service.Access.Identity
{
    public interface ITeam
    {
        string TeamFullName { get; set; }
        string TeamImage { get; set; }
        byte[] TeamImageData { get; set; }
        string TeamName { get; set; }
        string TeamWebsites { get; set; }
    }
}