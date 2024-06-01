using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.SDK.Service.Server.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServerHostSetup"/>.
/// </summary>
[TestClass]
public class ServerHostSetupTests
{
    private ServerHostSetup _testClass;
    private IApplicationBuilder _application;
    private IWebHostEnvironment _environment;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServerHostSetup"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._application = Substitute.For<IApplicationBuilder>();
        this._environment = Substitute.For<IWebHostEnvironment>();
        this._testClass = new ServerHostSetup(this._application, this._environment);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServerHostSetup(this._application);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServerHostSetup(this._application, this._environment);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the application parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullApplication()
    {
        Should.Throw<ArgumentNullException>(() => new ServerHostSetup(default(IApplicationBuilder)));
        Should.Throw<ArgumentNullException>(() => new ServerHostSetup(default(IApplicationBuilder), this._environment));
    }

    /// <summary>
    /// Checks that instance construction throws when the environment parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullEnvironment()
    {
        Should.Throw<ArgumentNullException>(() => new ServerHostSetup(this._application, default(IWebHostEnvironment)));
    }

    /// <summary>
    /// Checks that the RebuildProviders method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRebuildProviders()
    {
        // Act
        var result = this._testClass.RebuildProviders();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseEndpoints method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseEndpoints()
    {
        // Arrange
        var useRazorPages = true;

        // Act
        var result = this._testClass.UseEndpoints(useRazorPages);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MapFallbackToFile method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMapFallbackToFile()
    {
        // Arrange
        var filePath = "TestValue1552322015";

        // Act
        var result = this._testClass.MapFallbackToFile(filePath);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MapFallbackToFile method throws when the filePath parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallMapFallbackToFileWithInvalidFilePath(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.MapFallbackToFile(value));
    }

    /// <summary>
    /// Checks that the UseServiceClients method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseServiceClients()
    {
        // Act
        var result = this._testClass.UseServiceClients();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseDataMigrations method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseDataMigrations()
    {
        // Act
        var result = this._testClass.UseDataMigrations();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseDefaultProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseDefaultProvider()
    {
        // Arrange
        _application.ApplicationServices.Returns(Substitute.For<IServiceProvider>());

        // Act
        var result = this._testClass.UseDefaultProvider();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseInternalProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseInternalProvider()
    {
        // Act
        var result = this._testClass.UseInternalProvider();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseServiceServer method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseServiceServer()
    {
        // Arrange
        var apiVersions = new[] { "TestValue1662689151", "TestValue563865780", "TestValue1266033670" };

        // Act
        var result = this._testClass.UseServiceServer(apiVersions);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseCustomSetup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseCustomSetup()
    {
        // Arrange
        Action<IServerHostSetup> action = x => { };

        // Act
        var result = this._testClass.UseCustomSetup(action);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseCustomSetup method throws when the action parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseCustomSetupWithNullAction()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.UseCustomSetup(default(Action<IServerHostSetup>)));
    }

    /// <summary>
    /// Checks that the UseSwaggerSetup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseSwaggerSetup()
    {
        // Arrange
        var apiVersions = new[] { "TestValue766843593", "TestValue1454084582", "TestValue1986584009" };

        // Act
        var result = this._testClass.UseSwaggerSetup(apiVersions);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseSwaggerSetup method throws when the apiVersions parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUseSwaggerSetupWithNullApiVersions()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.UseSwaggerSetup(default(string[])));
    }

    /// <summary>
    /// Checks that the UseHeaderForwarding method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseHeaderForwarding()
    {
        // Act
        var result = this._testClass.UseHeaderForwarding();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseJwtMiddleware method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseJwtMiddleware()
    {
        // Act
        var result = this._testClass.UseJwtMiddleware();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Manager property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetManager()
    {
        // Assert
        this._testClass.Manager.ShouldBeOfType<IServiceManager>();

        Assert.Fail("Create or modify test");
    }
}