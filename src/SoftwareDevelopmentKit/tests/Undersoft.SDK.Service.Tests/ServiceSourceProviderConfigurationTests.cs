using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServiceSourceProviderConfiguration"/>.
/// </summary>
[TestClass]
public class ServiceSourceProviderConfigurationTests
{
    private ServiceSourceProviderConfiguration _testClass;
    private IServiceRegistry _registry;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceSourceProviderConfiguration"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._registry = Substitute.For<IServiceRegistry>();
        this._testClass = new ServiceSourceProviderConfiguration(this._registry);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceSourceProviderConfiguration();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceSourceProviderConfiguration(this._registry);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the registry parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullRegistry()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceSourceProviderConfiguration(default(IServiceRegistry)));
    }

    /// <summary>
    /// Checks that the AddSourceProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddSourceProvider()
    {
        // Arrange
        var provider = StoreProvider.MariaDb;

        // Act
        var result = this._testClass.AddSourceProvider(provider);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the BuildOptions method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallBuildOptions()
    {
        // Arrange
        var builder = new DbContextOptionsBuilder();
        var provider = StoreProvider.PostgreSql;
        var connectionString = "TestValue1089867159";

        // Act
        var result = this._testClass.BuildOptions(builder, provider, connectionString);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the BuildOptions method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallBuildOptionsWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.BuildOptions(default(DbContextOptionsBuilder), StoreProvider.SqlLite, "TestValue1255389167"));
    }

    /// <summary>
    /// Checks that the BuildOptions method throws when the connectionString parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallBuildOptionsWithInvalidConnectionString(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.BuildOptions(new DbContextOptionsBuilder(), StoreProvider.SqlLite, value));
    }
}