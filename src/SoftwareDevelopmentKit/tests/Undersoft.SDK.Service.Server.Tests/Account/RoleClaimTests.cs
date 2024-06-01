using System;
using System.IO;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="RoleClaim"/>.
/// </summary>
[TestClass]
public class RoleClaimTests
{
    private RoleClaim _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="RoleClaim"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new RoleClaim();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new RoleClaim();

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
        var testValue = 1051137040L;

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
        var testValue = 39002486L;

        // Act
        this._testClass.TypeId = testValue;

        // Assert
        this._testClass.TypeId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the AccountRoleId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccountRoleId()
    {
        // Arrange
        var testValue = 1807652407L;

        // Act
        this._testClass.AccountRoleId = testValue;

        // Assert
        this._testClass.AccountRoleId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Role property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRole()
    {
        // Arrange
        var testValue = new Role
        {
            TypeId = 1735351475L,
            Claims = new Listing<RoleClaim>(),
            Accounts = new Listing<Account>()
        };

        // Act
        this._testClass.Role = testValue;

        // Assert
        this._testClass.Role.ShouldBeSameAs(testValue);
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