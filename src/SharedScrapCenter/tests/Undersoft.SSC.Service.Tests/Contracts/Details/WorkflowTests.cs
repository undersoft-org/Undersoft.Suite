using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="LambdaQuery"/>.
/// </summary>
[TestClass]
public class LambdaQueryTests
{
    private LambdaQuery _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="LambdaQuery"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new LambdaQuery();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new LambdaQuery();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that setting the Query property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetQuery()
    {
        this._testClass.CheckProperty(x => x.Query, new QuerySet
        {
            FilterItems = new List<FilterItem>(),
            SortItems = new List<SortItem>()
        }, new QuerySet
        {
            FilterItems = new List<FilterItem>(),
            SortItems = new List<SortItem>()
        });
    }

    /// <summary>
    /// Checks that setting the Name property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        this._testClass.CheckProperty(x => x.Name);
    }

    /// <summary>
    /// Checks that setting the Description property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDescription()
    {
        this._testClass.CheckProperty(x => x.Description);
    }
}