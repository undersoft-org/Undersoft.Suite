using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Configuration;
using T = System.String;

namespace Undersoft.SDK.Service.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServiceManager"/>.
/// </summary>
[TestClass]
public class ServiceManagerTests
{
    private ServiceManager _testClass;
    private IServiceManager _serviceManager;
    private IServiceCollection _services;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceManager"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._serviceManager = Substitute.For<IServiceManager>();
        this._services = Substitute.For<IServiceCollection>();
        this._testClass = new ServiceManager(this._serviceManager);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceManager();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceManager(this._serviceManager);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the serviceManager parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServiceManager()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceManager(default(IServiceManager)));
    }

    /// <summary>
    /// Checks that the BuildServiceProviderFactory method throws when the registry parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallBuildServiceProviderFactoryWithNullRegistry()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.BuildServiceProviderFactory(default(IServiceRegistry)));
    }

    /// <summary>
    /// Checks that the GetRootService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootServiceWithT()
    {
        // Act
        var result = this._testClass.GetRootService<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRootServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootServices()
    {
        // Act
        var result = this._testClass.GetRootServices<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRequiredRootService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRequiredRootService()
    {
        // Act
        var result = this._testClass.GetRequiredRootService<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRootService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootServiceWithType()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = this._testClass.GetRootService(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRootService method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetRootServiceWithTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetRootService(default(Type)));
    }

    /// <summary>
    /// Checks that the GetService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetServiceWithT()
    {
        // Act
        var result = this._testClass.GetService<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetServicesWithT()
    {
        // Act
        var result = this._testClass.GetServices<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRequiredService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRequiredServiceWithT()
    {
        // Act
        var result = this._testClass.GetRequiredService<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetServiceWithType()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = this._testClass.GetService(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetService method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetServiceWithTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetService(default(Type)));
    }

    /// <summary>
    /// Checks that the GetServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetServicesWithType()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = this._testClass.GetServices(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetServices method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetServicesWithTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetServices(default(Type)));
    }

    /// <summary>
    /// Checks that the GetRequiredServiceLazy method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRequiredServiceLazy()
    {
        // Act
        var result = this._testClass.GetRequiredServiceLazy<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetServiceLazy method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetServiceLazy()
    {
        // Act
        var result = this._testClass.GetServiceLazy<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetServicesLazy method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetServicesLazy()
    {
        // Act
        var result = this._testClass.GetServicesLazy<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetSingleton method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetSingletonWithT()
    {
        // Act
        var result = this._testClass.GetSingleton<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetSingleton method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetSingletonWithTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetSingleton(default(Type)));
    }

    /// <summary>
    /// Checks that the GetRequiredService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRequiredServiceWithType()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = this._testClass.GetRequiredService(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRequiredService method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetRequiredServiceWithTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetRequiredService(default(Type)));
    }

    /// <summary>
    /// Checks that the InitializeRootService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallInitializeRootService()
    {
        // Arrange
        var parameters = new[] { new object(), new object(), new object() };

        // Act
        var result = this._testClass.InitializeRootService<T>(parameters);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the InitializeRootService method throws when the parameters parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallInitializeRootServiceWithNullParameters()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.InitializeRootService<T>(default(object[])));
    }

    /// <summary>
    /// Checks that the EnsureGetRootService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallEnsureGetRootService()
    {
        // Act
        var result = this._testClass.EnsureGetRootService<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SetProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSetProvider()
    {
        // Arrange
        var serviceProvider = Substitute.For<IServiceProvider>();

        // Act
        ServiceManager.SetProvider(serviceProvider);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SetProvider method throws when the serviceProvider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSetProviderWithNullServiceProvider()
    {
        Should.Throw<ArgumentNullException>(() => ServiceManager.SetProvider(default(IServiceProvider)));
    }

    /// <summary>
    /// Checks that the BuildInternalRootProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallBuildInternalRootProvider()
    {
        // Arrange
        var withPropertyInjection = false;

        // Act
        var result = ServiceManager.BuildInternalRootProvider(withPropertyInjection);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRootProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootProvider()
    {
        // Act
        var result = ServiceManager.GetRootProvider();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetProviderFactory method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetProviderFactory()
    {
        // Act
        var result = this._testClass.GetProviderFactory();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CreateProviderFromFacotry method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreateProviderFromFacotry()
    {
        // Act
        var result = this._testClass.CreateProviderFromFacotry();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRootProviderFactory method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootProviderFactory()
    {
        // Act
        var result = ServiceManager.GetRootProviderFactory();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CreateFactory method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreateFactoryWithConstrTypes()
    {
        // Arrange
        var constrTypes = new[] { typeof(string), typeof(string), typeof(string) };

        // Act
        var result = this._testClass.CreateFactory<T>(constrTypes);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CreateFactory method throws when the constrTypes parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCreateFactoryWithConstrTypesWithNullConstrTypes()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.CreateFactory<T>(default(Type[])));
    }

    /// <summary>
    /// Checks that the CreateFactory method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreateFactoryWithInstanceTypeAndConstrTypes()
    {
        // Arrange
        var instanceType = typeof(string);
        var constrTypes = new[] { typeof(string), typeof(string), typeof(string) };

        // Act
        var result = this._testClass.CreateFactory(instanceType, constrTypes);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CreateFactory method throws when the instanceType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCreateFactoryWithInstanceTypeAndConstrTypesWithNullInstanceType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.CreateFactory(default(Type), new[] { typeof(string), typeof(string), typeof(string) }));
    }

    /// <summary>
    /// Checks that the CreateFactory method throws when the constrTypes parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCreateFactoryWithInstanceTypeAndConstrTypesWithNullConstrTypes()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.CreateFactory(typeof(string), default(Type[])));
    }

    /// <summary>
    /// Checks that the Initialize method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallInitializeWithBesidesInjectedArguments()
    {
        // Arrange
        var besidesInjectedArguments = new[] { new object(), new object(), new object() };

        // Act
        var result = this._testClass.Initialize<T>(besidesInjectedArguments);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Initialize method throws when the besidesInjectedArguments parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallInitializeWithBesidesInjectedArgumentsWithNullBesidesInjectedArguments()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Initialize<T>(default(object[])));
    }

    /// <summary>
    /// Checks that the Initialize method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallInitializeWithTypeAndBesidesInjectedArguments()
    {
        // Arrange
        var @type = typeof(string);
        var besidesInjectedArguments = new[] { new object(), new object(), new object() };

        // Act
        var result = this._testClass.Initialize(type, besidesInjectedArguments);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Initialize method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallInitializeWithTypeAndBesidesInjectedArgumentsWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Initialize(default(Type), new[] { new object(), new object(), new object() }));
    }

    /// <summary>
    /// Checks that the Initialize method throws when the besidesInjectedArguments parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallInitializeWithTypeAndBesidesInjectedArgumentsWithNullBesidesInjectedArguments()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Initialize(typeof(string), default(object[])));
    }

    /// <summary>
    /// Checks that the EnsureGetService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallEnsureGetServiceWithT()
    {
        // Act
        var result = this._testClass.EnsureGetService<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the EnsureGetService method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallEnsureGetServiceWithTAndType()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = this._testClass.EnsureGetService<T>(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the EnsureGetService method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallEnsureGetServiceWithTAndTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.EnsureGetService<T>(default(Type)));
    }

    /// <summary>
    /// Checks that the AddObject method throws when the obj parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddObjectWithTAndTWithNullObj()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AddObject<T>(default(T)));
    }


    /// <summary>
    /// Checks that the GetRootObject method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootObject()
    {
        // Act
        var result = ServiceManager.GetRootObject<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddRootObject method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddRootObjectWithTAndT()
    {
        // Arrange
        var obj = "TestValue477667013";

        // Act
        var result = ServiceManager.AddRootObject<T>(obj);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddRootObject method throws when the obj parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddRootObjectWithTAndTWithNullObj()
    {
        Should.Throw<ArgumentNullException>(() => ServiceManager.AddRootObject<T>(default(T)));
    }


    /// <summary>
    /// Checks that the CreateRootSession method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreateRootSession()
    {
        // Act
        var result = ServiceManager.CreateRootSession();

        // Assert
        Assert.Fail("Create or modify test");
    }


    /// <summary>
    /// Checks that the GetRootManager method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootManager()
    {
        // Act
        var result = ServiceManager.GetRootManager();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetManager method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetManager()
    {
        // Act
        var result = this._testClass.GetManager();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRootRegistry method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootRegistry()
    {
        // Act
        var result = ServiceManager.GetRootRegistry();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRegistry method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRegistryWithNoParameters()
    {
        // Act
        var result = this._testClass.GetRegistry();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRegistry method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRegistryWithServices()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();

        // Act
        var result = this._testClass.GetRegistry(services);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRegistry method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetRegistryWithServicesWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetRegistry(default(IServiceCollection)));
    }

    /// <summary>
    /// Checks that the GetRegistry maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void GetRegistryWithServicesPerformsMapping()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();

        // Act
        var result = this._testClass.GetRegistry(services);

        // Assert
        result.Services.ShouldBeSameAs(services);
    }

    /// <summary>
    /// Checks that the GetRootConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRootConfiguration()
    {
        // Act
        var result = ServiceManager.GetRootConfiguration();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetConfiguration()
    {
        // Act
        var result = this._testClass.GetConfiguration();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the DisposeAsyncCore method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallDisposeAsyncCoreAsync()
    {
        // Act
        await this._testClass.DisposeAsyncCore();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the RootProvider property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetRootProvider()
    {
        // Assert
        this._testClass.RootProvider.ShouldBeOfType<IServiceProvider>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configuration property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetConfiguration()
    {
        // Arrange
        var testValue = Substitute.For<IServiceConfiguration>();

        // Act
        this._testClass.Configuration = testValue;

        // Assert
        this._testClass.Configuration.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Registry property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetRegistry()
    {
        // Assert
        this._testClass.Registry.ShouldBeOfType<IServiceRegistry>();

        Assert.Fail("Create or modify test");
    }
}