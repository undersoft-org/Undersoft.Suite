using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Query;
using TEntity = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Query;

/// <summary>
/// Unit tests for the type <see cref="FilterExpression"/>.
/// </summary>
[TestClass]
public class FilterExpression_1Tests
{
    private FilterExpression<TEntity> _testClass;
    private Filter<TEntity>[] _filterItemsUnknownType;
    private IEnumerable<Filter<TEntity>> _filterItemsIEnumerableFilterTEntity;
    private IEnumerable<FilterItem> _filterItemsIEnumerableFilterItem;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="FilterExpression"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._filterItemsUnknownType = new[] { new Filter<TEntity>(), new Filter<TEntity>(), new Filter<TEntity>() };
        this._filterItemsIEnumerableFilterTEntity = new[] { new Filter<TEntity>(), new Filter<TEntity>(), new Filter<TEntity>() };
        this._filterItemsIEnumerableFilterItem = new[] {
            new FilterItem
            {
                Property = "TestValue1224706171",
                Operand = "TestValue950435207",
                Data = "TestValue768060619",
                Value = new object(),
                Type = "TestValue654348385",
                Logic = "TestValue1808265128"
            },
            new FilterItem
            {
                Property = "TestValue1044589812",
                Operand = "TestValue1546199992",
                Data = "TestValue1276840580",
                Value = new object(),
                Type = "TestValue1372158349",
                Logic = "TestValue1652651827"
            },
            new FilterItem
            {
                Property = "TestValue67496983",
                Operand = "TestValue1212267105",
                Data = "TestValue1093120663",
                Value = new object(),
                Type = "TestValue321423418",
                Logic = "TestValue1214701026"
            }
        };
        this._testClass = new FilterExpression<TEntity>(this._filterItemsUnknownType);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new FilterExpression<TEntity>();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new FilterExpression<TEntity>(this._filterItemsUnknownType);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new FilterExpression<TEntity>(this._filterItemsIEnumerableFilterTEntity);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new FilterExpression<TEntity>(this._filterItemsIEnumerableFilterItem);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the filterItems parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullFilterItems()
    {
        Should.Throw<ArgumentNullException>(() => new FilterExpression<TEntity>(default(Filter<TEntity>[])));
        Should.Throw<ArgumentNullException>(() => new FilterExpression<TEntity>(default(IEnumerable<Filter<TEntity>>)));
        Should.Throw<ArgumentNullException>(() => new FilterExpression<TEntity>(default(IEnumerable<FilterItem>)));
    }

    /// <summary>
    /// Checks that the Create method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreateWithNoParameters()
    {
        // Act
        var result = this._testClass.Create();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Create method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreateWithIEnumerableOfFilterOfTEntity()
    {
        // Arrange
        var filterItems = new[] { new Filter<TEntity>(), new Filter<TEntity>(), new Filter<TEntity>() };

        // Act
        var result = this._testClass.Create(filterItems);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Create method throws when the filterItems parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCreateWithIEnumerableOfFilterOfTEntityWithNullFilterItems()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Create(default(IEnumerable<Filter<TEntity>>)));
    }


    /// <summary>
    /// Checks that the Add method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddWithItem()
    {
        // Arrange
        var item = new Filter<TEntity>();

        // Act
        var result = this._testClass.Add(item);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Add method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddWithItemWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Add(default(Filter<TEntity>)));
    }

    /// <summary>
    /// Checks that the Add method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddWithIEnumerableOfFilterOfTEntity()
    {
        // Arrange
        var filterItems = new[] { new Filter<TEntity>(), new Filter<TEntity>(), new Filter<TEntity>() };

        // Act
        var result = this._testClass.Add(filterItems);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Add method throws when the filterItems parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddWithIEnumerableOfFilterOfTEntityWithNullFilterItems()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Add(default(IEnumerable<Filter<TEntity>>)));
    }

    /// <summary>
    /// Checks that the FilterItems property is initialized correctly by the constructors.
    /// </summary>
    [TestMethod]
    public void FilterItemsIsInitializedCorrectly()
    {
        this._testClass = new FilterExpression<TEntity>(this._filterItemsUnknownType);
        this._testClass.FilterItems.ShouldBeSameAs(this._filterItemsUnknownType);
        this._testClass = new FilterExpression<TEntity>(this._filterItemsIEnumerableFilterTEntity);
        this._testClass.FilterItems.ShouldBeSameAs(this._filterItemsUnknownType);
        this._testClass = new FilterExpression<TEntity>(this._filterItemsIEnumerableFilterItem);
        this._testClass.FilterItems.ShouldBeSameAs(this._filterItemsUnknownType);
    }
}