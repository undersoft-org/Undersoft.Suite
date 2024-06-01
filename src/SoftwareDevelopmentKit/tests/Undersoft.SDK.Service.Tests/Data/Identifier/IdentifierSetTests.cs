using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Identifier;
using TObject = Undersoft.SDK.Service.Data.Remote.RemoteLink;

namespace Undersoft.SDK.Service.Tests.Data.Identifier;

/// <summary>
/// Unit tests for the type <see cref="IdentifierSet"/>.
/// </summary>
[TestClass]
public class IdentifierSet_1Tests
{
    private IdentifierSet<TObject> _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="IdentifierSet"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new IdentifierSet<TObject>();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new IdentifierSet<TObject>();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the Search method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSearch()
    {
        // Arrange
        var id = new object();

        // Act
        var result = this._testClass.Search(id);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Search method throws when the id parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSearchWithNullId()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Search(default(object)));
    }


}