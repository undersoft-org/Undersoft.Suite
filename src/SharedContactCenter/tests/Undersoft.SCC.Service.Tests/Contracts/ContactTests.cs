using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SCC.Service.Contracts.Contacts;
using Undersoft.SDK.Series;

namespace Undersoft.SCC.Service.Tests.Contracts;

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
    /// Checks that the Type property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetType()
    {
        // Arrange
        var testValue = ContactType.Generic;

        // Act
        this._testClass.Type = testValue;

        // Assert
        this._testClass.Type.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Notes property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNotes()
    {
        // Arrange
        var testValue = "TestValue1675372525";

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
        var testValue = 1055824578L;

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
            FirstName = "TestValue778259845",
            LastName = "TestValue945851623",
            Email = "TestValue469104203",
            PhoneNumber = "TestValue2020886145",
            Birthdate = DateTime.UtcNow,
            PersonalImage = "TestValue1722591878",
            PersonalImageData = new byte[] { 122, 110, 193, 208 },
            ContactId = 1734508763L
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
        var testValue = 1802527498L;

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
            CountryName = "TestValue768946341",
            State = "TestValue870392399",
            City = "TestValue937394175",
            Postcode = "TestValue611515208",
            Street = "TestValue894783745",
            Building = "TestValue495373343",
            Apartment = "TestValue1775073891",
            Notes = "TestValue1673239875",
            ContactId = 1119679369L
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
        var testValue = 725045254L;

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
            ProfessionIndustry = "TestValue2085464671",
            Profession = "TestValue124028291",
            ProfessionalEmail = "TestValue270877578",
            ProfessionalPhoneNumber = "TestValue859893794",
            ProfessionalSocialMedia = "TestValue65888463",
            ProfessionalWebsites = "TestValue1550197118",
            ProfessionalExperience = 22471.7285F,
            ContactId = 1003137000L
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
        var testValue = 1235421824L;

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
        var testValue = new Organization
        {
            OrganizationIndustry = "TestValue966015966",
            OrganizationName = "TestValue1300230043",
            OrganizationFullName = "TestValue1521656239",
            PositionInOrganization = "TestValue1204730234",
            OrganizationWebsites = "TestValue594919737",
            OrganizationSize = OrganizationSize.Micro,
            OrganizationImage = "TestValue2144842092",
            OrganizationImageData = new byte[] { 135, 249, 176, 16 },
            ContactId = 68916203L
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