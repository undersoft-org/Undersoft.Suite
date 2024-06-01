using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Tests.Data.Query;

/// <summary>
/// Unit tests for the type <see cref="FilterItem"/>.
/// </summary>
[TestClass]
public class FilterItemTests
{
    private FilterItem _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="FilterItem"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new FilterItem();
    }

    /// <summary>
    /// Checks that the Property property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProperty()
    {
        // Arrange
        var testValue = "TestValue498354766";

        // Act
        this._testClass.Property = testValue;

        // Assert
        this._testClass.Property.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Operand property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOperand()
    {
        // Arrange
        var testValue = "TestValue318036133";

        // Act
        this._testClass.Operand = testValue;

        // Assert
        this._testClass.Operand.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Data property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetData()
    {
        // Arrange
        var testValue = "TestValue63556357";

        // Act
        this._testClass.Data = testValue;

        // Assert
        this._testClass.Data.ShouldBe(testValue);
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
    /// Checks that the Type property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetType()
    {
        // Arrange
        var testValue = "TestValue427210924";

        // Act
        this._testClass.Type = testValue;

        // Assert
        this._testClass.Type.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Logic property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLogic()
    {
        // Arrange
        var testValue = "TestValue214753545";

        // Act
        this._testClass.Logic = testValue;

        // Assert
        this._testClass.Logic.ShouldBe(testValue);
    }
}