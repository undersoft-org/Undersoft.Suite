using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Identifier;
using TObject = Undersoft.SDK.Stocks.StockContext;

namespace Undersoft.SDK.Service.Tests.Data.Identifier;

/// <summary>
/// Unit tests for the type <see cref="IdentifiersMapping"/>.
/// </summary>
[TestClass]
public class IdentifiersMapping_1Tests
{
    private IdentifiersMapping<TObject> _testClass;
    private ModelBuilder _builder;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="IdentifiersMapping"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._builder = new ModelBuilder();
        this._testClass = new IdentifiersMapping<TObject>(this._builder);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new IdentifiersMapping<TObject>(this._builder);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => new IdentifiersMapping<TObject>(default(ModelBuilder)));
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Act
        var result = this._testClass.Configure();

        // Assert
        Assert.Fail("Create or modify test");
    }
}