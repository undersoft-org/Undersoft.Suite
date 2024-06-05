using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Application.Server;

namespace Undersoft.SCC.Service.Application.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="Setup"/>.
/// </summary>
[TestClass]
public class SetupTests
{
    private Setup _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Setup"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Setup();
    }

    /// <summary>
    /// Checks that the ConfigureServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigureServices()
    {
        // Arrange
        var srvc = Substitute.For<IServiceCollection>();

        // Act
        this._testClass.ConfigureServices(srvc);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ConfigureServices method throws when the srvc parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureServicesWithNullSrvc()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ConfigureServices(default(IServiceCollection)));
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var app = Substitute.For<IApplicationBuilder>();
        var env = Substitute.For<IWebHostEnvironment>();

        // Act
        this._testClass.Configure(app, env);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the app parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullApp()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(IApplicationBuilder), Substitute.For<IWebHostEnvironment>()));
    }

    /// <summary>
    /// Checks that the Configure method throws when the env parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullEnv()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(Substitute.For<IApplicationBuilder>(), default(IWebHostEnvironment)));
    }
}