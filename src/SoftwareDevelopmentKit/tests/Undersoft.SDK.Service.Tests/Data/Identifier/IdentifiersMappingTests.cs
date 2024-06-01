using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Identifier;

namespace Undersoft.SDK.Service.Tests.Data.Identifier;

/// <summary>
/// Unit tests for the type <see cref="IdentifiersMapping"/>.
/// </summary>
[TestClass]
public class IdentifiersMappingTests
{
    private IdentifiersMapping _testClass;
    private Type _type;
    private ModelBuilder _builder;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="IdentifiersMapping"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._type = typeof(string);
        this._builder = new ModelBuilder();
        this._testClass = new IdentifiersMapping(this._type, this._builder);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new IdentifiersMapping(this._type, this._builder);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => new IdentifiersMapping(default(Type), this._builder));
    }

    /// <summary>
    /// Checks that instance construction throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => new IdentifiersMapping(this._type, default(ModelBuilder)));
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