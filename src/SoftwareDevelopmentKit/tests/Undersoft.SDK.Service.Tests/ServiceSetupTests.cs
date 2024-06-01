using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Mapper;

namespace Undersoft.SDK.Service.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServiceSetup"/>.
/// </summary>
[TestClass]
public partial class ServiceSetupTests
{
    private ServiceSetup _testClass;
    private IServiceCollection _services;
    private IConfiguration _configuration;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceSetup"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._services = Substitute.For<IServiceCollection>();
        this._configuration = Substitute.For<IConfiguration>();
        this._testClass = new ServiceSetup(this._services, this._configuration);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceSetup(this._services);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceSetup(this._services, this._configuration);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceSetup(default(IServiceCollection)));
        Should.Throw<ArgumentNullException>(() => new ServiceSetup(default(IServiceCollection), this._configuration));
    }

    /// <summary>
    /// Checks that instance construction throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceSetup(this._services, default(IConfiguration)));
    }

    /// <summary>
    /// Checks that the AddCaching method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddCaching()
    {
        // Act
        var result = this._testClass.AddCaching();

        // Assert
        Assert.Fail("Create or modify test");
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
    /// Checks that the AddJsonSerializerDefaults method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddJsonSerializerDefaults()
    {
        // Act
        this._testClass.AddJsonSerializerDefaults();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddLogging method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddLogging()
    {
        // Act
        var result = this._testClass.AddLogging();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddPropertyInjection method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddPropertyInjection()
    {
        // Act
        var result = this._testClass.AddPropertyInjection();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddImplementations method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddImplementations()
    {
        // Act
        var result = this._testClass.AddImplementations();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddMapper method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddMapperWithProfiles()
    {
        // Arrange
        var profiles = new[] { new MapperProfile(), new MapperProfile(), new MapperProfile() };

        // Act
        var result = this._testClass.AddMapper(profiles);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddMapper method throws when the profiles parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddMapperWithProfilesWithNullProfiles()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AddMapper(default(MapperProfile[])));
    }

    /// <summary>
    /// Checks that the AddMapper method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddMapperWithMapper()
    {
        // Arrange
        var mapper = Substitute.For<IDataMapper>();

        // Act
        var result = this._testClass.AddMapper(mapper);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddMapper method throws when the mapper parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddMapperWithMapperWithNullMapper()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AddMapper(default(IDataMapper)));
    }

    /// <summary>
    /// Checks that the AddRepositoryClients method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddRepositoryClientsWithNoParameters()
    {
        // Act
        var result = this._testClass.AddRepositoryClients();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddRepositoryClients method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddRepositoryClientsWithServiceTypes()
    {
        // Arrange
        var serviceTypes = new[] { typeof(string), typeof(string), typeof(string) };

        // Act
        var result = this._testClass.AddRepositoryClients(serviceTypes);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddRepositoryClients method throws when the serviceTypes parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddRepositoryClientsWithServiceTypesWithNullServiceTypes()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AddRepositoryClients(default(Type[])));
    }

    /// <summary>
    /// Checks that the ConfigureServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigureServices()
    {
        // Arrange
        var clientTypes = new[] { typeof(string), typeof(string), typeof(string) };

        // Act
        var result = this._testClass.ConfigureServices(clientTypes);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MergeServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMergeServices()
    {
        // Act
        var result = this._testClass.MergeServices();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddStoreCache method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddStoreCacheWithTstore()
    {
        // Arrange
        var tstore = typeof(string);

        // Act
        var result = this._testClass.AddStoreCache(tstore);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddStoreCache method throws when the tstore parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddStoreCacheWithTstoreWithNullTstore()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AddStoreCache(default(Type)));
    }

    /// <summary>
    /// Checks that the Services property is initialized correctly by the constructors.
    /// </summary>
    [TestMethod]
    public void ServicesIsInitializedCorrectly()
    {
        this._testClass = new ServiceSetup(this._services);
        this._testClass.Services.ShouldBeSameAs(this._services);
        this._testClass = new ServiceSetup(this._services, this._configuration);
        this._testClass.Services.ShouldBeSameAs(this._services);
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