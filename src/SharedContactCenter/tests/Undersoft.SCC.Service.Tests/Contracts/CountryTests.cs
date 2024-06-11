using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SCC.Service.Contracts.Countries;
using Undersoft.SDK.Series;

namespace Undersoft.SCC.Service.Tests.Contracts;

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
    /// Checks that the CountryImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountryImage()
    {
        // Arrange
        var testValue = "TestValue1656703415";

        // Act
        this._testClass.CountryImage = testValue;

        // Assert
        this._testClass.CountryImage.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue1626792704";

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
        var testValue = "TestValue1601604953";

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
        var testValue = "TestValue626609189";

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
        var testValue = "TestValue521234536";

        // Act
        this._testClass.TimeZone = testValue;

        // Assert
        this._testClass.TimeZone.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CountryImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountryImageData()
    {
        // Arrange
        var testValue = new byte[] { 165, 215, 40, 225 };

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
        var testValue = 897425300L;

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
            Name = "TestValue1498832661",
            CurrencyCode = "TestValue1819356503"
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
        var testValue = 188232833L;

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
            Name = "TestValue981728191",
            LanguageCode = "TestValue656129051"
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
        var testValue = new Listing<CountryState>();

        // Act
        this._testClass.States = testValue;

        // Assert
        this._testClass.States.ShouldBeSameAs(testValue);
    }
}