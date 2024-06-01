using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using T = System.String;
using TContext = System.String;
using TService = System.String;

namespace Undersoft.SDK.Service.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServiceRegistry"/>.
/// </summary>
[TestClass]
public partial class ServiceRegistryTests
{
    private ServiceRegistry _testClass;
    private IServiceCollection _services;
    private IServiceManager _manager;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceRegistry"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._services = Substitute.For<IServiceCollection>();
        this._manager = Substitute.For<IServiceManager>();
        this._testClass = new ServiceRegistry(this._services, this._manager);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceRegistry();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceRegistry(this._services, this._manager);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceRegistry(default(IServiceCollection), this._manager));
    }

    /// <summary>
    /// Checks that instance construction throws when the manager parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullManager()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceRegistry(this._services, default(IServiceManager)));
    }

    /// <summary>
    /// Checks that the Get method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetWithContextType()
    {
        // Arrange
        var contextType = typeof(string);

        // Act
        var result = this._testClass.Get(contextType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Get method throws when the contextType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetWithContextTypeWithNullContextType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Get(default(Type)));
    }

    /// <summary>
    /// Checks that the Get method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetWithTService()
    {
        // Act
        var result = this._testClass.Get<TService>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the TryGet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallTryGet()
    {
        // Act
        var result = this._testClass.TryGet<TService>(out var profile);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the TryAdd method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallTryAdd()
    {
        // Arrange
        var profile = new ServiceDescriptor(typeof(string), new object());

        // Act
        var result = this._testClass.TryAdd(profile);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the TryAdd method throws when the profile parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallTryAddWithNullProfile()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.TryAdd(default(ServiceDescriptor)));
    }

    /// <summary>
    /// Checks that the Remove method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRemoveWithTContext()
    {
        // Act
        var result = this._testClass.Remove<TContext>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Set method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSet()
    {
        // Arrange
        var descriptor = new ServiceDescriptor(typeof(string), new object());

        // Act
        var result = this._testClass.Set(descriptor);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Set method throws when the descriptor parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSetWithNullDescriptor()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Set(default(ServiceDescriptor)));
    }

    /// <summary>
    /// Checks that the Add method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAdd()
    {
        // Arrange
        var item = new ServiceDescriptor(typeof(string), new object());

        // Act
        this._testClass.Add(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Add method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Add(default(ServiceDescriptor)));
    }

    /// <summary>
    /// Checks that the GetKey method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetKeyWithServiceDescriptor()
    {
        // Arrange
        var item = new ServiceDescriptor(typeof(string), new object());

        // Act
        var result = this._testClass.GetKey(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetKey method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetKeyWithServiceDescriptorWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetKey(default(ServiceDescriptor)));
    }

    /// <summary>
    /// Checks that the GetKey method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetKeyWithType()
    {
        // Arrange
        var item = typeof(string);

        // Act
        var result = this._testClass.GetKey(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetKey method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetKeyWithTypeWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetKey(default(Type)));
    }

    /// <summary>
    /// Checks that the GetKey method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetKeyWithString()
    {
        // Arrange
        var item = "TestValue44313671";

        // Act
        var result = this._testClass.GetKey(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetKey method throws when the item parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetKeyWithStringWithInvalidItem(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetKey(value));
    }

    /// <summary>
    /// Checks that the GetKey method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetKeyWithT()
    {
        // Act
        var result = this._testClass.GetKey<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IndexOf method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallIndexOf()
    {
        // Arrange
        var item = new ServiceDescriptor(typeof(string), new object());

        // Act
        var result = this._testClass.IndexOf(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IndexOf method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallIndexOfWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.IndexOf(default(ServiceDescriptor)));
    }

    /// <summary>
    /// Checks that the Insert method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallInsert()
    {
        // Arrange
        var index = 1656808897;
        var item = new ServiceDescriptor(typeof(string), new object());

        // Act
        this._testClass.Insert(index, item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Insert method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallInsertWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Insert(1945319495, default(ServiceDescriptor)));
    }

    /// <summary>
    /// Checks that the Contains method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallContains()
    {
        // Arrange
        var item = new ServiceDescriptor(typeof(string), new object());

        // Act
        var result = this._testClass.Contains(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Contains method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallContainsWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Contains(default(ServiceDescriptor)));
    }

    /// <summary>
    /// Checks that the CopyTo method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCopyTo()
    {
        // Arrange
        var array = new[] { new ServiceDescriptor(typeof(string), new object()), new ServiceDescriptor(typeof(string), new object()), new ServiceDescriptor(typeof(string), new object()) };
        var arrayIndex = 2012376286;

        // Act
        this._testClass.CopyTo(array, arrayIndex);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CopyTo method throws when the array parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCopyToWithNullArray()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.CopyTo(default(ServiceDescriptor[]), 2139387416));
    }

    /// <summary>
    /// Checks that the Remove method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRemoveWithServiceDescriptor()
    {
        // Arrange
        var item = new ServiceDescriptor(typeof(string), new object());

        // Act
        var result = this._testClass.Remove(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Remove method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallRemoveWithServiceDescriptorWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Remove(default(ServiceDescriptor)));
    }

    /// <summary>
    /// Checks that the ContainsKey method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallContainsKeyWithTService()
    {
        // Act
        var result = this._testClass.ContainsKey<TService>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ContainsKey method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallContainsKeyWithType()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = this._testClass.ContainsKey(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ContainsKey method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallContainsKeyWithTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ContainsKey(default(Type)));
    }

    /// <summary>
    /// Checks that the MergeServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMergeServicesWithActualizeExternalServices()
    {
        // Arrange
        var actualizeExternalServices = true;

        // Act
        this._testClass.MergeServices(actualizeExternalServices);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MergeServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMergeServicesWithServicesAndActualizeExternalServices()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();
        var actualizeExternalServices = true;

        // Act
        this._testClass.MergeServices(services, actualizeExternalServices);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MergeServices method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallMergeServicesWithServicesAndActualizeExternalServicesWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.MergeServices(default(IServiceCollection), false));
    }

    /// <summary>
    /// Checks that the Services property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ServicesIsInitializedCorrectly()
    {
        this._testClass.Services.ShouldBeSameAs(this._services);
    }

    /// <summary>
    /// Checks that the Services property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServices()
    {
        // Arrange
        var testValue = Substitute.For<IServiceCollection>();

        // Act
        this._testClass.Services = testValue;

        // Assert
        this._testClass.Services.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Manager property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ManagerIsInitializedCorrectly()
    {
        this._testClass.Manager.ShouldBeSameAs(this._manager);
    }

    /// <summary>
    /// Checks that the indexer functions correctly.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIndexerForString()
    {
        var testValue = new ServiceDescriptor(typeof(string), new object());
        this._testClass["TestValue49708957"].ShouldBeOfType<ServiceDescriptor>();
        this._testClass["TestValue49708957"] = testValue;
        this._testClass["TestValue49708957"].ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the indexer functions correctly.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIndexerForType()
    {
        var testValue = new ServiceDescriptor(typeof(string), new object());
        this._testClass[typeof(string)].ShouldBeOfType<ServiceDescriptor>();
        this._testClass[typeof(string)] = testValue;
        this._testClass[typeof(string)].ShouldBeSameAs(testValue);
    }
}