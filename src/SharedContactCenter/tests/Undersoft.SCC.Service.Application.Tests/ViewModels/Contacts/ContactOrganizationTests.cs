using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Application.ViewModels.Contacts;

namespace Undersoft.SCC.Service.Application.Tests.ViewModels.Contacts;

/// <summary>
/// Unit tests for the type <see cref="ContactOrganization"/>.
/// </summary>
[TestClass]
public class ContactOrganizationTests
{
    private ContactOrganization _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactOrganization"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ContactOrganization();
    }

    /// <summary>
    /// Checks that the OrganizationIndustry property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationIndustry()
    {
        // Arrange
        var testValue = "TestValue330018495";

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
        var testValue = "TestValue297898319";

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
        var testValue = "TestValue1213822037";

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
        var testValue = "TestValue1230802061";

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
        var testValue = "TestValue1664258935";

        // Act
        this._testClass.OrganizationWebsites = testValue;

        // Assert
        this._testClass.OrganizationWebsites.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationSize property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationSize()
    {
        // Arrange
        var testValue = OrganizationSize.Micro;

        // Act
        this._testClass.OrganizationSize = testValue;

        // Assert
        this._testClass.OrganizationSize.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationImage()
    {
        // Arrange
        var testValue = "TestValue116738159";

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
        var testValue = new byte[] { 36, 166, 215, 142 };

        // Act
        this._testClass.OrganizationImageData = testValue;

        // Assert
        this._testClass.OrganizationImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the ContactId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContactId()
    {
        // Arrange
        var testValue = 591827256L;

        // Act
        this._testClass.ContactId = testValue;

        // Assert
        this._testClass.ContactId.ShouldBe(testValue);
    }
}