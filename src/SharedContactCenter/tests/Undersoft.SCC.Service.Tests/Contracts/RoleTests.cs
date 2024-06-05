using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SCC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Role"/>.
/// </summary>
[TestClass]
public class RoleTests
{
    private Role _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Role"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Role();
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue41538055";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the NormalizedName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNormalizedName()
    {
        // Arrange
        var testValue = "TestValue1946615467";

        // Act
        this._testClass.NormalizedName = testValue;

        // Assert
        this._testClass.NormalizedName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Claims property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaims()
    {
        // Arrange
        var testValue = new ObjectSet<Claim>();

        // Act
        this._testClass.Claims = testValue;

        // Assert
        this._testClass.Claims.ShouldBeSameAs(testValue);
    }
}