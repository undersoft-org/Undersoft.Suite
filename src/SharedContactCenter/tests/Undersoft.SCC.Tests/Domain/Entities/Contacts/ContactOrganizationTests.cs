using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities.Contacts;

/// <summary>
/// Unit tests for the type <see cref="Organization"/>.
/// </summary>
[TestClass]
public class ContactOrganizationTests
{
    private Organization _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Organization"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Organization();
    }

    /// <summary>
    /// Checks that the OrganizationIndustry property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationIndustry()
    {
        // Arrange
        var testValue = "TestValue598560353";

        // Act
        this._testClass.OrganizationIndustry = testValue;

        // Assert
        this._testClass.OrganizationIndustry.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationName()
    {
        // Arrange
        var testValue = "TestValue1465392067";

        // Act
        this._testClass.OrganizationName = testValue;

        // Assert
        this._testClass.OrganizationName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationFullName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationFullName()
    {
        // Arrange
        var testValue = "TestValue363566634";

        // Act
        this._testClass.OrganizationFullName = testValue;

        // Assert
        this._testClass.OrganizationFullName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PositionInOrganization property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPositionInOrganization()
    {
        // Arrange
        var testValue = "TestValue1042760567";

        // Act
        this._testClass.PositionInOrganization = testValue;

        // Assert
        this._testClass.PositionInOrganization.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationWebsites property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationWebsites()
    {
        // Arrange
        var testValue = "TestValue875503531";

        // Act
        this._testClass.OrganizationWebsites = testValue;

        // Assert
        this._testClass.OrganizationWebsites.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationSize property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationSize()
    {
        // Arrange
        var testValue = OrganizationSize.Medium;

        // Act
        this._testClass.OrganizationSize = testValue;

        // Assert
        this._testClass.OrganizationSize.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationImage()
    {
        // Arrange
        var testValue = "TestValue191936221";

        // Act
        this._testClass.OrganizationImage = testValue;

        // Assert
        this._testClass.OrganizationImage.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationImageData()
    {
        // Arrange
        var testValue = new byte[] { 142, 179, 124, 2 };

        // Act
        this._testClass.OrganizationImageData = testValue;

        // Assert
        this._testClass.OrganizationImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the ContactId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContactId()
    {
        // Arrange
        var testValue = 460217394L;

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
            Name = "TestValue294879352",
            Notes = "TestValue95485360",
            Type = ContactType.Private,
            PersonalId = 974787185L,
            Personal = new ContactPersonal
            {
                FirstName = "TestValue495542731",
                LastName = "TestValue1815274138",
                Email = "TestValue1180230509",
                PhoneNumber = "TestValue990383741",
                Birthdate = DateTime.UtcNow,
                PersonalImage = "TestValue1917144616",
                PersonalImageData = new byte[] { 215, 175, 199, 125 },
                ContactId = 553557383L,
                Contact = default(Contact)
            },
            AddressId = 2055362342L,
            Address = new ContactAddress
            {
                CountryName = "TestValue1723329831",
                State = "TestValue1024495039",
                City = "TestValue1839996911",
                Postcode = "TestValue351032230",
                Street = "TestValue1386618870",
                Building = "TestValue622972019",
                Apartment = "TestValue1793965837",
                Notes = "TestValue1333099296",
                ContactId = 849747437L,
                Contact = default(Contact)
            },
            ProfessionalId = 992036263L,
            Professional = new ContactProfessional
            {
                ProfessionIndustry = "TestValue746055294",
                Profession = "TestValue1541263501",
                ProfessionalEmail = "TestValue2131956519",
                ProfessionalPhoneNumber = "TestValue165901578",
                ProfessionalSocialMedia = "TestValue1053931760",
                ProfessionalWebsites = "TestValue2081229266",
                ProfessionalExperience = 22871.48F,
                ContactId = 1676435421L,
                Contact = default(Contact)
            },
            OrganizationId = 1569676812L,
            Organization = new Organization
            {
                OrganizationIndustry = "TestValue2038591509",
                OrganizationName = "TestValue263228236",
                OrganizationFullName = "TestValue1211172076",
                PositionInOrganization = "TestValue1100137903",
                OrganizationWebsites = "TestValue353064782",
                OrganizationSize = OrganizationSize.Nano,
                OrganizationImage = "TestValue1205044084",
                OrganizationImageData = new byte[] { 49, 135, 110, 253 },
                ContactId = 831928685L,
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