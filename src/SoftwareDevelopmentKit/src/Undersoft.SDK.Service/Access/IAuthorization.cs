using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SDK.Service.Access
{
    public interface IAuthorization : IOrigin, IInnerProxy
    {
        Credentials Credentials { get; set; }

        OperationNotes Notes { get; set; }
    }
}