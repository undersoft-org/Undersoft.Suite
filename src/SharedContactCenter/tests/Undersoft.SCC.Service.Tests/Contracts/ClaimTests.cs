using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts;

namespace Undersoft.SCC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Claim"/>.
/// </summary>
[TestClass]
public class ClaimTests
{
    private Claim _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Claim"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Claim();
    }

    /// <summary>
    /// Checks that the ClaimType property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaimType()
    {
        // Arrange
        var testValue = "TestValue540590394";

        // Act
        this._testClass.ClaimType = testValue;

        // Assert
        this._testClass.ClaimType.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ClaimValue property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaimValue()
    {
        // Arrange
        var testValue = "TestValue567039301";

        // Act
        this._testClass.ClaimValue = testValue;

        // Assert
        this._testClass.ClaimValue.ShouldBe(testValue);
    }
}