using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;
using TUser = System.String;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountPasswordResetTokenProvider"/>.
/// </summary>
[TestClass]
public class AccountPasswordResetTokenProvider_1Tests
{
    private AccountPasswordResetTokenProvider<TUser> _testClass;
    private IDataProtectionProvider _dataProtectionProvider;
    private IOptions<AccountPasswordResetTokenProviderOptions> _options;
    private ILogger<DataProtectorTokenProvider<TUser>> _logger;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountPasswordResetTokenProvider"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._dataProtectionProvider = Substitute.For<IDataProtectionProvider>();
        this._options = Substitute.For<IOptions<AccountPasswordResetTokenProviderOptions>>();
        this._logger = Substitute.For<ILogger<DataProtectorTokenProvider<TUser>>>();
        this._testClass = new AccountPasswordResetTokenProvider<TUser>(this._dataProtectionProvider, this._options, this._logger);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountPasswordResetTokenProvider<TUser>(this._dataProtectionProvider, this._options, this._logger);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the dataProtectionProvider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullDataProtectionProvider()
    {
        Should.Throw<ArgumentNullException>(() => new AccountPasswordResetTokenProvider<TUser>(default(IDataProtectionProvider), this._options, this._logger));
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new AccountPasswordResetTokenProvider<TUser>(this._dataProtectionProvider, default(IOptions<AccountPasswordResetTokenProviderOptions>), this._logger));
    }

    /// <summary>
    /// Checks that instance construction throws when the logger parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullLogger()
    {
        Should.Throw<ArgumentNullException>(() => new AccountPasswordResetTokenProvider<TUser>(this._dataProtectionProvider, this._options, default(ILogger<DataProtectorTokenProvider<TUser>>)));
    }
}