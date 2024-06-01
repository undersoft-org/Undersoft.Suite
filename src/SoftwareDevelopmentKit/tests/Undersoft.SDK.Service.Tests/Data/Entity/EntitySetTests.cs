using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Entity;
using TDto = Undersoft.SDK.Origin;

namespace Undersoft.SDK.Service.Tests.Data.Entity;

/// <summary>
/// Unit tests for the type <see cref="EntitySet"/>.
/// </summary>
[TestClass]
public class EntitySet_1Tests
{
    private EntitySet<TDto> _testClass;
    private IEnumerable<TDto> _list;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EntitySet"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._list = new[] { new Origin(), new Origin(), new Origin() };
        this._testClass = new EntitySet<TDto>(this._list);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EntitySet<TDto>();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new EntitySet<TDto>(this._list);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the list parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullList()
    {
        Should.Throw<ArgumentNullException>(() => new EntitySet<TDto>(default(IEnumerable<TDto>)));
    }

    /// <summary>
    /// Checks that the Single property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetSingle()
    {
        // Assert
        this._testClass.Single.ShouldBeOfType<TDto>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the indexer functions correctly.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIndexer()
    {
        var testValue = new object();
        this._testClass[new object()].ShouldBeOfType<object>();
        this._testClass[new object()] = testValue;
        this._testClass[new object()].ShouldBeSameAs(testValue);
    }
}