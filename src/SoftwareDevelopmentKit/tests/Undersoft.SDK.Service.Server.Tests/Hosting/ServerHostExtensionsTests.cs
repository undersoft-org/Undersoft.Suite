using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.SDK.Service.Server.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServerHostExtensions"/>.
/// </summary>
[TestClass]
public class ServerHostExtensionsTests
{
    /// <summary>
    /// Checks that the UseServerSetup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseServerSetupWithIApplicationBuilder()
    {
        // Arrange
        var app = Substitute.For<IApplicationBuilder>();

        // Act
        var result = app.UseServerSetup();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseServerSetup method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseServerSetupWithIApplicationBuilderWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => default(IApplicationBuilder).UseServerSetup());
    }

    /// <summary>
    /// Checks that the UseServerSetup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseServerSetupWithAppAndEnv()
    {
        // Arrange
        var app = Substitute.For<IApplicationBuilder>();
        var env = Substitute.For<IWebHostEnvironment>();

        // Act
        var result = app.UseServerSetup(env);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseServerSetup method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseServerSetupWithAppAndEnvWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => default(IApplicationBuilder).UseServerSetup(Substitute.For<IWebHostEnvironment>()));
    }

    /// <summary>
    /// Checks that the UseServerSetup method throws when the env parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseServerSetupWithAppAndEnvWithNullEnv()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IApplicationBuilder>().UseServerSetup(default(IWebHostEnvironment)));
    }

    /// <summary>
    /// Checks that the UseDefaultProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseDefaultProvider()
    {
        // Arrange
        var app = Substitute.For<IApplicationBuilder>();

        // Act
        var result = app.UseDefaultProvider();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseDefaultProvider method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseDefaultProviderWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => default(IApplicationBuilder).UseDefaultProvider());
    }

    /// <summary>
    /// Checks that the UseInternalProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseInternalProvider()
    {
        // Arrange
        var app = Substitute.For<IApplicationBuilder>();

        // Act
        var result = app.UseInternalProvider();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseInternalProvider method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseInternalProviderWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => default(IApplicationBuilder).UseInternalProvider());
    }

    /// <summary>
    /// Checks that the RebuildProviders method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRebuildProviders()
    {
        // Arrange
        var app = Substitute.For<IApplicationBuilder>();

        // Act
        var result = app.RebuildProviders();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the RebuildProviders method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallRebuildProvidersWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => default(IApplicationBuilder).RebuildProviders());
    }

    /// <summary>
    /// Checks that the LoadOpenDataEdms method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallLoadOpenDataEdmsAsync()
    {
        // Arrange
        var app = new ServerHostSetup(Substitute.For<IApplicationBuilder>());

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
        await Should.ThrowAsync<ArgumentNullException>(() => default(ServerHostSetup).LoadOpenDataEdms());
    }
}