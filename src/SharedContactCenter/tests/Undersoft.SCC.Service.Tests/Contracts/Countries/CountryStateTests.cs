using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts.Countries;

namespace Undersoft.SCC.Service.Tests.Contracts.Countries;

/// <summary>
/// Unit tests for the type <see cref="CountryState"/>.
/// </summary>
[TestClass]
public class CountryStateTests
{
    private CountryState _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="CountryState"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new CountryState();
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue663356408";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the StateCode property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetStateCode()
    {
        // Arrange
        var testValue = "TestValue1670764322";

        // Act
        this._testClass.StateCode = testValue;

        // Assert
        this._testClass.StateCode.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the TimeZone property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTimeZone()
    {
        // Arrange
        var testValue = "TestValue103277507";

        // Act
        this._testClass.TimeZone = testValue;

        // Assert
        this._testClass.TimeZone.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CountryId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountryId()
    {
        // Arrange
        var testValue = 2010045041L;

        // Act
        this._testClass.CountryId = testValue;

        // Assert
        this._testClass.CountryId.ShouldBe(testValue);
    }
}