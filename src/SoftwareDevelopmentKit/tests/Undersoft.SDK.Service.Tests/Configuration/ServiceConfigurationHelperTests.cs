using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Configuration;
using Undersoft.SDK.Service.Configuration.Options;

namespace Undersoft.SDK.Service.Tests.Configuration;

/// <summary>
/// Unit tests for the type <see cref="ServiceConfigurationHelper"/>.
/// </summary>
[TestClass]
public class ServiceConfigurationHelperTests
{
    /// <summary>
    /// Checks that the BuildConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallBuildConfiguration()
    {
        // Arrange
        var args = new[] { "TestValue1314684751", "TestValue1058874828", "TestValue15646430" };
        var options = new ConfigurationOptions
        {
            BasePath = "TestValue172342742",
            CommandLineArgs = new[] { "TestValue1396939454", "TestValue1469574243", "TestValue2035079180" },
            EnvironmentName = "TestValue478086132",
            EnvironmentVariablesPrefix = "TestValue1392422541",
            GeneralFileName = "TestValue157269835",
            OptionalFileNames = new[] { "TestValue1422574785", "TestValue737917854", "TestValue708154151" },
            UserSecretsAssembly = Assembly.GetAssembly(typeof(string)),
            UserSecretsId = "TestValue593389293"
        };
        Action<IConfigurationBuilder> builderAction = x => { };

        // Act
        var result = ServiceConfigurationHelper.BuildConfiguration(args, options, builderAction);

        // Assert
        Assert.Fail("Create or modify test");
    }
}