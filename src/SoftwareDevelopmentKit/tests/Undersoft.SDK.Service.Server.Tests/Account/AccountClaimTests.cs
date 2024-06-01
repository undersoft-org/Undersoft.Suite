using System;
using System.IO;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountClaim"/>.
/// </summary>
[TestClass]
public class AccountClaimTests
{
    private AccountClaim _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountClaim"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountClaim();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountClaim();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the Id property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetId()
    {
        // Arrange
        var testValue = 1666762462L;

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
        var testValue = 956135872L;

        // Act
        this._testClass.TypeId = testValue;

        // Assert
        this._testClass.TypeId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Claim property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaim()
    {
        // Arrange
        var testValue = new Claim(new BinaryReader(new MemoryStream()));

        // Act
        this._testClass.Claim = testValue;

        // Assert
        this._testClass.Claim.ShouldBeSameAs(testValue);
    }
}