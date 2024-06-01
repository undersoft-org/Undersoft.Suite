using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Configuration;

namespace Undersoft.SDK.Service.Tests.Configuration;

/// <summary>
/// Unit tests for the type <see cref="ServiceConfigurationContext"/>.
/// </summary>
[TestClass]
public class ServiceConfigurationContextTests
{
    private ServiceConfigurationContext _testClass;
    private IServiceRegistry _services;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceConfigurationContext"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._services = Substitute.For<IServiceRegistry>();
        this._testClass = new ServiceConfigurationContext(this._services);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceConfigurationContext(this._services);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceConfigurationContext(default(IServiceRegistry)));
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
    /// Checks that the Items property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetItems()
    {
        // Assert
        this._testClass.Items.ShouldBeOfType<ISeries<object>>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the indexer functions correctly.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIndexer()
    {
        var testValue = new object();
        this._testClass["TestValue493079366"].ShouldBeOfType<object>();
        this._testClass["TestValue493079366"] = testValue;
        this._testClass["TestValue493079366"].ShouldBeSameAs(testValue);
    }
}