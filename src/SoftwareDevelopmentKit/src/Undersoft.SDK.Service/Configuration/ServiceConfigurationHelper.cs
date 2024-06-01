using Microsoft.Extensions.Configuration;

namespace Undersoft.SDK.Service.Configuration;

using Undersoft.SDK.Service.Configuration.Options;

public static class ServiceConfigurationHelper
{
    public static IConfigurationRoot BuildConfiguration(string[] args = null,
        ConfigurationOptions options = null,
        Action<IConfigurationBuilder> builderAction = null
    )
    {
        options = options ?? new ConfigurationOptions();
        options.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (options.BasePath == null)
            options.BasePath = Directory.GetCurrentDirectory();

        string suffix = ".json";
        if (!(options.EnvironmentName == null))
            suffix = $".{options.EnvironmentName}.json";

        IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(options.BasePath)
        .AddJsonFile($"{options.GeneralFileName}{suffix}", optional: true, reloadOnChange: true);

        if (options.OptionalFileNames != null && options.OptionalFileNames.Length > 0)
            options.OptionalFileNames.ForEach(
               optionalName => builder.AddJsonFile($"{optionalName}{suffix}", optional: true, reloadOnChange: true)
            );

        if (args != null)
            builder.AddCommandLine(args);

        if (options.EnvironmentName == "Development")
        {
            if (options.UserSecretsId != null)
            {
                builder.AddUserSecrets(options.UserSecretsId);
            }
            else if (options.UserSecretsAssembly != null)
            {
                builder.AddUserSecrets(options.UserSecretsAssembly, true);
            }
        }

        builderAction?.Invoke(builder);

        return builder.Build();
    }
}
