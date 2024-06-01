using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;

namespace Undersoft.SDK.Service.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServiceSetupExtensions"/>.
/// </summary>
[TestClass]
public class ServiceSetupExtensionsTests
{
    /// <summary>
    /// Checks that the AddServiceSetup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddServiceSetupWithServices()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();

        // Act
        var result = services.AddServiceSetup();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddServiceSetup method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddServiceSetupWithServicesWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceCollection).AddServiceSetup());
    }

    /// <summary>
    /// Checks that the AddServiceSetup maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void AddServiceSetupWithServicesPerformsMapping()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();

        // Act
        var result = services.AddServiceSetup();

        // Assert
        result.Services.ShouldBeSameAs(services);
    }

    /// <summary>
    /// Checks that the AddServiceSetup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddServiceSetupWithServicesAndConfiguration()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();
        var configuration = Substitute.For<IConfiguration>();

        // Act
        var result = services.AddServiceSetup(configuration);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddServiceSetup method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddServiceSetupWithServicesAndConfigurationWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceCollection).AddServiceSetup(Substitute.For<IConfiguration>()));
    }

    /// <summary>
    /// Checks that the AddServiceSetup method throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddServiceSetupWithServicesAndConfigurationWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IServiceCollection>().AddServiceSetup(default(IConfiguration)));
    }

    /// <summary>
    /// Checks that the AddServiceSetup maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void AddServiceSetupWithServicesAndConfigurationPerformsMapping()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();
        var configuration = Substitute.For<IConfiguration>();

        // Act
        var result = services.AddServiceSetup(configuration);

        // Assert
        result.Services.ShouldBeSameAs(services);
    }
}