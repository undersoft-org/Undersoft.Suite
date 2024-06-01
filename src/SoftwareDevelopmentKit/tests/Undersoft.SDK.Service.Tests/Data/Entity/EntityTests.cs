using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Undersoft.SDK.Service.Tests.Data.Entity;

/// <summary>
/// Unit tests for the type <see cref="Entity"/>.
/// </summary>
[TestClass]
public class EntityTests
{
    private Undersoft.SDK.Service.Data.Entity.Entity _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Entity"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Undersoft.SDK.Service.Data.Entity.Entity();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Undersoft.SDK.Service.Data.Entity.Entity();

        // Assert
        instance.ShouldNotBeNull();
    }
}