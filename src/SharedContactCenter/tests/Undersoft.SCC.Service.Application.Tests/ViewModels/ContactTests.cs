using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Application.ViewModels;
using Undersoft.SCC.Service.Application.ViewModels.Contacts;
using Undersoft.SDK.Series;

namespace Undersoft.SCC.Service.Application.Tests.ViewModels;

/// <summary>
/// Unit tests for the type <see cref="Contact"/>.
/// </summary>
[TestClass]
public class ContactTests
{
    private Contact _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Contact"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Contact();
    }

    /// <summary>
    /// Checks that the PersonalImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalImage()
    {
        // Arrange
        var testValue = "TestValue426491718";

        // Act
        this._testClass.PersonalImage = testValue;

        // Assert
        this._testClass.PersonalImage.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue1215848370";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PhoneNumber property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumber()
    {
        // Arrange
        var testValue = "TestValue1296389158";

        // Act
        this._testClass.PhoneNumber = testValue;

        // Assert
        this._testClass.PhoneNumber.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Email property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        // Arrange
        var testValue = "TestValue323521445";

        // Act
        this._testClass.Email = testValue;

        // Assert
        this._testClass.Email.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ContactAddress property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContactAddress()
    {
        // Arrange
        var testValue = "TestValue2116284540";

        // Act
        this._testClass.ContactAddress = testValue;

        // Assert
        this._testClass.ContactAddress.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Type property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetType()
    {
        // Arrange
        var testValue = ContactType.Additional;

        // Act
        this._testClass.Type = testValue;

        // Assert
        this._testClass.Type.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PersonaImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonaImageData()
    {
        // Arrange
        var testValue = new byte[] { 148, 229, 148, 107 };

        // Act
        this._testClass.PersonaImageData = testValue;

        // Assert
        this._testClass.PersonaImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Notes property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNotes()
    {
        // Arrange
        var testValue = "TestValue1046118271";

        // Act
        this._testClass.Notes = testValue;

        // Assert
        this._testClass.Notes.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PersonalId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalId()
    {
        // Arrange
        var testValue = 302883887L;

        // Act
        this._testClass.PersonalId = testValue;

        // Assert
        this._testClass.PersonalId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Personal property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonal()
    {
        // Arrange
        var testValue = new ContactPersonal
        {
            FirstName = "TestValue1987572324",
            LastName = "TestValue120847775",
            Email = "TestValue662790153",
            PhoneNumber = "TestValue1832017787",
            Birthdate = DateTime.UtcNow,
            PersonalImage = "TestValue902885572",
            PersonalImageData = new byte[] { 26, 156, 111, 99 },
            ContactId = 1245424571L
        };

        // Act
        this._testClass.Personal = testValue;

        // Assert
        this._testClass.Personal.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the AddressId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAddressId()
    {
        // Arrange
        var testValue = 138684650L;

        // Act
        this._testClass.AddressId = testValue;

        // Assert
        this._testClass.AddressId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Address property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAddress()
    {
        // Arrange
        var testValue = new ContactAddress
        {
            Country = "TestValue1727075282",
            State = "TestValue2087845749",
            City = "TestValue179920692",
            Postcode = "TestValue1779643639",
            Street = "TestValue1272643725",
            Building = "TestValue498192262",
            Apartment = "TestValue1642082596",
            Notes = "TestValue2085969569",
            ContactId = 480216843L
        };

        // Act
        this._testClass.Address = testValue;

        // Assert
        this._testClass.Address.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the ProfessionalId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessionalId()
    {
        // Arrange
        var testValue = 667052908L;

        // Act
        this._testClass.ProfessionalId = testValue;

        // Assert
        this._testClass.ProfessionalId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Professional property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfessional()
    {
        // Arrange
        var testValue = new ContactProfessional
        {
            ProfessionIndustry = "TestValue685283967",
            Profession = "TestValue1046666361",
            ProfessionalEmail = "TestValue1140203006",
            ProfessionalPhoneNumber = "TestValue218628467",
            ProfessionalSocialMedia = "TestValue1936159133",
            ProfessionalWebsites = "TestValue1376549713",
            ProfessionalExperience = 24001.0156F,
            ContactId = 1421720060L
        };

        // Act
        this._testClass.Professional = testValue;

        // Assert
        this._testClass.Professional.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the OrganizationId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganizationId()
    {
        // Arrange
        var testValue = 653069829L;

        // Act
        this._testClass.OrganizationId = testValue;

        // Assert
        this._testClass.OrganizationId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Organization property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOrganization()
    {
        // Arrange
        var testValue = new ContactOrganization
        {
            OrganizationIndustry = "TestValue1448417407",
            OrganizationName = "TestValue1069477438",
            OrganizationFullName = "TestValue2112762495",
            PositionInOrganization = "TestValue1856671542",
            OrganizationWebsites = "TestValue1061935387",
            OrganizationSize = OrganizationSize.Small,
            OrganizationImage = "TestValue1866660702",
            OrganizationImageData = new byte[] { 13, 26, 62, 124 },
            ContactId = 585871651L
        };

        // Act
        this._testClass.Organization = testValue;

        // Assert
        this._testClass.Organization.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Groups property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGroups()
    {
        // Arrange
        var testValue = new Listing<Group>();

        // Act
        this._testClass.Groups = testValue;

        // Assert
        this._testClass.Groups.ShouldBeSameAs(testValue);
    }
}