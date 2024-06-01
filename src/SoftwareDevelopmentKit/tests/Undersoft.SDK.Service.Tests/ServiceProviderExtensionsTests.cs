using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;

namespace Undersoft.SDK.Service.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServiceProviderExtensions"/>.
/// </summary>
[TestClass]
public class ServiceProviderExtensionsTests
{
    /// <summary>
    /// Checks that the AddPropertyInjection method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddPropertyInjection()
    {
        // Arrange
        var provider = Substitute.For<IServiceProvider>();

        // Act
        var result = provider.AddPropertyInjection();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddPropertyInjection method throws when the provider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddPropertyInjectionWithNullProvider()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceProvider).AddPropertyInjection());
    }

    /// <summary>
    /// Checks that the LoadDataServiceModelsAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallLoadDataServiceModelsAsync()
    {
        // Arrange
        var provider = Substitute.For<IServiceProvider>();

        // Act
        await provider.LoadDataServiceModelsAsync();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the LoadDataServiceModelsAsync method throws when the provider parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallLoadDataServiceModelsAsyncWithNullProviderAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => default(IServiceProvider).LoadDataServiceModelsAsync());
    }

    /// <summary>
    /// Checks that the LoadDataServiceModels method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLoadDataServiceModels()
    {
        // Arrange
        var provider = Substitute.For<IServiceProvider>();

        // Act
        provider.LoadDataServiceModels();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the LoadDataServiceModels method throws when the provider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLoadDataServiceModelsWithNullProvider()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceProvider).LoadDataServiceModels());
    }

    /// <summary>
    /// Checks that the UseServiceClientsAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallUseServiceClientsAsync()
    {
        // Arrange
        var provider = Substitute.For<IServiceProvider>();

        // Act
        var result = await provider.UseServiceClientsAsync();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseServiceClientsAsync method throws when the provider parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallUseServiceClientsAsyncWithNullProviderAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => default(IServiceProvider).UseServiceClientsAsync());
    }

    /// <summary>
    /// Checks that the UseServiceClients method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseServiceClients()
    {
        // Arrange
        var provider = Substitute.For<IServiceProvider>();

        // Act
        var result = provider.UseServiceClients();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseServiceClients method throws when the provider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseServiceClientsWithNullProvider()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceProvider).UseServiceClients());
    }
}

/// <summary>
/// Unit tests for the type <see cref="InjectPropertyAttribute"/>.
/// </summary>
[TestClass]
public class InjectPropertyAttributeTests
{
    private InjectPropertyAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="InjectPropertyAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new InjectPropertyAttribute();
    }
}