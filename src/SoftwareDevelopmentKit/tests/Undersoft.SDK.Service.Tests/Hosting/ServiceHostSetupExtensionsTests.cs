using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServiceHostSetupExtensions"/>.
/// </summary>
[TestClass]
public class ServiceHostSetupExtensionsTests
{
    /// <summary>
    /// Checks that the UseAppSetup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseAppSetup()
    {
        // Arrange
        var app = Substitute.For<IHostBuilder>();
        var sm = Substitute.For<IServiceManager>();
        var env = Substitute.For<IHostEnvironment>();

        // Act
        var result = app.UseAppSetup(sm, env);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseAppSetup method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseAppSetupWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => default(IHostBuilder).UseAppSetup(Substitute.For<IServiceManager>(), Substitute.For<IHostEnvironment>()));
    }

    /// <summary>
    /// Checks that the UseAppSetup method throws when the sm parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseAppSetupWithNullSm()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IHostBuilder>().UseAppSetup(default(IServiceManager), Substitute.For<IHostEnvironment>()));
    }

    /// <summary>
    /// Checks that the UseAppSetup method throws when the env parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseAppSetupWithNullEnv()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IHostBuilder>().UseAppSetup(Substitute.For<IServiceManager>(), default(IHostEnvironment)));
    }

    /// <summary>
    /// Checks that the UseInternalProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseInternalProvider()
    {
        // Arrange
        var app = Substitute.For<IHostBuilder>();
        var sm = Substitute.For<IServiceManager>();

        // Act
        var result = app.UseInternalProvider(sm);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseInternalProvider method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseInternalProviderWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => default(IHostBuilder).UseInternalProvider(Substitute.For<IServiceManager>()));
    }

    /// <summary>
    /// Checks that the UseInternalProvider method throws when the sm parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseInternalProviderWithNullSm()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IHostBuilder>().UseInternalProvider(default(IServiceManager)));
    }

    /// <summary>
    /// Checks that the RebuildProviders method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRebuildProviders()
    {
        // Arrange
        var app = Substitute.For<IHostBuilder>();
        var sm = Substitute.For<IServiceManager>();

        // Act
        var result = app.RebuildProviders(sm);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the RebuildProviders method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallRebuildProvidersWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => default(IHostBuilder).RebuildProviders(Substitute.For<IServiceManager>()));
    }

    /// <summary>
    /// Checks that the RebuildProviders method throws when the sm parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallRebuildProvidersWithNullSm()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IHostBuilder>().RebuildProviders(default(IServiceManager)));
    }

    /// <summary>
    /// Checks that the LoadOpenDataEdms method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallLoadOpenDataEdmsAsync()
    {
        // Arrange
        var app = Substitute.For<IServiceHostSetup>();

        // Act
        await app.LoadOpenDataEdms();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the LoadOpenDataEdms method throws when the app parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallLoadOpenDataEdmsWithNullAppAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => default(IServiceHostSetup).LoadOpenDataEdms());
    }
}