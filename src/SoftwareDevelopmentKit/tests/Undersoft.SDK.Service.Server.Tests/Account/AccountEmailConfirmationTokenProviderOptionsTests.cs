using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountEmailConfirmationTokenProviderOptions"/>.
/// </summary>
[TestClass]
public class AccountEmailConfirmationTokenProviderOptionsTests
{
    private AccountEmailConfirmationTokenProviderOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountEmailConfirmationTokenProviderOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountEmailConfirmationTokenProviderOptions();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountEmailConfirmationTokenProviderOptions();

        // Assert
        instance.ShouldNotBeNull();
    }
}