using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountTokenOptions"/>.
/// </summary>
[TestClass]
public class AccountTokenOptionsTests
{
    private AccountTokenOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountTokenOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountTokenOptions();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountTokenOptions();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the SecurityKey property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSecurityKey()
    {
        // Arrange
        var testValue = new byte[] { 94, 135, 240, 127 };

        // Act
        this._testClass.SecurityKey = testValue;

        // Assert
        this._testClass.SecurityKey.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Issuer property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIssuer()
    {
        // Arrange
        var testValue = "TestValue831632198";

        // Act
        this._testClass.Issuer = testValue;

        // Assert
        this._testClass.Issuer.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Audience property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAudience()
    {
        // Arrange
        var testValue = "TestValue496052063";

        // Act
        this._testClass.Audience = testValue;

        // Assert
        this._testClass.Audience.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the MinutesToExpire property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMinutesToExpire()
    {
        // Arrange
        var testValue = 169780781;

        // Act
        this._testClass.MinutesToExpire = testValue;

        // Assert
        this._testClass.MinutesToExpire.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Value property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetValue()
    {
        // Assert
        this._testClass.Value.ShouldBeOfType<AccountTokenOptions>();

        Assert.Fail("Create or modify test");
    }
}