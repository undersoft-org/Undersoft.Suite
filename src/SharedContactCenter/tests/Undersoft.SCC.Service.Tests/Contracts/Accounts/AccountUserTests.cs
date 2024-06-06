using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Service.Contracts.Accounts;

namespace Undersoft.SCC.Service.Tests.Contracts.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountUser"/>.
/// </summary>
[TestClass]
public class AccountUserTests
{
    private AccountUser _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountUser"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountUser();
    }

    /// <summary>
    /// Checks that the UserName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetUserName()
    {
        // Arrange
        var testValue = "TestValue32370179";

        // Act
        this._testClass.UserName = testValue;

        // Assert
        this._testClass.UserName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the NormalizedUserName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNormalizedUserName()
    {
        // Arrange
        var testValue = "TestValue1309267992";

        // Act
        this._testClass.NormalizedUserName = testValue;

        // Assert
        this._testClass.NormalizedUserName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Email property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        // Arrange
        var testValue = "TestValue1774707225";

        // Act
        this._testClass.Email = testValue;

        // Assert
        this._testClass.Email.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the NormalizedEmail property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNormalizedEmail()
    {
        // Arrange
        var testValue = "TestValue590576270";

        // Act
        this._testClass.NormalizedEmail = testValue;

        // Assert
        this._testClass.NormalizedEmail.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the EmailConfirmed property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmailConfirmed()
    {
        // Arrange
        var testValue = false;

        // Act
        this._testClass.EmailConfirmed = testValue;

        // Assert
        this._testClass.EmailConfirmed.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PhoneNumber property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumber()
    {
        // Arrange
        var testValue = "TestValue1584303320";

        // Act
        this._testClass.PhoneNumber = testValue;

        // Assert
        this._testClass.PhoneNumber.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PhoneNumberConfirmed property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumberConfirmed()
    {
        // Arrange
        var testValue = false;

        // Act
        this._testClass.PhoneNumberConfirmed = testValue;

        // Assert
        this._testClass.PhoneNumberConfirmed.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the TwoFactorEnabled property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTwoFactorEnabled()
    {
        // Arrange
        var testValue = false;

        // Act
        this._testClass.TwoFactorEnabled = testValue;

        // Assert
        this._testClass.TwoFactorEnabled.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the LockoutEnd property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLockoutEnd()
    {
        // Arrange
        var testValue = DateTimeOffset.UtcNow;

        // Act
        this._testClass.LockoutEnd = testValue;

        // Assert
        this._testClass.LockoutEnd.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the LockoutEnabled property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLockoutEnabled()
    {
        // Arrange
        var testValue = false;

        // Act
        this._testClass.LockoutEnabled = testValue;

        // Assert
        this._testClass.LockoutEnabled.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the AccessFailedCount property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccessFailedCount()
    {
        // Arrange
        var testValue = 1995717824;

        // Act
        this._testClass.AccessFailedCount = testValue;

        // Assert
        this._testClass.AccessFailedCount.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the RegistrationCompleted property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRegistrationCompleted()
    {
        // Arrange
        var testValue = false;

        // Act
        this._testClass.RegistrationCompleted = testValue;

        // Assert
        this._testClass.RegistrationCompleted.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the IsLockedOut property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIsLockedOut()
    {
        // Arrange
        var testValue = false;

        // Act
        this._testClass.IsLockedOut = testValue;

        // Assert
        this._testClass.IsLockedOut.ShouldBe(testValue);
    }
}