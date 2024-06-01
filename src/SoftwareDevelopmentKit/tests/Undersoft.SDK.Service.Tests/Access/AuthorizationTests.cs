using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SDK.Service.Tests.Access;

/// <summary>
/// Unit tests for the type <see cref="Authorization"/>.
/// </summary>
[TestClass]
public class AuthorizationTests
{
    private Authorization _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Authorization"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Authorization();
    }

    /// <summary>
    /// Checks that the Map method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMap()
    {
        // Arrange
        var user = new object();

        // Act
        this._testClass.Map(user);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Map method throws when the user parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallMapWithNullUser()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Map(default(object)));
    }

    /// <summary>
    /// Checks that setting the Credentials property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCredentials()
    {
        this._testClass.CheckProperty(x => x.Credentials, new Credentials
        {
            Site = new ServiceSite?(),
            Type = new IdentityType?(),
            UserName = "TestValue1617325441",
            NormalizedUserName = "TestValue1244027538",
            Email = "TestValue1655919007",
            OldPassword = "TestValue1863784015",
            Password = "TestValue1830660713",
            PhoneNumber = "TestValue550429598",
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            RegistrationCompleted = true,
            SessionToken = "TestValue527447981",
            PasswordResetToken = "TestValue596357980",
            EmailConfirmationToken = "TestValue1569811525",
            PhoneNumberConfirmationToken = "TestValue82834646",
            RegistrationCompleteToken = "TestValue1712218359",
            AccessFailedCount = 1325580245,
            SaveAccountInCookies = true,
            Authenticated = true,
            IsLockedOut = true,
            ReturnPath = "TestValue1645733035",
            RetypedPassword = "TestValue710814162",
            FirstName = "TestValue1333169225",
            LastName = "TestValue319719678",
            TermsConsent = false,
            CookiesConsent = true,
            OptionalConsent = false,
            NewPassword = "TestValue1400091713"
        }, new Credentials
        {
            Site = new ServiceSite?(),
            Type = new IdentityType?(),
            UserName = "TestValue1961191331",
            NormalizedUserName = "TestValue887848976",
            Email = "TestValue477356170",
            OldPassword = "TestValue2045819180",
            Password = "TestValue921954779",
            PhoneNumber = "TestValue1168061491",
            EmailConfirmed = true,
            PhoneNumberConfirmed = false,
            RegistrationCompleted = false,
            SessionToken = "TestValue1732157213",
            PasswordResetToken = "TestValue693067037",
            EmailConfirmationToken = "TestValue686977249",
            PhoneNumberConfirmationToken = "TestValue1302295284",
            RegistrationCompleteToken = "TestValue916674683",
            AccessFailedCount = 548177822,
            SaveAccountInCookies = false,
            Authenticated = true,
            IsLockedOut = true,
            ReturnPath = "TestValue799089285",
            RetypedPassword = "TestValue1228458754",
            FirstName = "TestValue1571963501",
            LastName = "TestValue728422631",
            TermsConsent = false,
            CookiesConsent = false,
            OptionalConsent = false,
            NewPassword = "TestValue1891807977"
        });
    }

    /// <summary>
    /// Checks that setting the Notes property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNotes()
    {
        this._testClass.CheckProperty(x => x.Notes, new OperationNotes
        {
            Errors = "TestValue252348651",
            Success = "TestValue98036597",
            Info = "TestValue1827380811",
            Status = SigningStatus.RegistrationCompleted
        }, new OperationNotes
        {
            Errors = "TestValue1041840268",
            Success = "TestValue318835822",
            Info = "TestValue1874580162",
            Status = SigningStatus.SignedIn
        });
    }

    /// <summary>
    /// Checks that setting the IsAvailable property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIsAvailable()
    {
        this._testClass.CheckProperty(x => x.IsAvailable);
    }

    /// <summary>
    /// Checks that setting the Authenticated property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAuthenticated()
    {
        this._testClass.CheckProperty(x => x.Authenticated);
    }
}