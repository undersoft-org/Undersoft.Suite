using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts.Accounts;

namespace Undersoft.SCC.Service.Tests.Contracts.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountOrganization"/>.
/// </summary>
[TestClass]
public class AccountOrganizationTests
{
    private AccountOrganization _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountOrganization"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountOrganization();
    }

    /// <summary>
    /// Checks that the OrganizationIndustry property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationIndustry()
    {
        // Arrange
        var testValue = "TestValue1557003796";

        // Act
        this._testClass.OrganizationIndustry = testValue;

        // Assert
        this._testClass.OrganizationIndustry.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationName()
    {
        // Arrange
        var testValue = "TestValue432326693";

        // Act
        this._testClass.OrganizationName = testValue;

        // Assert
        this._testClass.OrganizationName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationFullName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationFullName()
    {
        // Arrange
        var testValue = "TestValue1835980686";

        // Act
        this._testClass.OrganizationFullName = testValue;

        // Assert
        this._testClass.OrganizationFullName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PositionInOrganization property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPositionInOrganization()
    {
        // Arrange
        var testValue = "TestValue195250399";

        // Act
        this._testClass.PositionInOrganization = testValue;

        // Assert
        this._testClass.PositionInOrganization.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationWebsites property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationWebsites()
    {
        // Arrange
        var testValue = "TestValue151169633";

        // Act
        this._testClass.OrganizationWebsites = testValue;

        // Assert
        this._testClass.OrganizationWebsites.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationImage()
    {
        // Arrange
        var testValue = "TestValue1376474737";

        // Act
        this._testClass.OrganizationImage = testValue;

        // Assert
        this._testClass.OrganizationImage.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationImageData()
    {
        // Arrange
        var testValue = new byte[] { 223, 134, 137, 78 };

        // Act
        this._testClass.OrganizationImageData = testValue;

        // Assert
        this._testClass.OrganizationImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the AccountId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccountId()
    {
        // Arrange
        var testValue = 368872132L;

        // Act
        this._testClass.AccountId = testValue;

        // Assert
        this._testClass.AccountId.ShouldBe(testValue);
    }
}