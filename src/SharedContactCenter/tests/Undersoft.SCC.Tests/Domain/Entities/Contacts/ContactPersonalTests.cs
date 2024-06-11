using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities.Contacts;

/// <summary>
/// Unit tests for the type <see cref="ContactPersonal"/>.
/// </summary>
[TestClass]
public class ContactPersonalTests
{
    private ContactPersonal _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactPersonal"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ContactPersonal();
    }

    /// <summary>
    /// Checks that the FirstName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetFirstName()
    {
        // Arrange
        var testValue = "TestValue1806980274";

        // Act
        this._testClass.FirstName = testValue;

        // Assert
        this._testClass.FirstName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the LastName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLastName()
    {
        // Arrange
        var testValue = "TestValue2092264957";

        // Act
        this._testClass.LastName = testValue;

        // Assert
        this._testClass.LastName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Email property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        // Arrange
        var testValue = "TestValue103932282";

        // Act
        this._testClass.Email = testValue;

        // Assert
        this._testClass.Email.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PhoneNumber property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumber()
    {
        // Arrange
        var testValue = "TestValue253124633";

        // Act
        this._testClass.PhoneNumber = testValue;

        // Assert
        this._testClass.PhoneNumber.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Birthdate property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBirthdate()
    {
        // Arrange
        var testValue = DateTime.UtcNow;

        // Act
        this._testClass.Birthdate = testValue;

        // Assert
        this._testClass.Birthdate.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PersonalImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalImage()
    {
        // Arrange
        var testValue = "TestValue336105126";

        // Act
        this._testClass.PersonalImage = testValue;

        // Assert
        this._testClass.PersonalImage.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PersonalImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalImageData()
    {
        // Arrange
        var testValue = new byte[] { 86, 71, 210, 254 };

        // Act
        this._testClass.PersonalImageData = testValue;

        // Assert
        this._testClass.PersonalImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the ContactId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContactId()
    {
        // Arrange
        var testValue = 509559033L;

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
            Name = "TestValue1380844268",
            Notes = "TestValue1970126991",
            Type = ContactType.Bussines,
            PersonalId = 1202176931L,
            Personal = new ContactPersonal
            {
                FirstName = "TestValue800716534",
                LastName = "TestValue1274670622",
                Email = "TestValue2134246512",
                PhoneNumber = "TestValue1621317752",
                Birthdate = DateTime.UtcNow,
                PersonalImage = "TestValue1329117224",
                PersonalImageData = new byte[] { 149, 243, 168, 34 },
                ContactId = 1546073399L,
                Contact = default(Contact)
            },
            AddressId = 954453802L,
            Address = new ContactAddress
            {
                CountryName = "TestValue883354784",
                State = "TestValue2009224720",
                City = "TestValue172288339",
                Postcode = "TestValue621537246",
                Street = "TestValue380340807",
                Building = "TestValue1347031823",
                Apartment = "TestValue859676462",
                Notes = "TestValue1478348715",
                ContactId = 269004158L,
                Contact = default(Contact)
            },
            ProfessionalId = 1559820726L,
            Professional = new ContactProfessional
            {
                ProfessionIndustry = "TestValue450259793",
                Profession = "TestValue1593362906",
                ProfessionalEmail = "TestValue623325358",
                ProfessionalPhoneNumber = "TestValue2069570532",
                ProfessionalSocialMedia = "TestValue69037082",
                ProfessionalWebsites = "TestValue291035811",
                ProfessionalExperience = 32121.8164F,
                ContactId = 1397344079L,
                Contact = default(Contact)
            },
            OrganizationId = 991283139L,
            Organization = new Organization
            {
                OrganizationIndustry = "TestValue1880207382",
                OrganizationName = "TestValue1294743637",
                OrganizationFullName = "TestValue1616766905",
                PositionInOrganization = "TestValue1052946976",
                OrganizationWebsites = "TestValue1356053560",
                OrganizationSize = OrganizationSize.Nano,
                OrganizationImage = "TestValue288443222",
                OrganizationImageData = new byte[] { 205, 97, 204, 69 },
                ContactId = 387241453L,
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