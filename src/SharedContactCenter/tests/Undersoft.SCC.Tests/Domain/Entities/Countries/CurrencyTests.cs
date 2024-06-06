using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Countries;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities.Countries;

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
        var testValue = "TestValue188830610";

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
        var testValue = "TestValue864079840";

        // Act
        this._testClass.CurrencyCode = testValue;

        // Assert
        this._testClass.CurrencyCode.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Countries property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountries()
    {
        // Arrange
        var testValue = new EntitySet<Country>();

        // Act
        this._testClass.Countries = testValue;

        // Assert
        this._testClass.Countries.ShouldBeSameAs(testValue);
    }
}