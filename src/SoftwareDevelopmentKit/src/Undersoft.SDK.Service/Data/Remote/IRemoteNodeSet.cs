using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Data.Remote
{
    public interface IRemoteNodeSet<TLeft, TRight>
        where TLeft : class, IOrigin, IInnerProxy
        where TRight : class, IOrigin, IInnerProxy
    {
        object this[object key] { get; set; }

        IRemoteLink<TLeft, TRight> Single { get; }
    }
}