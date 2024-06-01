using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountToken"/>.
/// </summary>
[TestClass]
public class AccountTokenTests
{
    private AccountToken _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountToken"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountToken();
    }

    /// <summary>
    /// Checks that the Id property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetId()
    {
        // Arrange
        var testValue = 1489718600L;

        // Act
        this._testClass.Id = testValue;

        // Assert
        this._testClass.Id.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the TypeId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTypeId()
    {
        // Arrange
        var testValue = 322921711L;

        // Act
        this._testClass.TypeId = testValue;

        // Assert
        this._testClass.TypeId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the AccountId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccountId()
    {
        // Arrange
        var testValue = 1659821985L;

        // Act
        this._testClass.AccountId = testValue;

        // Assert
        this._testClass.AccountId.ShouldBe(testValue);
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