using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Tests.Access;

/// <summary>
/// Unit tests for the type <see cref="Credentials"/>.
/// </summary>
[TestClass]
public class CredentialsTests
{
    private Credentials _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Credentials"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Credentials();
    }

    /// <summary>
    /// Checks that setting the Site property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSite()
    {
        this._testClass.CheckProperty(x => x.Site, new ServiceSite?(), new ServiceSite?());
    }

    /// <summary>
    /// Checks that setting the Type property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetType()
    {
        this._testClass.CheckProperty(x => x.Type, new IdentityType?(), new IdentityType?());
    }

    /// <summary>
    /// Checks that setting the UserName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetUserName()
    {
        this._testClass.CheckProperty(x => x.UserName);
    }

    /// <summary>
    /// Checks that setting the NormalizedUserName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNormalizedUserName()
    {
        this._testClass.CheckProperty(x => x.NormalizedUserName);
    }

    /// <summary>
    /// Checks that setting the Email property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        this._testClass.CheckProperty(x => x.Email);
    }

    /// <summary>
    /// Checks that setting the OldPassword property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOldPassword()
    {
        this._testClass.CheckProperty(x => x.OldPassword);
    }

    /// <summary>
    /// Checks that setting the Password property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPassword()
    {
        this._testClass.CheckProperty(x => x.Password);
    }

    /// <summary>
    /// Checks that setting the PhoneNumber property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumber()
    {
        this._testClass.CheckProperty(x => x.PhoneNumber);
    }

    /// <summary>
    /// Checks that setting the EmailConfirmed property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmailConfirmed()
    {
        this._testClass.CheckProperty(x => x.EmailConfirmed);
    }

    /// <summary>
    /// Checks that setting the PhoneNumberConfirmed property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumberConfirmed()
    {
        this._testClass.CheckProperty(x => x.PhoneNumberConfirmed);
    }

    /// <summary>
    /// Checks that setting the RegistrationCompleted property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRegistrationCompleted()
    {
        this._testClass.CheckProperty(x => x.RegistrationCompleted);
    }

    /// <summary>
    /// Checks that setting the SessionToken property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSessionToken()
    {
        this._testClass.CheckProperty(x => x.SessionToken);
    }

    /// <summary>
    /// Checks that setting the PasswordResetToken property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPasswordResetToken()
    {
        this._testClass.CheckProperty(x => x.PasswordResetToken);
    }

    /// <summary>
    /// Checks that setting the EmailConfirmationToken property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmailConfirmationToken()
    {
        this._testClass.CheckProperty(x => x.EmailConfirmationToken);
    }

    /// <summary>
    /// Checks that setting the PhoneNumberConfirmationToken property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumberConfirmationToken()
    {
        this._testClass.CheckProperty(x => x.PhoneNumberConfirmationToken);
    }

    /// <summary>
    /// Checks that setting the RegistrationCompleteToken property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRegistrationCompleteToken()
    {
        this._testClass.CheckProperty(x => x.RegistrationCompleteToken);
    }

    /// <summary>
    /// Checks that setting the AccessFailedCount property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccessFailedCount()
    {
        this._testClass.CheckProperty(x => x.AccessFailedCount);
    }

    /// <summary>
    /// Checks that setting the SaveAccountInCookies property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSaveAccountInCookies()
    {
        this._testClass.CheckProperty(x => x.SaveAccountInCookies);
    }

    /// <summary>
    /// Checks that setting the Authenticated property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAuthenticated()
    {
        this._testClass.CheckProperty(x => x.Authenticated);
    }

    /// <summary>
    /// Checks that setting the IsLockedOut property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIsLockedOut()
    {
        this._testClass.CheckProperty(x => x.IsLockedOut);
    }

    /// <summary>
    /// Checks that setting the ReturnPath property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetReturnPath()
    {
        this._testClass.CheckProperty(x => x.ReturnPath);
    }

    /// <summary>
    /// Checks that setting the RetypedPassword property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRetypedPassword()
    {
        this._testClass.CheckProperty(x => x.RetypedPassword);
    }

    /// <summary>
    /// Checks that setting the FirstName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetFirstName()
    {
        this._testClass.CheckProperty(x => x.FirstName);
    }

    /// <summary>
    /// Checks that setting the LastName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLastName()
    {
        this._testClass.CheckProperty(x => x.LastName);
    }

    /// <summary>
    /// Checks that setting the TermsConsent property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTermsConsent()
    {
        this._testClass.CheckProperty(x => x.TermsConsent);
    }

    /// <summary>
    /// Checks that setting the CookiesConsent property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCookiesConsent()
    {
        this._testClass.CheckProperty(x => x.CookiesConsent);
    }

    /// <summary>
    /// Checks that setting the OptionalConsent property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOptionalConsent()
    {
        this._testClass.CheckProperty(x => x.OptionalConsent);
    }

    /// <summary>
    /// Checks that setting the NewPassword property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNewPassword()
    {
        this._testClass.CheckProperty(x => x.NewPassword);
    }
}