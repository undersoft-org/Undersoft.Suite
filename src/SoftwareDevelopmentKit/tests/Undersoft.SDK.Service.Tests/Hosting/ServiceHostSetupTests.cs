using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServiceHostSetup"/>.
/// </summary>
[TestClass]
public partial class ServiceHostSetupTests
{
    private ServiceHostSetup _testClass;
    private IHostBuilder _host;
    private IServiceManager _manager;
    private IHostEnvironment _environment;
    private bool _useSwagger;
    private string[] _apiVersions;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceHostSetup"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._host = Substitute.For<IHostBuilder>();
        this._manager = Substitute.For<IServiceManager>();
        this._environment = Substitute.For<IHostEnvironment>();
        this._useSwagger = true;
        this._apiVersions = new[] { "TestValue1032148722", "TestValue1376839861", "TestValue63109829" };
        this._testClass = new ServiceHostSetup(this._host, this._manager, this._environment, this._useSwagger);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceHostSetup(this._host, this._manager);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceHostSetup(this._host, this._manager, this._environment, this._useSwagger);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceHostSetup(this._host, this._manager, this._environment, this._apiVersions);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the host parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullHost()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceHostSetup(default(IHostBuilder), this._manager));
        Should.Throw<ArgumentNullException>(() => new ServiceHostSetup(default(IHostBuilder), this._manager, this._environment, this._useSwagger));
        Should.Throw<ArgumentNullException>(() => new ServiceHostSetup(default(IHostBuilder), this._manager, this._environment, this._apiVersions));
    }

    /// <summary>
    /// Checks that instance construction throws when the manager parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullManager()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceHostSetup(this._host, default(IServiceManager)));
        Should.Throw<ArgumentNullException>(() => new ServiceHostSetup(this._host, default(IServiceManager), this._environment, this._useSwagger));
        Should.Throw<ArgumentNullException>(() => new ServiceHostSetup(this._host, default(IServiceManager), this._environment, this._apiVersions));
    }

    /// <summary>
    /// Checks that instance construction throws when the environment parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullEnvironment()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceHostSetup(this._host, this._manager, default(IHostEnvironment), this._useSwagger));
        Should.Throw<ArgumentNullException>(() => new ServiceHostSetup(this._host, this._manager, default(IHostEnvironment), this._apiVersions));
    }

    /// <summary>
    /// Checks that the RebuildProviders method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRebuildProviders()
    {
        // Arrange
        _host.UseServiceProviderFactory<IServiceCollection>(Arg.Any<IServiceProviderFactory<IServiceCollection>>()).Returns(Substitute.For<IHostBuilder>());
        _manager.GetProviderFactory().Returns(Substitute.For<IServiceProviderFactory<IServiceCollection>>());

        // Act
        var result = this._testClass.RebuildProviders();

        // Assert
        _host.Received().UseServiceProviderFactory<IServiceCollection>(Arg.Any<IServiceProviderFactory<IServiceCollection>>());
        _manager.Received().GetProviderFactory();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseDataServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseDataServices()
    {
        // Act
        var result = this._testClass.UseDataServices();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseDataMigrations method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseDataMigrations()
    {
        // Arrange
        _manager.CreateScope().Returns(Substitute.For<IServiceScope>());

        // Act
        var result = this._testClass.UseDataMigrations();

        // Assert
        _manager.Received().CreateScope();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UseInternalProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUseInternalProvider()
    {
        // Arrange
        _host.UseServiceProviderFactory<IServiceCollection>(Arg.Any<IServiceProviderFactory<IServiceCollection>>()).Returns(Substitute.For<IHostBuilder>());
        _manager.GetProviderFactory().Returns(Substitute.For<IServiceProviderFactory<IServiceCollection>>());
        _manager.Registry.Returns(Substitute.For<IServiceRegistry>());

        // Act
        var result = this._testClass.UseInternalProvider();

        // Assert
        _host.Received().UseServiceProviderFactory<IServiceCollection>(Arg.Any<IServiceProviderFactory<IServiceCollection>>());
        _manager.Received().GetProviderFactory();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Manager property is initialized correctly by the constructors.
    /// </summary>
    [TestMethod]
    public void ManagerIsInitializedCorrectly()
    {
        this._testClass = new ServiceHostSetup(this._host, this._manager);
        this._testClass.Manager.ShouldBeSameAs(this._manager);
        this._testClass = new ServiceHostSetup(this._host, this._manager, this._environment, this._useSwagger);
        this._testClass.Manager.ShouldBeSameAs(this._manager);
        this._testClass = new ServiceHostSetup(this._host, this._manager, this._environment, this._apiVersions);
        this._testClass.Manager.ShouldBeSameAs(this._manager);
    }
}