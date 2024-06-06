using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Tests.Domain.Entities;

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
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue1055504807";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Notes property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNotes()
    {
        // Arrange
        var testValue = "TestValue787086235";

        // Act
        this._testClass.Notes = testValue;

        // Assert
        this._testClass.Notes.ShouldBe(testValue);
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
    /// Checks that the PersonalId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalId()
    {
        // Arrange
        var testValue = 2121339367L;

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
            FirstName = "TestValue1084297833",
            LastName = "TestValue438502674",
            Email = "TestValue1021783206",
            PhoneNumber = "TestValue715351249",
            Birthdate = DateTime.UtcNow,
            PersonalImage = "TestValue2053321612",
            PersonalImageData = new byte[] { 56, 74, 16, 137 },
            ContactId = 304917371L,
            Contact = new Contact
            {
                Name = "TestValue228717009",
                Notes = "TestValue1626389831",
                Type = ContactType.Bussines,
                PersonalId = 820489920L,
                Personal = default(ContactPersonal),
                AddressId = 72070569L,
                Address = new Address
                {
                    Country = "TestValue1841052631",
                    State = "TestValue1842082161",
                    City = "TestValue765059608",
                    Postcode = "TestValue1861297380",
                    Street = "TestValue1144822952",
                    Building = "TestValue1713826472",
                    Apartment = "TestValue2132900324",
                    Notes = "TestValue865109329",
                    ContactId = 509882542L,
                    Contact = default(Contact)
                },
                ProfessionalId = 2069465274L,
                Professional = new ContactProfessional
                {
                    ProfessionIndustry = "TestValue1274058000",
                    Profession = "TestValue1790483888",
                    ProfessionalEmail = "TestValue419110581",
                    ProfessionalPhoneNumber = "TestValue891082347",
                    ProfessionalSocialMedia = "TestValue1070803633",
                    ProfessionalWebsites = "TestValue1416409416",
                    ProfessionalExperience = 2434.75122F,
                    ContactId = 1544392442L,
                    Contact = default(Contact)
                },
                OrganizationId = 764805804L,
                Organization = new ContactOrganization
                {
                    OrganizationIndustry = "TestValue2037425187",
                    OrganizationName = "TestValue566299189",
                    OrganizationFullName = "TestValue658316810",
                    PositionInOrganization = "TestValue1044612685",
                    OrganizationWebsites = "TestValue1753635884",
                    OrganizationSize = OrganizationSize.None,
                    OrganizationImage = "TestValue790503849",
                    OrganizationImageData = new byte[] { 107, 88, 43, 31 },
                    ContactId = 1227219701L,
                    Contact = default(Contact)
                },
                Groups = new EntitySet<Group>()
            }
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
        var testValue = 1451101954L;

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
        var testValue = new Address
        {
            Country = "TestValue1616046837",
            State = "TestValue1248456296",
            City = "TestValue1243548307",
            Postcode = "TestValue11280945",
            Street = "TestValue290445199",
            Building = "TestValue1073272502",
            Apartment = "TestValue783070237",
            Notes = "TestValue407512895",
            ContactId = 1098881156L,
            Contact = new Contact
            {
                Name = "TestValue1720876992",
                Notes = "TestValue511900664",
                Type = ContactType.Private,
                PersonalId = 779263612L,
                Personal = new ContactPersonal
                {
                    FirstName = "TestValue832536423",
                    LastName = "TestValue207984291",
                    Email = "TestValue1399151433",
                    PhoneNumber = "TestValue91326628",
                    Birthdate = DateTime.UtcNow,
                    PersonalImage = "TestValue1035991602",
                    PersonalImageData = new byte[] { 8, 9, 72, 110 },
                    ContactId = 1653255027L,
                    Contact = default(Contact)
                },
                AddressId = 1182735821L,
                Address = default(Address),
                ProfessionalId = 797469476L,
                Professional = new ContactProfessional
                {
                    ProfessionIndustry = "TestValue1158907371",
                    Profession = "TestValue1827693749",
                    ProfessionalEmail = "TestValue354319103",
                    ProfessionalPhoneNumber = "TestValue805618959",
                    ProfessionalSocialMedia = "TestValue1389795193",
                    ProfessionalWebsites = "TestValue497610670",
                    ProfessionalExperience = 3726.2146F,
                    ContactId = 842245573L,
                    Contact = default(Contact)
                },
                OrganizationId = 1970439693L,
                Organization = new ContactOrganization
                {
                    OrganizationIndustry = "TestValue174437051",
                    OrganizationName = "TestValue1318137932",
                    OrganizationFullName = "TestValue1795017687",
                    PositionInOrganization = "TestValue1059522687",
                    OrganizationWebsites = "TestValue1125964217",
                    OrganizationSize = OrganizationSize.Small,
                    OrganizationImage = "TestValue761322205",
                    OrganizationImageData = new byte[] { 42, 111, 117, 17 },
                    ContactId = 251243063L,
                    Contact = default(Contact)
                },
                Groups = new EntitySet<Group>()
            }
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
        var testValue = 974372272L;

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
            ProfessionIndustry = "TestValue1348550855",
            Profession = "TestValue582519558",
            ProfessionalEmail = "TestValue1656539727",
            ProfessionalPhoneNumber = "TestValue651778503",
            ProfessionalSocialMedia = "TestValue1478990704",
            ProfessionalWebsites = "TestValue196525599",
            ProfessionalExperience = 17474.1719F,
            ContactId = 836930297L,
            Contact = new Contact
            {
                Name = "TestValue685498457",
                Notes = "TestValue1742684916",
                Type = ContactType.Generic,
                PersonalId = 1361295117L,
                Personal = new ContactPersonal
                {
                    FirstName = "TestValue1279021475",
                    LastName = "TestValue1393062400",
                    Email = "TestValue428751134",
                    PhoneNumber = "TestValue1749377583",
                    Birthdate = DateTime.UtcNow,
                    PersonalImage = "TestValue1856569610",
                    PersonalImageData = new byte[] { 145, 31, 249, 113 },
                    ContactId = 658099372L,
                    Contact = default(Contact)
                },
                AddressId = 1037330006L,
                Address = new Address
                {
                    Country = "TestValue1751617393",
                    State = "TestValue1179287588",
                    City = "TestValue2057511032",
                    Postcode = "TestValue982852057",
                    Street = "TestValue1468158831",
                    Building = "TestValue256878748",
                    Apartment = "TestValue2139487996",
                    Notes = "TestValue660349183",
                    ContactId = 1036319675L,
                    Contact = default(Contact)
                },
                ProfessionalId = 546226413L,
                Professional = default(ContactProfessional),
                OrganizationId = 184535099L,
                Organization = new ContactOrganization
                {
                    OrganizationIndustry = "TestValue479142894",
                    OrganizationName = "TestValue1919283192",
                    OrganizationFullName = "TestValue1296562879",
                    PositionInOrganization = "TestValue738016690",
                    OrganizationWebsites = "TestValue1166103613",
                    OrganizationSize = OrganizationSize.None,
                    OrganizationImage = "TestValue1844506908",
                    OrganizationImageData = new byte[] { 134, 194, 204, 205 },
                    ContactId = 1845711217L,
                    Contact = default(Contact)
                },
                Groups = new EntitySet<Group>()
            }
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
        var testValue = 1994426389L;

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
            OrganizationIndustry = "TestValue1988201119",
            OrganizationName = "TestValue332571071",
            OrganizationFullName = "TestValue755398973",
            PositionInOrganization = "TestValue1229458068",
            OrganizationWebsites = "TestValue1917123169",
            OrganizationSize = OrganizationSize.Enterprise,
            OrganizationImage = "TestValue300119014",
            OrganizationImageData = new byte[] { 2, 81, 200, 243 },
            ContactId = 1619974562L,
            Contact = new Contact
            {
                Name = "TestValue1568963319",
                Notes = "TestValue1361157190",
                Type = ContactType.Additional,
                PersonalId = 580051549L,
                Personal = new ContactPersonal
                {
                    FirstName = "TestValue693494108",
                    LastName = "TestValue1082335733",
                    Email = "TestValue1171976458",
                    PhoneNumber = "TestValue815068704",
                    Birthdate = DateTime.UtcNow,
                    PersonalImage = "TestValue1094486376",
                    PersonalImageData = new byte[] { 108, 78, 53, 132 },
                    ContactId = 57162709L,
                    Contact = default(Contact)
                },
                AddressId = 220008944L,
                Address = new Address
                {
                    Country = "TestValue254100788",
                    State = "TestValue1970281817",
                    City = "TestValue1169160778",
                    Postcode = "TestValue1461876990",
                    Street = "TestValue17412192",
                    Building = "TestValue1481060018",
                    Apartment = "TestValue63084643",
                    Notes = "TestValue1142134585",
                    ContactId = 1135587760L,
                    Contact = default(Contact)
                },
                ProfessionalId = 1648963422L,
                Professional = new ContactProfessional
                {
                    ProfessionIndustry = "TestValue910029928",
                    Profession = "TestValue890709661",
                    ProfessionalEmail = "TestValue1157595533",
                    ProfessionalPhoneNumber = "TestValue246107399",
                    ProfessionalSocialMedia = "TestValue166470393",
                    ProfessionalWebsites = "TestValue1936175058",
                    ProfessionalExperience = 3457.68848F,
                    ContactId = 1391640545L,
                    Contact = default(Contact)
                },
                OrganizationId = 1265525775L,
                Organization = default(ContactOrganization),
                Groups = new EntitySet<Group>()
            }
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
        var testValue = new EntitySet<Group>();

        // Act
        this._testClass.Groups = testValue;

        // Assert
        this._testClass.Groups.ShouldBeSameAs(testValue);
    }
}