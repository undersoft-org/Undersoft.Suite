using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK;
using Undersoft.SDK.Uniques;
using T = Undersoft.SDK.Origin;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="UniqueOne"/>.
/// </summary>
[TestClass]
public class UniqueOne_1Tests
{
    private UniqueOne<T> _testClass;
    private T _entity;
    private IEnumerable<T> _enumerable;
    private IQueryable<T> _queryable;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="UniqueOne"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._entity = new Origin();
        this._enumerable = new[] { new Origin(), new Origin(), new Origin() };
        this._queryable = Substitute.For<IQueryable<T>>();
        this._testClass = new UniqueOne<T>(this._entity);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new UniqueOne<T>(this._entity);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new UniqueOne<T>(this._enumerable);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new UniqueOne<T>(this._queryable);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the enumerable parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullEnumerable()
    {
        Should.Throw<ArgumentNullException>(() => new UniqueOne<T>(default(IEnumerable<T>)));
    }

    /// <summary>
    /// Checks that instance construction throws when the queryable parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullQueryable()
    {
        Should.Throw<ArgumentNullException>(() => new UniqueOne<T>(default(IQueryable<T>)));
    }

    /// <summary>
    /// Checks that the Queryable property is initialized correctly by the constructors.
    /// </summary>
    [TestMethod]
    public void QueryableIsInitializedCorrectly()
    {
        this._testClass = new UniqueOne<T>(this._queryable);
        this._testClass.Queryable.ShouldBeSameAs(this._queryable);
    }
}

/// <summary>
/// Unit tests for the type <see cref="UniqueOne"/>.
/// </summary>
[TestClass]
public class UniqueOneTests
{
    private class TestUniqueOne : UniqueOne
    {
        public TestUniqueOne(object entity) : base(entity)
        {
        }

        public TestUniqueOne(IQueryable queryable) : base(queryable)
        {
        }
    }

    private TestUniqueOne _testClass;
    private object _entity;
    private IQueryable _queryable;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="UniqueOne"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._entity = new object();
        this._queryable = Substitute.For<IQueryable>();
        this._testClass = new TestUniqueOne(this._entity);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new TestUniqueOne(this._entity);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new TestUniqueOne(this._queryable);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => new TestUniqueOne(default(object)));
    }

    /// <summary>
    /// Checks that instance construction throws when the queryable parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullQueryable()
    {
        Should.Throw<ArgumentNullException>(() => new TestUniqueOne(default(IQueryable)));
    }

    /// <summary>
    /// Checks that the Queryable property is initialized correctly by the constructors.
    /// </summary>
    [TestMethod]
    public void QueryableIsInitializedCorrectly()
    {
        this._testClass = new TestUniqueOne(this._queryable);
        this._testClass.Queryable.ShouldBeSameAs(this._queryable);
    }
}