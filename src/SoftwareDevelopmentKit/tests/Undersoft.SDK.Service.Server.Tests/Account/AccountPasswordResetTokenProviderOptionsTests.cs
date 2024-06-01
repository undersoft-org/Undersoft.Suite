using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountPasswordResetTokenProviderOptions"/>.
/// </summary>
[TestClass]
public class AccountPasswordResetTokenProviderOptionsTests
{
    private AccountPasswordResetTokenProviderOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountPasswordResetTokenProviderOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountPasswordResetTokenProviderOptions();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountPasswordResetTokenProviderOptions();

        // Assert
        instance.ShouldNotBeNull();
    }
}