using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountRegistrationConfirmationTokenProviderOptions"/>.
/// </summary>
[TestClass]
public class AccountRegistrationConfirmationTokenProviderOptionsTests
{
    private AccountRegistrationConfirmationTokenProviderOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountRegistrationConfirmationTokenProviderOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountRegistrationConfirmationTokenProviderOptions();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountRegistrationConfirmationTokenProviderOptions();

        // Assert
        instance.ShouldNotBeNull();
    }
}