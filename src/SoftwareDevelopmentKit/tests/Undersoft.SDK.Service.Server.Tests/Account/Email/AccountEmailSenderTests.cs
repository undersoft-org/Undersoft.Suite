using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts.Email;

namespace Undersoft.SDK.Service.Server.Tests.Accounts.Email;

/// <summary>
/// Unit tests for the type <see cref="AccountEmailSender"/>.
/// </summary>
[TestClass]
public class AccountEmailSenderTests
{
    private AccountEmailSender _testClass;
    private IOptions<AccountEmailSenderOptions> _optionsAccessor;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountEmailSender"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._optionsAccessor = Substitute.For<IOptions<AccountEmailSenderOptions>>();
        this._testClass = new AccountEmailSender(this._optionsAccessor);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountEmailSender(this._optionsAccessor);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the optionsAccessor parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptionsAccessor()
    {
        Should.Throw<ArgumentNullException>(() => new AccountEmailSender(default(IOptions<AccountEmailSenderOptions>)));
    }

    /// <summary>
    /// Checks that the SendEmailAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSendEmailAsync()
    {
        // Arrange
        var toEmail = "TestValue1128456560";
        var subject = "TestValue1454101853";
        var message = "TestValue111687319";

        // Act
        await this._testClass.SendEmailAsync(toEmail, subject, message);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SendEmailAsync method throws when the toEmail parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSendEmailAsyncWithInvalidToEmailAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SendEmailAsync(value, "TestValue1512835843", "TestValue1196480791"));
    }

    /// <summary>
    /// Checks that the SendEmailAsync method throws when the subject parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSendEmailAsyncWithInvalidSubjectAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SendEmailAsync("TestValue2052858227", value, "TestValue161099522"));
    }

    /// <summary>
    /// Checks that the SendEmailAsync method throws when the message parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSendEmailAsyncWithInvalidMessageAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SendEmailAsync("TestValue806988198", "TestValue401444183", value));
    }

    /// <summary>
    /// Checks that the Execute method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallExecuteAsync()
    {
        // Arrange
        var apiKey = "TestValue731486793";
        var subject = "TestValue1956849230";
        var message = "TestValue800448762";
        var toEmail = "TestValue1573836511";

        // Act
        await this._testClass.Execute(apiKey, subject, message, toEmail);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Execute method throws when the apiKey parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallExecuteWithInvalidApiKeyAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Execute(value, "TestValue293011720", "TestValue1288435008", "TestValue1561841622"));
    }

    /// <summary>
    /// Checks that the Execute method throws when the subject parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallExecuteWithInvalidSubjectAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Execute("TestValue837104716", value, "TestValue749147067", "TestValue456066627"));
    }

    /// <summary>
    /// Checks that the Execute method throws when the message parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallExecuteWithInvalidMessageAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Execute("TestValue1398659664", "TestValue1680747643", value, "TestValue330171259"));
    }

    /// <summary>
    /// Checks that the Execute method throws when the toEmail parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallExecuteWithInvalidToEmailAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Execute("TestValue1847492000", "TestValue1412877197", "TestValue831037024", value));
    }

    /// <summary>
    /// Checks that the Options property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetOptions()
    {
        // Assert
        this._testClass.Options.ShouldBeOfType<AccountEmailSenderOptions>();

        Assert.Fail("Create or modify test");
    }
}