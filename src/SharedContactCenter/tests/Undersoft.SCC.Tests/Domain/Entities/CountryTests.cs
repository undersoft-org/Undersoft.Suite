using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Countries;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities;

/// <summary>
/// Unit tests for the type <see cref="Country"/>.
/// </summary>
[TestClass]
public partial class CountryTests
{
    private Country _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Country"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Country();
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue46018226";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CountryCode property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountryCode()
    {
        // Arrange
        var testValue = "TestValue195266536";

        // Act
        this._testClass.CountryCode = testValue;

        // Assert
        this._testClass.CountryCode.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Continent property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContinent()
    {
        // Arrange
        var testValue = "TestValue1668774352";

        // Act
        this._testClass.Continent = testValue;

        // Assert
        this._testClass.Continent.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the TimeZone property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTimeZone()
    {
        // Arrange
        var testValue = "TestValue230796209";

        // Act
        this._testClass.TimeZone = testValue;

        // Assert
        this._testClass.TimeZone.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CountryImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountryImage()
    {
        // Arrange
        var testValue = "TestValue2034993789";

        // Act
        this._testClass.CountryImage = testValue;

        // Assert
        this._testClass.CountryImage.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CountryImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountryImageData()
    {
        // Arrange
        var testValue = new byte[] { 16, 178, 26, 81 };

        // Act
        this._testClass.CountryImageData = testValue;

        // Assert
        this._testClass.CountryImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the CurrencyId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCurrencyId()
    {
        // Arrange
        var testValue = 1078571774L;

        // Act
        this._testClass.CurrencyId = testValue;

        // Assert
        this._testClass.CurrencyId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Currency property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCurrency()
    {
        // Arrange
        var testValue = new Currency
        {
            Name = "TestValue1930947827",
            CurrencyCode = "TestValue191626072",
            Countries = new EntitySet<Country>()
        };

        // Act
        this._testClass.Currency = testValue;

        // Assert
        this._testClass.Currency.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the LanguageId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLanguageId()
    {
        // Arrange
        var testValue = 14380925L;

        // Act
        this._testClass.LanguageId = testValue;

        // Assert
        this._testClass.LanguageId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Language property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLanguage()
    {
        // Arrange
        var testValue = new CountryLanguage
        {
            Name = "TestValue568961305",
            LanguageCode = "TestValue928015983",
            Countries = new EntitySet<Country>()
        };

        // Act
        this._testClass.Language = testValue;

        // Assert
        this._testClass.Language.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the States property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetStates()
    {
        // Arrange
        var testValue = new EntitySet<CountryState>();

        // Act
        this._testClass.States = testValue;

        // Assert
        this._testClass.States.ShouldBeSameAs(testValue);
    }
}