using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SCC.Service.Contracts.Accounts;
using Undersoft.SDK.Series;

namespace Undersoft.SCC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Account"/>.
/// </summary>
[TestClass]
public class AccountTests
{
    private Account _testClass;
    private string _email;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Account"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._email = "TestValue664628685";
        this._testClass = new Account(this._email);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Account();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Account(this._email);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the constructor throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidEmail(string value)
    {
        Should.Throw<ArgumentNullException>(() => new Account(value));
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue1413402212";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Email property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void EmailIsInitializedCorrectly()
    {
        this._testClass.Email.ShouldBe(this._email);
    }

    /// <summary>
    /// Checks that the Email property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        // Arrange
        var testValue = "TestValue1005548019";

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
        var testValue = "TestValue1233793438";

        // Act
        this._testClass.PhoneNumber = testValue;

        // Assert
        this._testClass.PhoneNumber.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the RoleString property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRoleString()
    {
        // Arrange
        var testValue = "TestValue741562160";

        // Act
        this._testClass.RoleString = testValue;

        // Assert
        this._testClass.RoleString.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Image property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetImage()
    {
        // Arrange
        var testValue = "TestValue735541775";

        // Act
        this._testClass.Image = testValue;

        // Assert
        this._testClass.Image.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetImageData()
    {
        // Arrange
        var testValue = new byte[] { 192, 108, 39, 34 };

        // Act
        this._testClass.ImageData = testValue;

        // Assert
        this._testClass.ImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the User property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetUser()
    {
        // Arrange
        var testValue = new AccountUser
        {
            UserName = "TestValue323135471",
            NormalizedUserName = "TestValue795347147",
            Email = "TestValue139916826",
            NormalizedEmail = "TestValue776384639",
            EmailConfirmed = true,
            PhoneNumber = "TestValue937875465",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = true,
            LockoutEnd = DateTimeOffset.UtcNow,
            LockoutEnabled = false,
            AccessFailedCount = 1279751866,
            RegistrationCompleted = false,
            IsLockedOut = false
        };

        // Act
        this._testClass.User = testValue;

        // Assert
        this._testClass.User.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Roles property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRoles()
    {
        // Arrange
        var testValue = new Listing<Role>();

        // Act
        this._testClass.Roles = testValue;

        // Assert
        this._testClass.Roles.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Claims property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaims()
    {
        // Arrange
        var testValue = new Listing<Claim>();

        // Act
        this._testClass.Claims = testValue;

        // Assert
        this._testClass.Claims.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the PersonalId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalId()
    {
        // Arrange
        var testValue = 1060399532L;

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
        var testValue = new AccountPersonal
        {
            FirstName = "TestValue800405762",
            LastName = "TestValue1668884233",
            Email = "TestValue1556997230",
            PhoneNumber = "TestValue1869663267",
            Birthdate = DateTime.UtcNow,
            Image = "TestValue273968575",
            ImageData = new byte[] { 74, 244, 7, 229 },
            AccountId = 228307846L
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
        var testValue = 557805987L;

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
        var testValue = new AccountAddress
        {
            Country = "TestValue1586428615",
            State = "TestValue366121816",
            City = "TestValue1817728759",
            Postcode = "TestValue614454322",
            Street = "TestValue1445119742",
            Building = "TestValue239058090",
            Apartment = "TestValue1006507294",
            AccountId = 751533039L
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
        var testValue = 1441150505L;

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
        var testValue = new AccountProfessional
        {
            ProfessionIndustry = "TestValue1901983669",
            Profession = "TestValue907619964",
            ProfessionalEmail = "TestValue1631863906",
            ProfessionalPhoneNumber = "TestValue1280773231",
            ProfessionalSocialMedia = "TestValue1634560957",
            ProfessionalWebsites = "TestValue2022525915",
            ProfessionalExperience = 12400.7285F,
            AccountId = 1508918844L
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
        var testValue = 1566141273L;

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
        var testValue = new AccountOrganization
        {
            OrganizationIndustry = "TestValue271460858",
            OrganizationName = "TestValue588141972",
            OrganizationFullName = "TestValue457957690",
            PositionInOrganization = "TestValue2005645304",
            OrganizationWebsites = "TestValue353002680",
            OrganizationImage = "TestValue205142257",
            OrganizationImageData = new byte[] { 203, 158, 120, 159 },
            AccountId = 285070391L
        };

        // Act
        this._testClass.Organization = testValue;

        // Assert
        this._testClass.Organization.ShouldBeSameAs(testValue);
    }
}