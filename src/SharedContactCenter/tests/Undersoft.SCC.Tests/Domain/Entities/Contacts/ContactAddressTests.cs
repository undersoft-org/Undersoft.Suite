using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities.Contacts;

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
        var testValue = "TestValue807504727";

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
        var testValue = "TestValue2088961785";

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
        var testValue = "TestValue779106254";

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
        var testValue = "TestValue2039696239";

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
        var testValue = "TestValue640312824";

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
        var testValue = "TestValue1876081001";

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
        var testValue = "TestValue1000321112";

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
        var testValue = "TestValue1242537746";

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
        var testValue = 268870004L;

        // Act
        this._testClass.ContactId = testValue;

        // Assert
        this._testClass.ContactId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Contact property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContact()
    {
        // Arrange
        var testValue = new Contact
        {
            Name = "TestValue491755773",
            Notes = "TestValue1068713997",
            Type = ContactType.Bussines,
            PersonalId = 1838142399L,
            Personal = new ContactPersonal
            {
                FirstName = "TestValue2023763197",
                LastName = "TestValue821958658",
                Email = "TestValue42685711",
                PhoneNumber = "TestValue1377465590",
                Birthdate = DateTime.UtcNow,
                PersonalImage = "TestValue335123418",
                PersonalImageData = new byte[] { 116, 209, 216, 244 },
                ContactId = 1484881683L,
                Contact = default(Contact)
            },
            AddressId = 2140424099L,
            Address = new ContactAddress
            {
                Country = "TestValue326068013",
                State = "TestValue1587852630",
                City = "TestValue281818576",
                Postcode = "TestValue1163251363",
                Street = "TestValue1774973265",
                Building = "TestValue504967249",
                Apartment = "TestValue712933429",
                Notes = "TestValue1062284356",
                ContactId = 1526603235L,
                Contact = default(Contact)
            },
            ProfessionalId = 386988882L,
            Professional = new ContactProfessional
            {
                ProfessionIndustry = "TestValue376730960",
                Profession = "TestValue482910683",
                ProfessionalEmail = "TestValue1340727904",
                ProfessionalPhoneNumber = "TestValue1497804228",
                ProfessionalSocialMedia = "TestValue1367599698",
                ProfessionalWebsites = "TestValue802381675",
                ProfessionalExperience = 24808.29F,
                ContactId = 1884331513L,
                Contact = default(Contact)
            },
            OrganizationId = 1393169738L,
            Organization = new ContactOrganization
            {
                OrganizationIndustry = "TestValue136660143",
                OrganizationName = "TestValue114854562",
                OrganizationFullName = "TestValue1358483787",
                PositionInOrganization = "TestValue1853300856",
                OrganizationWebsites = "TestValue1198606432",
                OrganizationSize = OrganizationSize.None,
                OrganizationImage = "TestValue1035428668",
                OrganizationImageData = new byte[] { 28, 133, 13, 117 },
                ContactId = 2133998911L,
                Contact = default(Contact)
            },
            Groups = new EntitySet<Group>()
        };

        // Act
        this._testClass.Contact = testValue;

        // Assert
        this._testClass.Contact.ShouldBeSameAs(testValue);
    }
}