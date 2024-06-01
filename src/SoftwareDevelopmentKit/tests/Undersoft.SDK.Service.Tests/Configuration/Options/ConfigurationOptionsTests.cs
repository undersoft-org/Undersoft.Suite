using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Configuration.Options;

namespace Undersoft.SDK.Service.Tests.Configuration.Options;

/// <summary>
/// Unit tests for the type <see cref="ConfigurationOptions"/>.
/// </summary>
[TestClass]
public class ConfigurationOptionsTests
{
    private ConfigurationOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ConfigurationOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ConfigurationOptions();
    }

    /// <summary>
    /// Checks that the BasePath property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBasePath()
    {
        // Arrange
        var testValue = "TestValue537993900";

        // Act
        this._testClass.BasePath = testValue;

        // Assert
        this._testClass.BasePath.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CommandLineArgs property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCommandLineArgs()
    {
        // Arrange
        var testValue = new[] { "TestValue908228746", "TestValue1975611859", "TestValue1232768700" };

        // Act
        this._testClass.CommandLineArgs = testValue;

        // Assert
        this._testClass.CommandLineArgs.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the EnvironmentName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEnvironmentName()
    {
        // Arrange
        var testValue = "TestValue1345682699";

        // Act
        this._testClass.EnvironmentName = testValue;

        // Assert
        this._testClass.EnvironmentName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the EnvironmentVariablesPrefix property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEnvironmentVariablesPrefix()
    {
        // Arrange
        var testValue = "TestValue1325470229";

        // Act
        this._testClass.EnvironmentVariablesPrefix = testValue;

        // Assert
        this._testClass.EnvironmentVariablesPrefix.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the GeneralFileName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGeneralFileName()
    {
        // Arrange
        var testValue = "TestValue1466803468";

        // Act
        this._testClass.GeneralFileName = testValue;

        // Assert
        this._testClass.GeneralFileName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OptionalFileNames property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOptionalFileNames()
    {
        // Arrange
        var testValue = new[] { "TestValue1177630449", "TestValue915712840", "TestValue92670092" };

        // Act
        this._testClass.OptionalFileNames = testValue;

        // Assert
        this._testClass.OptionalFileNames.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the UserSecretsAssembly property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetUserSecretsAssembly()
    {
        // Arrange
        var testValue = Assembly.GetAssembly(typeof(string));

        // Act
        this._testClass.UserSecretsAssembly = testValue;

        // Assert
        this._testClass.UserSecretsAssembly.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the UserSecretsId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetUserSecretsId()
    {
        // Arrange
        var testValue = "TestValue790939410";

        // Act
        this._testClass.UserSecretsId = testValue;

        // Assert
        this._testClass.UserSecretsId.ShouldBe(testValue);
    }
}