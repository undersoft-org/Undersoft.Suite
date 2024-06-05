using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts.Countries;

namespace Undersoft.SCC.Service.Tests.Contracts.Countries;

/// <summary>
/// Unit tests for the type <see cref="Currency"/>.
/// </summary>
[TestClass]
public partial class CurrencyTests
{
    private Currency _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Currency"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Currency();
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue1143420241";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CurrencyCode property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCurrencyCode()
    {
        // Arrange
        var testValue = "TestValue1587098432";

        // Act
        this._testClass.CurrencyCode = testValue;

        // Assert
        this._testClass.CurrencyCode.ShouldBe(testValue);
    }
}