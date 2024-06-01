using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServerSourceProviderConfiguration"/>.
/// </summary>
[TestClass]
public class ServerSourceProviderConfigurationTests
{
    private ServerSourceProviderConfiguration _testClass;
    private IServiceRegistry _registry;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServerSourceProviderConfiguration"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._registry = Substitute.For<IServiceRegistry>();
        this._testClass = new ServerSourceProviderConfiguration(this._registry);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServerSourceProviderConfiguration(this._registry);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the registry parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullRegistry()
    {
        Should.Throw<ArgumentNullException>(() => new ServerSourceProviderConfiguration(default(IServiceRegistry)));
    }

    /// <summary>
    /// Checks that the AddSourceProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddSourceProvider()
    {
        // Arrange
        var provider = StoreProvider.AzureSql;

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
        var provider = StoreProvider.MySql;
        var connectionString = "TestValue699372351";

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
        Should.Throw<ArgumentNullException>(() => this._testClass.BuildOptions(default(DbContextOptionsBuilder), StoreProvider.CosmosDb, "TestValue1050603515"));
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
        Should.Throw<ArgumentNullException>(() => this._testClass.BuildOptions(new DbContextOptionsBuilder(), StoreProvider.AzureSql, value));
    }
}