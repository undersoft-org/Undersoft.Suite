using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq.Expressions;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Service.Data.Query;
using TEntity = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Query;

/// <summary>
/// Unit tests for the type <see cref="Filter"/>.
/// </summary>
[TestClass]
public class Filter_1Tests
{
    private Filter<TEntity> _testClass;
    private Expression<Func<TEntity, bool>> _expressionItem;
    private LogicOperand _linkOperand;
    private MemberRubric _rubric;
    private MathOperand _compareOperandMathOperand;
    private object _compareValue;
    private string _propertyName;
    private string _compareOperandString;
    private string _linkLogic;
    private FilterItem _item;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Filter"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._linkOperand = LogicOperand.And;
        this._rubric = new MemberRubric();
        this._compareOperandMathOperand = MathOperand.None;
        this._compareValue = new object();
        this._propertyName = "TestValue741954159";
        this._compareOperandString = "TestValue1302748593";
        this._linkLogic = "TestValue551690553";
        this._item = new FilterItem
        {
            Property = "TestValue1305875192",
            Operand = "TestValue877601097",
            Data = "TestValue631268780",
            Value = new object(),
            Type = "TestValue241417882",
            Logic = "TestValue820295230"
        };
        this._testClass = new Filter<TEntity>(this._rubric, this._compareOperandMathOperand, this._compareValue, this._linkOperand);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Filter<TEntity>();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Filter<TEntity>(this._expressionItem, this._linkOperand);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Filter<TEntity>(this._rubric, this._compareOperandMathOperand, this._compareValue, this._linkOperand);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Filter<TEntity>(this._propertyName, this._compareOperandMathOperand, this._compareValue, this._linkOperand);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Filter<TEntity>(this._propertyName, this._compareOperandString, this._compareValue, this._linkLogic);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Filter<TEntity>(this._item);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the expressionItem parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullExpressionItem()
    {
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(default(Expression<Func<TEntity, bool>>), this._linkOperand));
    }

    /// <summary>
    /// Checks that instance construction throws when the rubric parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullRubric()
    {
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(default(MemberRubric), this._compareOperandMathOperand, this._compareValue, this._linkOperand));
    }

    /// <summary>
    /// Checks that instance construction throws when the compareValue parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullCompareValue()
    {
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(this._rubric, this._compareOperandMathOperand, default(object), this._linkOperand));
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(this._propertyName, this._compareOperandMathOperand, default(object), this._linkOperand));
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(this._propertyName, this._compareOperandString, default(object), this._linkLogic));
    }

    /// <summary>
    /// Checks that instance construction throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(default(FilterItem)));
    }

    /// <summary>
    /// Checks that the constructor throws when the propertyName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidPropertyName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(value, this._compareOperandMathOperand, this._compareValue, this._linkOperand));
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(value, this._compareOperandString, this._compareValue, this._linkLogic));
    }

    /// <summary>
    /// Checks that the constructor throws when the linkLogic parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidLinkLogic(string value)
    {
        Should.Throw<ArgumentNullException>(() => new Filter<TEntity>(this._propertyName, this._compareOperandString, this._compareValue, value));
    }

    /// <summary>
    /// Checks that the Assign method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAssign()
    {
        // Arrange
        var filterExpression = new FilterExpression<TEntity>();

        // Act
        this._testClass.Assign(filterExpression);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Assign method throws when the filterExpression parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAssignWithNullFilterExpression()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Assign(default(FilterExpression<TEntity>)));
    }

    /// <summary>
    /// Checks that the Clone method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCloneWithNoParameters()
    {
        // Act
        var result = this._testClass.Clone();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Clone method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCloneWithValue()
    {
        // Arrange
        var value = new object();

        // Act
        var result = this._testClass.Clone(value);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Clone method throws when the value parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCloneWithValueWithNullValue()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Clone(default(object)));
    }

    /// <summary>
    /// Checks that the Clone maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void CloneWithValuePerformsMapping()
    {
        // Arrange
        var value = new object();

        // Act
        var result = this._testClass.Clone(value);

        // Assert
        result.Value.ShouldBeSameAs(value);
    }

    /// <summary>
    /// Checks that the Compare method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCompare()
    {
        // Arrange
        var term = new Filter<TEntity>();

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
        Should.Throw<ArgumentNullException>(() => this._testClass.Compare(default(Filter<TEntity>)));
    }

    /// <summary>
    /// Checks that the ExpressionItem property is initialized correctly by the constructors.
    /// </summary>
    [TestMethod]
    public void ExpressionItemIsInitializedCorrectly()
    {
        this._testClass = new Filter<TEntity>(this._expressionItem, this._linkOperand);
        this._testClass.ExpressionItem.ShouldBeSameAs(this._expressionItem);
    }

    /// <summary>
    /// Checks that the Rubric property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void RubricIsInitializedCorrectly()
    {
        this._testClass.Rubric.ShouldBeSameAs(this._rubric);
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

    /// <summary>
    /// Checks that the Property property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProperty()
    {
        // Arrange
        var testValue = "TestValue1253861417";

        // Act
        this._testClass.Property = testValue;

        // Assert
        this._testClass.Property.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PropertyType property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPropertyType()
    {
        // Arrange
        var testValue = typeof(string);

        // Act
        this._testClass.PropertyType = testValue;

        // Assert
        this._testClass.PropertyType.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Operand property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOperand()
    {
        // Arrange
        var testValue = MathOperand.NotLike;

        // Act
        this._testClass.Operand = testValue;

        // Assert
        this._testClass.Operand.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Value property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetValue()
    {
        // Arrange
        var testValue = new object();

        // Act
        this._testClass.Value = testValue;

        // Assert
        this._testClass.Value.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Logic property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLogic()
    {
        // Arrange
        var testValue = LogicOperand.And;

        // Act
        this._testClass.Logic = testValue;

        // Assert
        this._testClass.Logic.ShouldBe(testValue);
    }
}