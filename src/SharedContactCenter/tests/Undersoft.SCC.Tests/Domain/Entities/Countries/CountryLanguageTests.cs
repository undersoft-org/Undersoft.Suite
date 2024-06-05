using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Countries;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities.Countries;

/// <summary>
/// Unit tests for the type <see cref="CountryLanguage"/>.
/// </summary>
[TestClass]
public class CountryLanguageTests
{
    private CountryLanguage _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="CountryLanguage"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new CountryLanguage();
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue734744068";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the LanguageCode property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLanguageCode()
    {
        // Arrange
        var testValue = "TestValue585166425";

        // Act
        this._testClass.LanguageCode = testValue;

        // Assert
        this._testClass.LanguageCode.ShouldBe(testValue);
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