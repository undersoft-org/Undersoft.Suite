using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountCredentials"/>.
/// </summary>
[TestClass]
public class AccountCredentialsTests
{
    private AccountCredentials _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountCredentials"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountCredentials();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountCredentials();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the MapUser method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMapUser()
    {
        // Arrange
        var account = new IdentityUser<long>();

        // Act
        this._testClass.MapUser(account);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MapUser method throws when the account parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallMapUserWithNullAccount()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.MapUser(default(IdentityUser<long>)));
    }
}