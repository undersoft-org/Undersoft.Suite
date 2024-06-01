namespace Undersoft.SDK.Service.Access
{
    public interface IClaim
    {
        string ClaimType { get; set; }
        string ClaimValue { get; set; }
    }
}