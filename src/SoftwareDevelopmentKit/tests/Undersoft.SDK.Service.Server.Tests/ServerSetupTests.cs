using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServerSetup"/>.
/// </summary>
[TestClass]
public partial class ServerSetupTests
{
    private ServerSetup _testClass;
    private IServiceCollection _services;
    private IMvcBuilder _mvcBuilder;
    private IConfiguration _configuration;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServerSetup"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._services = Substitute.For<IServiceCollection>();
        this._mvcBuilder = Substitute.For<IMvcBuilder>();
        this._configuration = Substitute.For<IConfiguration>();
        this._testClass = new ServerSetup(this._services, this._mvcBuilder);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServerSetup(this._services, this._mvcBuilder);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServerSetup(this._services, this._configuration);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => new ServerSetup(default(IServiceCollection), this._mvcBuilder));
        Should.Throw<ArgumentNullException>(() => new ServerSetup(default(IServiceCollection), this._configuration));
    }

    /// <summary>
    /// Checks that instance construction throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => new ServerSetup(this._services, default(IConfiguration)));
    }


    /// <summary>
    /// Checks that the AddSourceProviderConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddSourceProviderConfiguration()
    {
        // Act
        var result = this._testClass.AddSourceProviderConfiguration();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddHealthChecks method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddHealthChecks()
    {
        // Act
        var result = this._testClass.AddHealthChecks();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddOpenTelemetry method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddOpenTelemetry()
    {
        // Act
        var result = this._testClass.AddOpenTelemetry();

        // Assert
        Assert.Fail("Create or modify test");
    }



    /// <summary>
    /// Checks that the AddAuthentication method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddAuthentication()
    {
        // Act
        var result = this._testClass.AddAuthentication();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddAuthorization method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddAuthorization()
    {
        // Act
        var result = this._testClass.AddAuthorization();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddSwagger method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddSwagger()
    {
        // Act
        var result = this._testClass.AddSwagger();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddRepositorySources method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddRepositorySourcesWithNoParameters()
    {
        // Act
        var result = this._testClass.AddRepositorySources();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddRepositorySources method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddRepositorySourcesWithStoreTypes()
    {
        // Arrange
        var storeTypes = new[] { typeof(string), typeof(string), typeof(string) };

        // Act
        var result = this._testClass.AddRepositorySources(storeTypes);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddRepositorySources method throws when the storeTypes parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddRepositorySourcesWithStoreTypesWithNullStoreTypes()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AddRepositorySources(default(Type[])));
    }

    /// <summary>
    /// Checks that the AddApiVersions method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddApiVersions()
    {
        // Arrange
        var apiVersions = new[] { "TestValue886161328", "TestValue1253757374", "TestValue155267445" };

        // Act
        var result = this._testClass.AddApiVersions(apiVersions);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddApiVersions method throws when the apiVersions parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddApiVersionsWithNullApiVersions()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AddApiVersions(default(string[])));
    }

    /// <summary>
    /// Checks that the ConfigureServer method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigureServer()
    {
        // Arrange
        var includeSwagger = true;
        var sourceTypes = new[] { typeof(string), typeof(string), typeof(string) };
        var clientTypes = new[] { typeof(string), typeof(string), typeof(string) };

        // Act
        var result = this._testClass.ConfigureServer(includeSwagger, sourceTypes, clientTypes);

        // Assert
        Assert.Fail("Create or modify test");
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
}