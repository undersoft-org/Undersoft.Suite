using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SCC.Service.Contracts.Contacts;

namespace Undersoft.SCC.Service.Tests.Contracts.Contacts;

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
        var testValue = new Country();

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
        var testValue = "TestValue1256456671";

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
        var testValue = "TestValue715569983";

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
        var testValue = "TestValue1157268335";

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
        var testValue = "TestValue747117555";

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
        var testValue = "TestValue1871944955";

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
        var testValue = "TestValue1479475333";

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
        var testValue = "TestValue61289328";

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
        var testValue = 744053278L;

        // Act
        this._testClass.ContactId = testValue;

        // Assert
        this._testClass.ContactId.ShouldBe(testValue);
    }
}