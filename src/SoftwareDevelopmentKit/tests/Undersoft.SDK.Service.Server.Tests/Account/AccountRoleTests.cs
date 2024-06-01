using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountRole"/>.
/// </summary>
[TestClass]
public class AccountRoleTests
{
    private AccountRole _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountRole"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountRole();
    }

    /// <summary>
    /// Checks that the Id property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetId()
    {
        // Arrange
        var testValue = 159587435L;

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
        var testValue = 1168255973L;

        // Act
        this._testClass.TypeId = testValue;

        // Assert
        this._testClass.TypeId.ShouldBe(testValue);
    }
}