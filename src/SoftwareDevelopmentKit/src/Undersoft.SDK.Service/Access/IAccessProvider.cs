using System.Security.Claims;

namespace Undersoft.SDK.Service.Access
{
    public interface IAccessProvider<TModel> : IAccessService<TModel>, IAccessContext where TModel : class, IOrigin, IAuthorization
    {

    }
}