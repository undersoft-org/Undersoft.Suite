using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountService"/>.
/// </summary>
[TestClass]
public class AccountServiceTests
{
    private AccountService<Account> _testClass;
    private IServicer _servicer;
    private IAccountManager _accountManager;
    private IEmailSender _email;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountService"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._accountManager = Substitute.For<IAccountManager>();
        this._email = Substitute.For<IEmailSender>();
        this._testClass = new AccountService<Account>(this._servicer, this._accountManager, this._email);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountService<Account>();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new AccountService<Account>(this._servicer, this._accountManager, this._email);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new AccountService<Account>(default(IServicer), this._accountManager, this._email));
    }

    /// <summary>
    /// Checks that instance construction throws when the accountManager parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullAccountManager()
    {
        Should.Throw<ArgumentNullException>(() => new AccountService<Account>(this._servicer, default(IAccountManager), this._email));
    }

    /// <summary>
    /// Checks that instance construction throws when the email parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullEmail()
    {
        Should.Throw<ArgumentNullException>(() => new AccountService<Account>(this._servicer, this._accountManager, default(IEmailSender)));
    }

    /// <summary>
    /// Checks that the SignIn method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSignInAsync()
    {
        // Arrange
        var identity = Substitute.For<IAuthorization>();

        _accountManager.CheckPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(new Account());
        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());
        _accountManager.GetToken(Arg.Any<IAuthorization>()).Returns("TestValue1497070492");
        _accountManager.User.Returns(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()));
        _accountManager.SignIn.Returns(new SignInManager<AccountUser>(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()), Substitute.For<IHttpContextAccessor>(), Substitute.For<IUserClaimsPrincipalFactory<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<ILogger<SignInManager<AccountUser>>>(), Substitute.For<IAuthenticationSchemeProvider>(), Substitute.For<IUserConfirmation<AccountUser>>()));

        // Act
        var result = await this._testClass.SignIn(identity);

        // Assert
        await _servicer.Received().Serve<IEmailSender>(Arg.Any<Func<IEmailSender, Task>>());
        await _accountManager.Received().CheckPassword(Arg.Any<string>(), Arg.Any<string>());
        await _accountManager.Received().GetByEmail(Arg.Any<string>());
        await _accountManager.Received().GetToken(Arg.Any<IAuthorization>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SignIn method throws when the identity parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSignInWithNullIdentityAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SignIn(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the SignUp method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSignUpAsync()
    {
        // Arrange
        var identity = Substitute.For<IAuthorization>();

        _accountManager.SetUser(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>(), Arg.Any<IEnumerable<string>>()).Returns(new Account());
        _accountManager.CheckPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(new Account());
        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());

        // Act
        var result = await this._testClass.SignUp(identity);

        // Assert
        await _servicer.Received().Serve<IEmailSender>(Arg.Any<Func<IEmailSender, Task>>());
        await _accountManager.Received().SetUser(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>(), Arg.Any<IEnumerable<string>>());
        await _accountManager.Received().CheckPassword(Arg.Any<string>(), Arg.Any<string>());
        await _accountManager.Received().GetByEmail(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SignUp method throws when the identity parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSignUpWithNullIdentityAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SignUp(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the SignOut method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSignOutAsync()
    {
        // Arrange
        var identity = Substitute.For<IAuthorization>();

        _accountManager.SignIn.Returns(new SignInManager<AccountUser>(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()), Substitute.For<IHttpContextAccessor>(), Substitute.For<IUserClaimsPrincipalFactory<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<ILogger<SignInManager<AccountUser>>>(), Substitute.For<IAuthenticationSchemeProvider>(), Substitute.For<IUserConfirmation<AccountUser>>()));
        _accountManager.User.Returns(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()));

        // Act
        var result = await this._testClass.SignOut(identity);

        // Assert

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SignOut method throws when the identity parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSignOutWithNullIdentityAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SignOut(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the SignedIn method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSignedInAsync()
    {
        // Arrange
        var identity = Substitute.For<IAuthorization>();

        _accountManager.RenewToken(Arg.Any<string>()).Returns("TestValue1579479797");

        // Act
        var result = await this._testClass.SignedIn(identity);

        // Assert
        await _accountManager.Received().RenewToken(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SignedIn method throws when the identity parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSignedInWithNullIdentityAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SignedIn(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the SignedUp method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSignedUpAsync()
    {
        // Arrange
        var identity = Substitute.For<IAuthorization>();


        // Act
        var result = await this._testClass.SignedUp(identity);

        // Assert

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SignedUp method throws when the identity parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSignedUpWithNullIdentityAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SignedUp(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the Authenticate method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallAuthenticateAsync()
    {
        // Arrange
        var account = Substitute.For<IAuthorization>();

        _accountManager.CheckPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(new Account());

        // Act
        var result = await this._testClass.Authenticate(account);

        // Assert
        await _accountManager.Received().CheckPassword(Arg.Any<string>(), Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Authenticate method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallAuthenticateWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Authenticate(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the ConfirmEmail method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallConfirmEmailAsync()
    {
        // Arrange
        var account = Substitute.For<IAuthorization>();

        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());
        _accountManager.User.Returns(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()));

        // Act
        var result = await this._testClass.ConfirmEmail(account);

        // Assert
        await _servicer.Received().Serve<IEmailSender>(Arg.Any<Func<IEmailSender, Task>>());
        await _accountManager.Received().GetByEmail(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ConfirmEmail method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallConfirmEmailWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.ConfirmEmail(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the ResetPassword method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallResetPasswordAsync()
    {
        // Arrange
        var account = Substitute.For<IAuthorization>();

        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());
        _accountManager.User.Returns(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()));

        // Act
        var result = await this._testClass.ResetPassword(account);

        // Assert
        await _servicer.Received().Serve<IEmailSender>(Arg.Any<Func<IEmailSender, Task>>());
        await _accountManager.Received().GetByEmail(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ResetPassword method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallResetPasswordWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.ResetPassword(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the ChangePassword method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallChangePasswordAsync()
    {
        // Arrange
        var account = Substitute.For<IAuthorization>();

        _accountManager.CheckPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(new Account());
        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());
        _accountManager.User.Returns(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()));

        // Act
        var result = await this._testClass.ChangePassword(account);

        // Assert
        await _accountManager.Received().CheckPassword(Arg.Any<string>(), Arg.Any<string>());
        await _accountManager.Received().GetByEmail(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ChangePassword method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallChangePasswordWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.ChangePassword(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the CompleteRegistration method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallCompleteRegistrationAsync()
    {
        // Arrange
        var account = Substitute.For<IAuthorization>();

        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());
        _accountManager.User.Returns(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()));

        // Act
        var result = await this._testClass.CompleteRegistration(account);

        // Assert
        await _servicer.Received().Serve<IEmailSender>(Arg.Any<Func<IEmailSender, Task>>());
        await _accountManager.Received().GetByEmail(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CompleteRegistration method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallCompleteRegistrationWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.CompleteRegistration(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the Register method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallRegisterAsync()
    {
        // Arrange
        var account = Substitute.For<Account>();

        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());
        _accountManager.Accounts.Returns(Substitute.For<IStoreRepository<IAccountStore, Account>>());

        // Act
        var result = await this._testClass.Register(account);

        // Assert
        await _servicer.Received().Serve<IEmailSender>(Arg.Any<Func<IEmailSender, Task>>());
        await _accountManager.Received().GetByEmail(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Register method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallRegisterWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Register(default(Account)));
    }

    /// <summary>
    /// Checks that the Unregister method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallUnregisterAsync()
    {
        // Arrange
        var account = Substitute.For<Account>();

        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());
        _accountManager.Accounts.Returns(Substitute.For<IStoreRepository<IAccountStore, Account>>());

        // Act
        var result = await this._testClass.Unregister(account);

        // Assert
        await _accountManager.Received().GetByEmail(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Unregister method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallUnregisterWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Unregister(default(Account)));
    }

    /// <summary>
    /// Checks that the Registered method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallRegisteredAsync()
    {
        // Arrange
        var account = Substitute.For<Account>();

        _accountManager.GetByEmail(Arg.Any<string>()).Returns(new Account());
        _accountManager.Accounts.Returns(Substitute.For<IStoreRepository<IAccountStore, Account>>());

        // Act
        var result = await this._testClass.Registered(account);

        // Assert
        await _accountManager.Received().GetByEmail(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Registered method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallRegisteredWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Registered(default(Account)));
    }

    /// <summary>
    /// Checks that the GenerateRandomPassword method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGenerateRandomPassword()
    {
        // Act
        var result = AccountService<Account>.GenerateRandomPassword();

    }
}