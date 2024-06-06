using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts.Contacts;

namespace Undersoft.SCC.Service.Tests.Contracts.Contacts;

/// <summary>
/// Unit tests for the type <see cref="ContactProfessional"/>.
/// </summary>
[TestClass]
public class ContactProfessionalTests
{
    private ContactProfessional _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactProfessional"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ContactProfessional();
    }

    /// <summary>
    /// Checks that the ProfessionIndustry property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionIndustry()
    {
        // Arrange
        var testValue = "TestValue1224386338";

        // Act
        this._testClass.ProfessionIndustry = testValue;

        // Assert
        this._testClass.ProfessionIndustry.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Profession property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfession()
    {
        // Arrange
        var testValue = "TestValue1129061810";

        // Act
        this._testClass.Profession = testValue;

        // Assert
        this._testClass.Profession.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalEmail property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalEmail()
    {
        // Arrange
        var testValue = "TestValue1702375343";

        // Act
        this._testClass.ProfessionalEmail = testValue;

        // Assert
        this._testClass.ProfessionalEmail.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalPhoneNumber property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalPhoneNumber()
    {
        // Arrange
        var testValue = "TestValue1448575309";

        // Act
        this._testClass.ProfessionalPhoneNumber = testValue;

        // Assert
        this._testClass.ProfessionalPhoneNumber.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalSocialMedia property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalSocialMedia()
    {
        // Arrange
        var testValue = "TestValue1339260930";

        // Act
        this._testClass.ProfessionalSocialMedia = testValue;

        // Assert
        this._testClass.ProfessionalSocialMedia.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalWebsites property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalWebsites()
    {
        // Arrange
        var testValue = "TestValue1934519929";

        // Act
        this._testClass.ProfessionalWebsites = testValue;

        // Assert
        this._testClass.ProfessionalWebsites.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalExperience property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalExperience()
    {
        // Arrange
        var testValue = 5478.159F;

        // Act
        this._testClass.ProfessionalExperience = testValue;

        // Assert
        this._testClass.ProfessionalExperience.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ContactId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContactId()
    {
        // Arrange
        var testValue = 1426650246L;

        // Act
        this._testClass.ContactId = testValue;

        // Assert
        this._testClass.ContactId.ShouldBe(testValue);
    }
}