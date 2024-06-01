using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Tests.Data.Query;

/// <summary>
/// Unit tests for the type <see cref="QuerySet"/>.
/// </summary>
[TestClass]
public class QuerySetTests
{
    private QuerySet _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="QuerySet"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new QuerySet();
    }

    /// <summary>
    /// Checks that the FilterItems property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetFilterItems()
    {
        // Arrange
        var testValue = new List<FilterItem>();

        // Act
        this._testClass.FilterItems = testValue;

        // Assert
        this._testClass.FilterItems.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the SortItems property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSortItems()
    {
        // Arrange
        var testValue = new List<SortItem>();

        // Act
        this._testClass.SortItems = testValue;

        // Assert
        this._testClass.SortItems.ShouldBeSameAs(testValue);
    }
}