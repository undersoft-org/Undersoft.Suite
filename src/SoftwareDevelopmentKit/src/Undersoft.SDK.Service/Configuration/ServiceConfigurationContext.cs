using Undersoft.SDK.Series;
using System.Diagnostics.CodeAnalysis;

namespace Undersoft.SDK.Service.Configuration;

public class ServiceConfigurationContext
{
    public IServiceRegistry Services { get; }

    public ISeries<object> Items { get; }

    public object this[string key]
    {
        get => Items[key];
        set => Items[key] = value;
    }

    public ServiceConfigurationContext([DisallowNull] IServiceRegistry services)
    {
        Services = services;
        Items = new Registry<object>();
    }
}
