namespace Undersoft.SDK.Service.Access.Identity
{
    public interface IClaim
    {
        string ClaimType { get; set; }
        string ClaimValue { get; set; }
    }
}