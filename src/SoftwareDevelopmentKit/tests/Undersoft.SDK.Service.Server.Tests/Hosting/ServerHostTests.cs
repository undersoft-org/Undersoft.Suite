using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Hosting;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.SDK.Service.Server.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServerHost"/>.
/// </summary>
[TestClass]
public class ServerHostTests
{
    private ServerHost _testClass;
    private Action<IWebHostBuilder> _builder;
    private string[] _args;
    private IConfiguration _configuration;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServerHost"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._builder = x => { };
        this._args = new[] { "TestValue699760449", "TestValue400252202", "TestValue1829945209" };
        this._configuration = Substitute.For<IConfiguration>();
        this._testClass = new ServerHost(this._builder);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServerHost(this._builder);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServerHost(this._args);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServerHost(this._configuration);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => new ServerHost(default(Action<IWebHostBuilder>)));
    }

    /// <summary>
    /// Checks that instance construction throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => new ServerHost(default(IConfiguration)));
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        Action<IWebHostBuilder> builder = x => { };

        // Act
        var result = this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(Action<IWebHostBuilder>)));
    }

    /// <summary>
    /// Checks that the Run method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRun()
    {
        // Act
        this._testClass.Run();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ServiceHosts property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServiceHosts()
    {
        // Arrange
        var testValue = new Registry<ServiceHost>(false, 67117706);

        // Act
        this._testClass.ServiceHosts = testValue;

        // Assert
        this._testClass.ServiceHosts.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the HostedServices property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetHostedServices()
    {
        // Assert
        this._testClass.HostedServices.ShouldBeOfType<Registry<IServiceProvider>>();

        Assert.Fail("Create or modify test");
    }
}