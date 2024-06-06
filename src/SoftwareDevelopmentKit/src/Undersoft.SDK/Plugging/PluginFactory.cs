namespace Undersoft.SDK.Plugging
{
    using Undersoft.SDK.Series;

    public static class PluginFactory
    {
        public static ISeries<Plugin> Cache = new Registry<Plugin>(true);
    }
}
