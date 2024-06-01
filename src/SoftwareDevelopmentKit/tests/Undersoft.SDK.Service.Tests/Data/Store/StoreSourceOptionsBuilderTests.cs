using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Tests.Data.Store;

/// <summary>
/// Unit tests for the type <see cref="StoreSourceOptionsBuilder"/>.
/// </summary>
[TestClass]
public class StoreSourceOptionsBuilderTests
{

    /// <summary>
    /// Checks that the AddEntityFrameworkSourceProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddEntityFrameworkSourceProviderWithStoreProvider()
    {
        // Arrange
        var provider = StoreProvider.Oracle;

        // Act
        var result = StoreSourceOptionsBuilder.AddEntityFrameworkSourceProvider(provider);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddEntityFrameworkSourceProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddEntityFrameworkSourceProviderWithRegistryAndProvider()
    {
        // Arrange
        var registry = Substitute.For<IServiceRegistry>();
        var provider = StoreProvider.SqlServer;

        // Act
        var result = registry.AddEntityFrameworkSourceProvider(provider);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddEntityFrameworkSourceProvider method throws when the registry parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddEntityFrameworkSourceProviderWithRegistryAndProviderWithNullRegistry()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceRegistry).AddEntityFrameworkSourceProvider(StoreProvider.MemoryDb));
    }

    /// <summary>
    /// Checks that the BuildOptions method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallBuildOptionsWithStoreProviderAndString()
    {
        // Arrange
        var provider = StoreProvider.SqlServer;
        var connectionString = "TestValue1389870514";

        // Act
        var result = StoreSourceOptionsBuilder.BuildOptions(provider, connectionString);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the BuildOptions method throws when the connectionString parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallBuildOptionsWithStoreProviderAndStringWithInvalidConnectionString(string value)
    {
        Should.Throw<ArgumentNullException>(() => StoreSourceOptionsBuilder.BuildOptions(StoreProvider.CosmosDb, value));
    }
}