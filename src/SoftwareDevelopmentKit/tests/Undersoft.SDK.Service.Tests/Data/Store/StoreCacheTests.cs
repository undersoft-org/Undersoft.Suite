using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK;
using Undersoft.SDK.Invoking;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Data.Cache;
using Undersoft.SDK.Service.Data.Mapper;
using Undersoft.SDK.Service.Data.Store;
using TStore = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Store;

/// <summary>
/// Unit tests for the type <see cref="StoreCache"/>.
/// </summary>
[TestClass]
public class StoreCache_1Tests
{
    private StoreCache<TStore> _testClass;
    private IDataCache _cache;
    private TimeSpan? _lifeTime;
    private Invoker _callback;
    private int _capacity;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="StoreCache"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._cache = Substitute.For<IDataCache>();
        this._lifeTime = TimeSpan.FromSeconds(138);
        this._callback = new Invoker();
        this._capacity = 84803254;
        this._testClass = new StoreCache<TStore>(this._lifeTime, this._callback, this._capacity);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new StoreCache<TStore>(this._cache);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new StoreCache<TStore>(this._lifeTime, this._callback, this._capacity);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the cache parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullCache()
    {
        Should.Throw<ArgumentNullException>(() => new StoreCache<TStore>(default(IDataCache)));
    }

    /// <summary>
    /// Checks that the Catalog property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetCatalog()
    {
        // Assert
        this._testClass.Catalog.ShouldBeOfType<ITypedSeries<IIdentifiable>>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Mapper property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMapper()
    {
        // Arrange
        var testValue = Substitute.For<IDataMapper>();

        // Act
        this._testClass.Mapper = testValue;

        // Assert
        this._testClass.Mapper.ShouldBeSameAs(testValue);
    }
}