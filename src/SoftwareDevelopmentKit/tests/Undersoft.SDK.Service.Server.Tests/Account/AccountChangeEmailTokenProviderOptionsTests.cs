using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountChangeEmailTokenProviderOptions"/>.
/// </summary>
[TestClass]
public class AccountChangeEmailTokenProviderOptionsTests
{
    private AccountChangeEmailTokenProviderOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountChangeEmailTokenProviderOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountChangeEmailTokenProviderOptions();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountChangeEmailTokenProviderOptions();

        // Assert
        instance.ShouldNotBeNull();
    }
}