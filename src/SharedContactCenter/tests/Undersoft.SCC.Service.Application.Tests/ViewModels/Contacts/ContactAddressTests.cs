using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.ViewModels.Contacts;

namespace Undersoft.SCC.Service.Application.Tests.ViewModels.Contacts;

/// <summary>
/// Unit tests for the type <see cref="ContactAddress"/>.
/// </summary>
[TestClass]
public class ContactAddressTests
{
    private ContactAddress _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactAddress"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ContactAddress();
    }

    /// <summary>
    /// Checks that the Country property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountry()
    {
        // Arrange
        var testValue = "TestValue598367398";

        // Act
        this._testClass.Country = testValue;

        // Assert
        this._testClass.Country.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the State property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetState()
    {
        // Arrange
        var testValue = "TestValue1721448612";

        // Act
        this._testClass.State = testValue;

        // Assert
        this._testClass.State.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the City property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCity()
    {
        // Arrange
        var testValue = "TestValue1822133920";

        // Act
        this._testClass.City = testValue;

        // Assert
        this._testClass.City.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Postcode property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPostcode()
    {
        // Arrange
        var testValue = "TestValue1590246370";

        // Act
        this._testClass.Postcode = testValue;

        // Assert
        this._testClass.Postcode.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Street property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetStreet()
    {
        // Arrange
        var testValue = "TestValue1385667941";

        // Act
        this._testClass.Street = testValue;

        // Assert
        this._testClass.Street.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Building property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBuilding()
    {
        // Arrange
        var testValue = "TestValue1964877453";

        // Act
        this._testClass.Building = testValue;

        // Assert
        this._testClass.Building.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Apartment property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetApartment()
    {
        // Arrange
        var testValue = "TestValue1370063249";

        // Act
        this._testClass.Apartment = testValue;

        // Assert
        this._testClass.Apartment.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Notes property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNotes()
    {
        // Arrange
        var testValue = "TestValue1303100262";

        // Act
        this._testClass.Notes = testValue;

        // Assert
        this._testClass.Notes.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ContactId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContactId()
    {
        // Arrange
        var testValue = 1395055806L;

        // Act
        this._testClass.ContactId = testValue;

        // Assert
        this._testClass.ContactId.ShouldBe(testValue);
    }
}