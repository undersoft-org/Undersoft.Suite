using System.Reflection;

namespace Undersoft.SDK.Service.Configuration.Options;

public class ConfigurationOptions
{
    public string BasePath { get; set; }

    public string[] CommandLineArgs { get; set; }

    public string EnvironmentName { get; set; }

    public string EnvironmentVariablesPrefix { get; set; }

    public string GeneralFileName { get; set; } = "appsettings";

    public string[] OptionalFileNames { get; set; }

    public Assembly UserSecretsAssembly { get; set; }

    public string UserSecretsId { get; set; } = "2e23f1cc-5ef2-44a4-83ae-0123d3ca2365";
}