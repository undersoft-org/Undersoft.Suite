namespace Undersoft.SDK.Service.Access.Models
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