using Undersoft.SDK.Instant;
using Undersoft.SDK.Instant.Series;

namespace Undersoft.SDK.Proxies
{
    public interface IProxyCreator : IInstantCreator
    {

        Type TargetType { get; }
        IInstantSeries Create();
        IProxy CreateProxy(object target = null);
        ProxyCreator GetProxyCreator();
    }
}