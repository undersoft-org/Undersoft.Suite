using System.Security.Claims;

namespace Undersoft.SDK.Service.Access
{
    public interface IAccessContext : IAccess
    {
        IAuthorization Current { get; }

        DateTime? Expiration { get; }     
    }
}