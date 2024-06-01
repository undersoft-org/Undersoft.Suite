using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq.Expressions;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Service.Data.Query;
using SortDirection = System.Linq.SortDirection;
using TEntity = Undersoft.SDK.Service.Data.Entity.Entity;

namespace Undersoft.SDK.Service.Tests.Data.Query;

/// <summary>
/// Unit tests for the type <see cref="Sort"/>.
/// </summary>
[TestClass]
public class Sort_1Tests
{
    private Sort<TEntity> _testClass;
    private Expression<Func<TEntity, object>> _expressionItem;
    private SortDirection _directionSortDirection;
    private MemberRubric _sortedRubric;
    private string _rubricName;
    private string _directionString;
    private SortItem _item;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Sort"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._directionSortDirection = SortDirection.Ascending;
        this._sortedRubric = new MemberRubric();
        this._rubricName = "TestValue647003631";
        this._directionString = "TestValue1669687337";
        this._item = new SortItem
        {
            Direction = "TestValue861958714",
            Property = "TestValue933087644"
        };
        this._testClass = new Sort<TEntity>(this._expressionItem, this._directionSortDirection);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Sort<TEntity>();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Sort<TEntity>(this._expressionItem, this._directionSortDirection);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Sort<TEntity>(this._sortedRubric, this._directionSortDirection);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Sort<TEntity>(this._rubricName, this._directionString);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Sort<TEntity>(this._item);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the expressionItem parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullExpressionItem()
    {
        Should.Throw<ArgumentNullException>(() => new Sort<TEntity>(default(Expression<Func<TEntity, object>>), this._directionSortDirection));
    }

    /// <summary>
    /// Checks that instance construction throws when the sortedRubric parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullSortedRubric()
    {
        Should.Throw<ArgumentNullException>(() => new Sort<TEntity>(default(MemberRubric), this._directionSortDirection));
    }

    /// <summary>
    /// Checks that instance construction throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => new Sort<TEntity>(default(SortItem)));
    }

    /// <summary>
    /// Checks that the constructor throws when the rubricName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidRubricName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new Sort<TEntity>(value, this._directionString));
    }

    /// <summary>
    /// Checks that the constructor throws when the direction parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidDirection(string value)
    {
        Should.Throw<ArgumentNullException>(() => new Sort<TEntity>(this._rubricName, value));
    }

    /// <summary>
    /// Checks that the Assign method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAssign()
    {
        // Arrange
        var sortExpression = new SortExpression<TEntity>();

        // Act
        this._testClass.Assign(sortExpression);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Assign method throws when the sortExpression parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAssignWithNullSortExpression()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Assign(default(SortExpression<TEntity>)));
    }

    /// <summary>
    /// Checks that the Compare method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCompare()
    {
        // Arrange
        var term = new Sort<TEntity>();

        // Act
        var result = this._testClass.Compare(term);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Compare method throws when the term parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCompareWithNullTerm()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Compare(default(Sort<TEntity>)));
    }

    /// <summary>
    /// Checks that the ExpressionItem property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ExpressionItemIsInitializedCorrectly()
    {
        this._testClass.ExpressionItem.ShouldBeSameAs(this._expressionItem);
    }

    /// <summary>
    /// Checks that the Direction property is initialized correctly by the constructors.
    /// </summary>
    [TestMethod]
    public void DirectionIsInitializedCorrectly()
    {
        this._testClass = new Sort<TEntity>(this._expressionItem, this._directionSortDirection);
        this._testClass.Direction.ShouldBe(this._directionSortDirection);
        this._testClass = new Sort<TEntity>(this._sortedRubric, this._directionSortDirection);
        this._testClass.Direction.ShouldBe(this._directionSortDirection);
        this._testClass = new Sort<TEntity>(this._rubricName, this._directionString);
        this._testClass.Direction.ShouldBe(this._directionSortDirection);
    }

    /// <summary>
    /// Checks that the Direction property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDirection()
    {
        // Arrange
        var testValue = SortDirection.Descending;

        // Act
        this._testClass.Direction = testValue;

        // Assert
        this._testClass.Direction.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Position property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPosition()
    {
        // Arrange
        var testValue = 1370355200;

        // Act
        this._testClass.Position = testValue;

        // Assert
        this._testClass.Position.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Property property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProperty()
    {
        // Arrange
        var testValue = "TestValue1086160826";

        // Act
        this._testClass.Property = testValue;

        // Assert
        this._testClass.Property.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Rubric property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRubric()
    {
        // Arrange
        var testValue = new MemberRubric();

        // Act
        this._testClass.Rubric = testValue;

        // Assert
        this._testClass.Rubric.ShouldBeSameAs(testValue);
    }
}