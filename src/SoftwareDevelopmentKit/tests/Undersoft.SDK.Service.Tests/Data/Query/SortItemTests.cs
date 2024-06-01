using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Tests.Data.Query;

/// <summary>
/// Unit tests for the type <see cref="SortItem"/>.
/// </summary>
[TestClass]
public class SortItemTests
{
    private SortItem _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="SortItem"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new SortItem();
    }

    /// <summary>
    /// Checks that the Direction property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDirection()
    {
        // Arrange
        var testValue = "TestValue1810383718";

        // Act
        this._testClass.Direction = testValue;

        // Assert
        this._testClass.Direction.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Property property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProperty()
    {
        // Arrange
        var testValue = "TestValue504079887";

        // Act
        this._testClass.Property = testValue;

        // Assert
        this._testClass.Property.ShouldBe(testValue);
    }
}