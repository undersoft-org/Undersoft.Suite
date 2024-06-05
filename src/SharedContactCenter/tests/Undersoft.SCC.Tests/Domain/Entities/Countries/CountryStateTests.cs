using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Countries;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities.Countries;

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
        var testValue = "TestValue1627371662";

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
        var testValue = "TestValue1727178916";

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
        var testValue = "TestValue934197209";

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
        var testValue = 1096318516L;

        // Act
        this._testClass.CountryId = testValue;

        // Assert
        this._testClass.CountryId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Country property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountry()
    {
        // Arrange
        var testValue = new Country
        {
            Name = "TestValue899840428",
            CountryCode = "TestValue653694759",
            Continent = "TestValue1688539322",
            TimeZone = "TestValue575636429",
            CountryImage = "TestValue794602690",
            CountryImageData = new byte[] { 97, 178, 200, 98 },
            CurrencyId = 1856029415L,
            Currency = new Currency
            {
                Name = "TestValue1410861085",
                CurrencyCode = "TestValue903477999",
                Countries = new EntitySet<Country>()
            },
            LanguageId = 759606167L,
            Language = new CountryLanguage
            {
                Name = "TestValue1069187401",
                LanguageCode = "TestValue887722051",
                Countries = new EntitySet<Country>()
            },
            States = new EntitySet<CountryState>()
        };

        // Act
        this._testClass.Country = testValue;

        // Assert
        this._testClass.Country.ShouldBeSameAs(testValue);
    }
}