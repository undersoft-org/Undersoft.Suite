using Undersoft.SDK.Instant;
using Undersoft.SDK.Instant.Series;

namespace Undersoft.SDK.Proxies
{
    public interface IProxyGenerator : IInstantGenerator
    {

        Type TargetType { get; }
        IInstantSeries Generate();
        IProxy GenerateProxy(object target = null);
        ProxyGenerator GetProxyGenerator();
    }
}