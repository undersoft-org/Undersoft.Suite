using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.ViewModels.Contacts;

namespace Undersoft.SCC.Service.Application.Tests.ViewModels.Contacts;

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
        var testValue = "TestValue1253325090";

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
        var testValue = "TestValue996120254";

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
        var testValue = "TestValue2139304690";

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
        var testValue = "TestValue165577114";

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
        var testValue = "TestValue918627133";

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
        var testValue = "TestValue838940894";

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
        var testValue = 26215.2344F;

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
        var testValue = 73405229L;

        // Act
        this._testClass.ContactId = testValue;

        // Assert
        this._testClass.ContactId.ShouldBe(testValue);
    }
}