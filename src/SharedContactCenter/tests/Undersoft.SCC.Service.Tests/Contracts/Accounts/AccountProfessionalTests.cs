using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts.Accounts;

namespace Undersoft.SCC.Service.Tests.Contracts.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountProfessional"/>.
/// </summary>
[TestClass]
public class AccountProfessionalTests
{
    private AccountProfessional _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountProfessional"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountProfessional();
    }

    /// <summary>
    /// Checks that the ProfessionIndustry property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionIndustry()
    {
        // Arrange
        var testValue = "TestValue102009322";

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
        var testValue = "TestValue1890177099";

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
        var testValue = "TestValue27640906";

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
        var testValue = "TestValue1435581634";

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
        var testValue = "TestValue2032347228";

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
        var testValue = "TestValue92809076";

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
        var testValue = 17739.26F;

        // Act
        this._testClass.ProfessionalExperience = testValue;

        // Assert
        this._testClass.ProfessionalExperience.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the AccountId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccountId()
    {
        // Arrange
        var testValue = 1238709776L;

        // Act
        this._testClass.AccountId = testValue;

        // Assert
        this._testClass.AccountId.ShouldBe(testValue);
    }
}