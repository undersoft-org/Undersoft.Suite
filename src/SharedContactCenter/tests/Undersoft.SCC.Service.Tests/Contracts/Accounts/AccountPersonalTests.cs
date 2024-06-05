using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts.Accounts;

namespace Undersoft.SCC.Service.Tests.Contracts.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountPersonal"/>.
/// </summary>
[TestClass]
public class AccountPersonalTests
{
    private AccountPersonal _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountPersonal"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountPersonal();
    }

    /// <summary>
    /// Checks that the FirstName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetFirstName()
    {
        // Arrange
        var testValue = "TestValue372301202";

        // Act
        this._testClass.FirstName = testValue;

        // Assert
        this._testClass.FirstName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the LastName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLastName()
    {
        // Arrange
        var testValue = "TestValue1054819278";

        // Act
        this._testClass.LastName = testValue;

        // Assert
        this._testClass.LastName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Email property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        // Arrange
        var testValue = "TestValue2136219944";

        // Act
        this._testClass.Email = testValue;

        // Assert
        this._testClass.Email.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PhoneNumber property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumber()
    {
        // Arrange
        var testValue = "TestValue1338704247";

        // Act
        this._testClass.PhoneNumber = testValue;

        // Assert
        this._testClass.PhoneNumber.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Birthdate property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBirthdate()
    {
        // Arrange
        var testValue = DateTime.UtcNow;

        // Act
        this._testClass.Birthdate = testValue;

        // Assert
        this._testClass.Birthdate.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Image property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetImage()
    {
        // Arrange
        var testValue = "TestValue118799454";

        // Act
        this._testClass.Image = testValue;

        // Assert
        this._testClass.Image.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetImageData()
    {
        // Arrange
        var testValue = new byte[] { 7, 12, 64, 228 };

        // Act
        this._testClass.ImageData = testValue;

        // Assert
        this._testClass.ImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the AccountId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccountId()
    {
        // Arrange
        var testValue = 1226164564L;

        // Act
        this._testClass.AccountId = testValue;

        // Assert
        this._testClass.AccountId.ShouldBe(testValue);
    }
}