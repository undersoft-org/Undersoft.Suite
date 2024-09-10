using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Accounts;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="AccountConsent"/>.
/// </summary>
[TestClass]
public class AccountConsentTests
{
    private AccountConsent _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountConsent"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountConsent();
    }

    /// <summary>
    /// Checks that setting the TermsText property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTermsText()
    {
        this._testClass.CheckProperty(x => x.TermsText);
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
    /// Checks that setting the PersonalDataText property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalDataText()
    {
        this._testClass.CheckProperty(x => x.PersonalDataText);
    }

    /// <summary>
    /// Checks that setting the PersonalDataConsent property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalDataConsent()
    {
        this._testClass.CheckProperty(x => x.PersonalDataConsent);
    }

    /// <summary>
    /// Checks that setting the MarketingText property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMarketingText()
    {
        this._testClass.CheckProperty(x => x.MarketingText);
    }

    /// <summary>
    /// Checks that setting the MarketingConsent property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMarketingConsent()
    {
        this._testClass.CheckProperty(x => x.MarketingConsent);
    }

    /// <summary>
    /// Checks that setting the ThirdPartyText property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetThirdPartyText()
    {
        this._testClass.CheckProperty(x => x.ThirdPartyText);
    }

    /// <summary>
    /// Checks that setting the ThirdPartyConsent property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetThirdPartyConsent()
    {
        this._testClass.CheckProperty(x => x.ThirdPartyConsent);
    }

    /// <summary>
    /// Checks that setting the AccountId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccountId()
    {
        this._testClass.CheckProperty(x => x.AccountId, 49511210L, 1568698341L);
    }
}