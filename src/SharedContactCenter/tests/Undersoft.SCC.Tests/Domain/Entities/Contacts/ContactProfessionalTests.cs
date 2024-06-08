using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities.Contacts;

/// <summary>
/// Unit tests for the type <see cref="ContactProfessional"/>.
/// </summary>
[TestClass]
public class ContactProfessionalTests
{
    private ContactProfessional _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactProfessional"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ContactProfessional();
    }

    /// <summary>
    /// Checks that the ProfessionIndustry property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionIndustry()
    {
        // Arrange
        var testValue = "TestValue364846711";

        // Act
        this._testClass.ProfessionIndustry = testValue;

        // Assert
        this._testClass.ProfessionIndustry.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Profession property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfession()
    {
        // Arrange
        var testValue = "TestValue353480581";

        // Act
        this._testClass.Profession = testValue;

        // Assert
        this._testClass.Profession.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalEmail property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalEmail()
    {
        // Arrange
        var testValue = "TestValue1039476513";

        // Act
        this._testClass.ProfessionalEmail = testValue;

        // Assert
        this._testClass.ProfessionalEmail.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalPhoneNumber property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalPhoneNumber()
    {
        // Arrange
        var testValue = "TestValue1786444313";

        // Act
        this._testClass.ProfessionalPhoneNumber = testValue;

        // Assert
        this._testClass.ProfessionalPhoneNumber.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalSocialMedia property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalSocialMedia()
    {
        // Arrange
        var testValue = "TestValue186162463";

        // Act
        this._testClass.ProfessionalSocialMedia = testValue;

        // Assert
        this._testClass.ProfessionalSocialMedia.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalWebsites property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalWebsites()
    {
        // Arrange
        var testValue = "TestValue2059731166";

        // Act
        this._testClass.ProfessionalWebsites = testValue;

        // Assert
        this._testClass.ProfessionalWebsites.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalExperience property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalExperience()
    {
        // Arrange
        var testValue = 24617.6152F;

        // Act
        this._testClass.ProfessionalExperience = testValue;

        // Assert
        this._testClass.ProfessionalExperience.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ContactId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContactId()
    {
        // Arrange
        var testValue = 992930309L;

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
            Name = "TestValue1668095523",
            Notes = "TestValue1689907431",
            Type = ContactType.Generic,
            PersonalId = 732934119L,
            Personal = new ContactPersonal
            {
                FirstName = "TestValue1185200942",
                LastName = "TestValue617260579",
                Email = "TestValue1701169971",
                PhoneNumber = "TestValue940770987",
                Birthdate = DateTime.UtcNow,
                PersonalImage = "TestValue186266627",
                PersonalImageData = new byte[] { 146, 244, 131, 135 },
                ContactId = 385043771L,
                Contact = default(Contact)
            },
            AddressId = 1766220160L,
            Address = new ContactAddress
            {
                Country = "TestValue454310088",
                State = "TestValue1004228451",
                City = "TestValue662594550",
                Postcode = "TestValue1693170620",
                Street = "TestValue2105605254",
                Building = "TestValue1527749949",
                Apartment = "TestValue1314375517",
                Notes = "TestValue1739889543",
                ContactId = 473889778L,
                Contact = default(Contact)
            },
            ProfessionalId = 1153009034L,
            Professional = new ContactProfessional
            {
                ProfessionIndustry = "TestValue1185699165",
                Profession = "TestValue807732562",
                ProfessionalEmail = "TestValue443289836",
                ProfessionalPhoneNumber = "TestValue1996027510",
                ProfessionalSocialMedia = "TestValue1685693861",
                ProfessionalWebsites = "TestValue283174080",
                ProfessionalExperience = 21907.1074F,
                ContactId = 395506091L,
                Contact = default(Contact)
            },
            OrganizationId = 2106574726L,
            Organization = new ContactOrganization
            {
                OrganizationIndustry = "TestValue116739095",
                OrganizationName = "TestValue138848498",
                OrganizationFullName = "TestValue874614819",
                PositionInOrganization = "TestValue70031242",
                OrganizationWebsites = "TestValue1713759681",
                OrganizationSize = OrganizationSize.Micro,
                OrganizationImage = "TestValue1451910495",
                OrganizationImageData = new byte[] { 235, 87, 112, 213 },
                ContactId = 1796028367L,
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