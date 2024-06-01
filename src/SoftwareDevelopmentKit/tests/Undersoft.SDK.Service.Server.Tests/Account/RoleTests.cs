using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="Role"/>.
/// </summary>
[TestClass]
public class RoleTests
{
    private Role _testClass;
    private string _roleName;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Role"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._roleName = "TestValue704014897";
        this._testClass = new Role(this._roleName);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Role();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Role(this._roleName);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the constructor throws when the roleName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidRoleName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new Role(value));
    }

    /// <summary>
    /// Checks that the TypeId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTypeId()
    {
        // Arrange
        var testValue = 1872253671L;

        // Act
        this._testClass.TypeId = testValue;

        // Assert
        this._testClass.TypeId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Claims property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaims()
    {
        // Arrange
        var testValue = new Listing<RoleClaim>();

        // Act
        this._testClass.Claims = testValue;

        // Assert
        this._testClass.Claims.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Accounts property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccounts()
    {
        // Arrange
        var testValue = new Listing<Account>();

        // Act
        this._testClass.Accounts = testValue;

        // Assert
        this._testClass.Accounts.ShouldBeSameAs(testValue);
    }
}