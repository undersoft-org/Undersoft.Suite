using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System.Linq.Expressions;
using Undersoft.SDK.Service.Data.Query;
using SortDirection = System.Linq.SortDirection;
using TEntity = Undersoft.SDK.Service.Data.Entity.IEntity;

namespace Undersoft.SDK.Service.Tests.Data.Query;

/// <summary>
/// Unit tests for the type <see cref="SortExpression"/>.
/// </summary>
[TestClass]
public class SortExpression_1Tests
{
    private SortExpression<TEntity> _testClass;
    private Expression<Func<TEntity, object>> _expressionItem;
    private SortDirection _direction;
    private Sort<TEntity>[] _sortItemsUnknownType;
    private IEnumerable<Sort<TEntity>> _sortItemsIEnumerableSortTEntity;
    private IEnumerable<SortItem> _sortItemsIEnumerableSortItem;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="SortExpression"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._direction = SortDirection.Ascending;
        this._sortItemsUnknownType = new[] { new Sort<TEntity>(), new Sort<TEntity>(), new Sort<TEntity>() };
        this._sortItemsIEnumerableSortTEntity = new[] { new Sort<TEntity>(), new Sort<TEntity>(), new Sort<TEntity>() };
        this._sortItemsIEnumerableSortItem = new[] {
            new SortItem
            {
                Direction = "TestValue1303234680",
                Property = "TestValue717209012"
            },
            new SortItem
            {
                Direction = "TestValue1737187482",
                Property = "TestValue704959227"
            },
            new SortItem
            {
                Direction = "TestValue674457176",
                Property = "TestValue90481487"
            }
        };
        this._testClass = new SortExpression<TEntity>(this._expressionItem, this._direction);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new SortExpression<TEntity>();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new SortExpression<TEntity>(this._expressionItem, this._direction);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new SortExpression<TEntity>(this._sortItemsUnknownType);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new SortExpression<TEntity>(this._sortItemsIEnumerableSortTEntity);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new SortExpression<TEntity>(this._sortItemsIEnumerableSortItem);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the expressionItem parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullExpressionItem()
    {
        Should.Throw<ArgumentNullException>(() => new SortExpression<TEntity>(default(Expression<Func<TEntity, object>>), this._direction));
    }

    /// <summary>
    /// Checks that instance construction throws when the sortItems parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullSortItems()
    {
        Should.Throw<ArgumentNullException>(() => new SortExpression<TEntity>(default(Sort<TEntity>[])));
        Should.Throw<ArgumentNullException>(() => new SortExpression<TEntity>(default(IEnumerable<Sort<TEntity>>)));
        Should.Throw<ArgumentNullException>(() => new SortExpression<TEntity>(default(IEnumerable<SortItem>)));
    }

    /// <summary>
    /// Checks that the Sort method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSortWithQuery()
    {
        // Arrange
        var query = Substitute.For<IQueryable<TEntity>>();

        // Act
        var result = this._testClass.Sort(query);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Sort method throws when the query parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSortWithQueryWithNullQuery()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Sort(default(IQueryable<TEntity>)));
    }

    /// <summary>
    /// Checks that the Sort method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSortWithQueryAndSortItems()
    {
        // Arrange
        var query = Substitute.For<IQueryable<TEntity>>();
        var sortItems = new[] { new Sort<TEntity>(), new Sort<TEntity>(), new Sort<TEntity>() };

        // Act
        var result = this._testClass.Sort(query, sortItems);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Sort method throws when the query parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSortWithQueryAndSortItemsWithNullQuery()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Sort(default(IQueryable<TEntity>), new[] { new Sort<TEntity>(), new Sort<TEntity>(), new Sort<TEntity>() }));
    }

    /// <summary>
    /// Checks that the Sort method throws when the sortItems parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSortWithQueryAndSortItemsWithNullSortItems()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Sort(Substitute.For<IQueryable<TEntity>>(), default(IEnumerable<Sort<TEntity>>)));
    }

    /// <summary>
    /// Checks that the Add method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddWithItem()
    {
        // Arrange
        var item = new Sort<TEntity>();

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
        Should.Throw<ArgumentNullException>(() => this._testClass.Add(default(Sort<TEntity>)));
    }

    /// <summary>
    /// Checks that the Add method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddWithSortItems()
    {
        // Arrange
        var sortItems = new[] { new Sort<TEntity>(), new Sort<TEntity>(), new Sort<TEntity>() };

        // Act
        var result = this._testClass.Add(sortItems);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Add method throws when the sortItems parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddWithSortItemsWithNullSortItems()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Add(default(IEnumerable<Sort<TEntity>>)));
    }

    /// <summary>
    /// Checks that the SortItems property is initialized correctly by the constructors.
    /// </summary>
    [TestMethod]
    public void SortItemsIsInitializedCorrectly()
    {
        this._testClass = new SortExpression<TEntity>(this._sortItemsUnknownType);
        this._testClass.SortItems.ShouldBeSameAs(this._sortItemsUnknownType);
        this._testClass = new SortExpression<TEntity>(this._sortItemsIEnumerableSortTEntity);
        this._testClass.SortItems.ShouldBeSameAs(this._sortItemsUnknownType);
        this._testClass = new SortExpression<TEntity>(this._sortItemsIEnumerableSortItem);
        this._testClass.SortItems.ShouldBeSameAs(this._sortItemsUnknownType);
    }
}