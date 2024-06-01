using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts.Email;

namespace Undersoft.SDK.Service.Server.Tests.Accounts.Email;

/// <summary>
/// Unit tests for the type <see cref="AccountEmailSenderOptions"/>.
/// </summary>
[TestClass]
public class AccountEmailSenderOptionsTests
{
    private AccountEmailSenderOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountEmailSenderOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountEmailSenderOptions();
    }

    /// <summary>
    /// Checks that the SendGridKey property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSendGridKey()
    {
        // Arrange
        var testValue = "TestValue1400650908";

        // Act
        this._testClass.SendGridKey = testValue;

        // Assert
        this._testClass.SendGridKey.ShouldBe(testValue);
    }
}