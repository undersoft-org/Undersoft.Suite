using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Store;
using TEntity = Undersoft.SDK.Origin;
using TStore = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Entity;

/// <summary>
/// Unit tests for the type <see cref="EntityCache"/>.
/// </summary>
[TestClass]
public class EntityCache_2Tests
{
    private EntityCache<TStore, TEntity> _testClass;
    private IStoreCache<TStore> _datacache;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EntityCache"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._datacache = Substitute.For<IStoreCache<TStore>>();
        this._testClass = new EntityCache<TStore, TEntity>(this._datacache);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EntityCache<TStore, TEntity>(this._datacache);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the datacache parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullDatacache()
    {
        Should.Throw<ArgumentNullException>(() => new EntityCache<TStore, TEntity>(default(IStoreCache<TStore>)));
    }

    /// <summary>
    /// Checks that the CacheSet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCacheSet()
    {
        // Act
        var result = this._testClass.CacheSet();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Lookup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLookupWithKeys()
    {
        // Arrange
        var keys = new object();

        // Act
        var result = this._testClass.Lookup(keys);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Lookup method throws when the keys parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLookupWithKeysWithNullKeys()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(default(object)));
    }

    /// <summary>
    /// Checks that the Lookup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLookupWithValueNamePair()
    {
        // Arrange
        var valueNamePair = new Tuple<string, object>("TestValue2140470124", new object());

        // Act
        var result = this._testClass.Lookup(valueNamePair);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Lookup method throws when the valueNamePair parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLookupWithValueNamePairWithNullValueNamePair()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(default(Tuple<string, object>)));
    }

    /// <summary>
    /// Checks that the Lookup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLookupWithSelector()
    {
        // Arrange
        Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>> selector = x => Substitute.For<ISeries<IIdentifiable>>();

        // Act
        var result = this._testClass.Lookup(selector);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Lookup method throws when the selector parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLookupWithSelectorWithNullSelector()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(default(Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>>)));
    }

    /// <summary>
    /// Checks that the Lookup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLookupWithTEntity()
    {
        // Arrange
        var item = new Origin();

        // Act
        var result = this._testClass.Lookup(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Lookup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLookupWithKeyAndValueNamePairs()
    {
        // Arrange
        var key = new[] { new object(), new object(), new object() };
        var valueNamePairs = new[] { new Tuple<string, object>("TestValue1670272955", new object()), new Tuple<string, object>("TestValue1362195077", new object()), new Tuple<string, object>("TestValue315223235", new object()) };

        // Act
        var result = this._testClass.Lookup(key, valueNamePairs);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Lookup method throws when the key parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLookupWithKeyAndValueNamePairsWithNullKey()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(default(object[]), new[] { new Tuple<string, object>("TestValue930499684", new object()), new Tuple<string, object>("TestValue2117965470", new object()), new Tuple<string, object>("TestValue1896012715", new object()) }));
    }

    /// <summary>
    /// Checks that the Lookup method throws when the valueNamePairs parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLookupWithKeyAndValueNamePairsWithNullValueNamePairs()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(new[] { new object(), new object(), new object() }, default(Tuple<string, object>[])));
    }

    /// <summary>
    /// Checks that the Lookup method throws when the selectors parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLookupWithKeyAndSelectorsWithNullSelectors()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(x => Substitute.For<IIdentifiable>(), default(Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>>[])));
    }

    /// <summary>
    /// Checks that the Lookup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLookupWithKeyAndPropertyNames()
    {
        // Arrange
        var key = new object();
        var propertyNames = "TestValue685647845";

        // Act
        var result = this._testClass.Lookup(key, propertyNames);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Lookup method throws when the key parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLookupWithKeyAndPropertyNamesWithNullKey()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(default(object), "TestValue2127122424"));
    }

    /// <summary>
    /// Checks that the Lookup method throws when the propertyNames parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallLookupWithKeyAndPropertyNamesWithInvalidPropertyNames(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(new object(), value));
    }

    /// <summary>
    /// Checks that the Lookup method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLookupWithItemAndPropertyNames()
    {
        // Arrange
        var item = new Origin();
        var propertyNames = new[] { "TestValue558016516", "TestValue794112253", "TestValue1205292129" };

        // Act
        var result = this._testClass.Lookup(item, propertyNames);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Lookup method throws when the propertyNames parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLookupWithItemAndPropertyNamesWithNullPropertyNames()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Lookup(new Origin(), default(string[])));
    }

    /// <summary>
    /// Checks that the Memorize method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMemorizeWithItems()
    {
        // Arrange
        var items = new[] { new Origin(), new Origin(), new Origin() };

        // Act
        var result = this._testClass.Memorize(items);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Memorize method throws when the items parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallMemorizeWithItemsWithNullItems()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Memorize(default(IEnumerable<TEntity>)));
    }

    /// <summary>
    /// Checks that the Memorize method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMemorizeWithTEntity()
    {
        // Arrange
        var item = new Origin();

        // Act
        var result = this._testClass.Memorize(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Memorize method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMemorizeWithTEntityAndArrayOfString()
    {
        // Arrange
        var item = new Origin();
        var names = new[] { "TestValue678831490", "TestValue1140939387", "TestValue2102176144" };

        // Act
        var result = this._testClass.Memorize(item, names);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Memorize method throws when the names parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallMemorizeWithTEntityAndArrayOfStringWithNullNames()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Memorize(new Origin(), default(string[])));
    }

    /// <summary>
    /// Checks that the MemorizeAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallMemorizeAsyncWithTEntityAsync()
    {
        // Arrange
        var item = new Origin();

        // Act
        var result = await this._testClass.MemorizeAsync(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MemorizeAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallMemorizeAsyncWithTEntityAndArrayOfStringAsync()
    {
        // Arrange
        var item = new Origin();
        var names = new[] { "TestValue655076451", "TestValue2144104227", "TestValue1173826366" };

        // Act
        var result = await this._testClass.MemorizeAsync(item, names);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MemorizeAsync method throws when the names parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallMemorizeAsyncWithTEntityAndArrayOfStringWithNullNamesAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.MemorizeAsync(new Origin(), default(string[])));
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
    /// Checks that the TypeId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTypeId()
    {
        // Arrange
        var testValue = 873283248L;

        // Act
        this._testClass.TypeId = testValue;

        // Assert
        this._testClass.TypeId.ShouldBe(testValue);
    }
}