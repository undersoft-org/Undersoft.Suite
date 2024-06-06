using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts;

namespace Undersoft.SCC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Group"/>.
/// </summary>
[TestClass]
public partial class GroupTests
{
    private Group _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Group"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Group();
    }

    /// <summary>
    /// Checks that the GroupImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGroupImage()
    {
        // Arrange
        var testValue = "TestValue1769369929";

        // Act
        this._testClass.GroupImage = testValue;

        // Assert
        this._testClass.GroupImage.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue653725801";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the GroupImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGroupImageData()
    {
        // Arrange
        var testValue = new byte[] { 146, 183, 60, 114 };

        // Act
        this._testClass.GroupImageData = testValue;

        // Assert
        this._testClass.GroupImageData.ShouldBeSameAs(testValue);
    }
}