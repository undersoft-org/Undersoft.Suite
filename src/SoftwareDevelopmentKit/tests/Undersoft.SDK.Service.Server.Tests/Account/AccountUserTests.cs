using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountUser"/>.
/// </summary>
[TestClass]
public class AccountUserTests
{
    private AccountUser _testClass;
    private string _email;
    private string _userName;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountUser"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._email = "TestValue205745164";
        this._userName = "TestValue1755234654";
        this._testClass = new AccountUser(this._userName, this._email);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountUser();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new AccountUser(this._email);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new AccountUser(this._userName, this._email);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the constructor throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidEmail(string value)
    {
        Should.Throw<ArgumentNullException>(() => new AccountUser(value));
        Should.Throw<ArgumentNullException>(() => new AccountUser(this._userName, value));
    }

    /// <summary>
    /// Checks that the constructor throws when the userName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidUserName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new AccountUser(value, this._email));
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

    /// <summary>
    /// Checks that the TypeId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTypeId()
    {
        // Arrange
        var testValue = 641927221L;

        // Act
        this._testClass.TypeId = testValue;

        // Assert
        this._testClass.TypeId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Account property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccount()
    {
        // Arrange
        var testValue = new Account();

        // Act
        this._testClass.Account = testValue;

        // Assert
        this._testClass.Account.ShouldBeSameAs(testValue);
    }
}