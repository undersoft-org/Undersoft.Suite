namespace Undersoft.SDK.Plugging
{
    using System.Reflection;
    using System.Runtime.Loader;
    using Undersoft.SDK.Proxies;
    using Undersoft.SDK.Series;

    public static class PluginFactory
    {
        public static ISeries<Plugin> Cache = new Registry<Plugin>(true);
    }
}
