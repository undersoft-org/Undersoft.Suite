using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Server;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServerSetupExtensions"/>.
/// </summary>
[TestClass]
public class ServerSetupExtensionsTests
{
    /// <summary>
    /// Checks that the AddServerSetup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddServerSetup()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();
        var mvcBuilder = Substitute.For<IMvcBuilder>();

        // Act
        var result = services.AddServerSetup(mvcBuilder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddServerSetup method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddServerSetupWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceCollection).AddServerSetup(Substitute.For<IMvcBuilder>()));
    }

    /// <summary>
    /// Checks that the LoadOpenDataEdms method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallLoadOpenDataEdmsAsync()
    {
        // Arrange
        var app = new ServerSetup(Substitute.For<IServiceCollection>(), Substitute.For<IMvcBuilder>());

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
        await Should.ThrowAsync<ArgumentNullException>(() => default(ServerSetup).LoadOpenDataEdms());
    }
}