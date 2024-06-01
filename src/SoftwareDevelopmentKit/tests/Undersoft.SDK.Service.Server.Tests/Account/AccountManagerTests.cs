using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
/// Unit tests for the type <see cref="AccountManager"/>.
/// </summary>
[TestClass]
public class AccountManagerTests
{
    private AccountManager _testClass;
    private UserManager<AccountUser> _user;
    private RoleManager<Role> _role;
    private SignInManager<AccountUser> _signIn;
    private AccountTokenGenerator _token;
    private IStoreRepository<IAccountStore, Account> _accounts;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountManager"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._user = new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>());
        this._role = new RoleManager<Role>(Substitute.For<IRoleStore<Role>>(), new[] { Substitute.For<IRoleValidator<Role>>(), Substitute.For<IRoleValidator<Role>>(), Substitute.For<IRoleValidator<Role>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<ILogger<RoleManager<Role>>>());
        this._signIn = new SignInManager<AccountUser>(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()), Substitute.For<IHttpContextAccessor>(), Substitute.For<IUserClaimsPrincipalFactory<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<ILogger<SignInManager<AccountUser>>>(), Substitute.For<IAuthenticationSchemeProvider>(), Substitute.For<IUserConfirmation<AccountUser>>());
        this._token = new AccountTokenGenerator(x => { });
        this._accounts = Substitute.For<IStoreRepository<IAccountStore, Account>>();
        this._testClass = new AccountManager(this._user, this._role, this._signIn, this._token, this._accounts);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountManager();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new AccountManager(this._user, this._role, this._signIn, this._token, this._accounts);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the user parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullUser()
    {
        Should.Throw<ArgumentNullException>(() => new AccountManager(default(UserManager<AccountUser>), this._role, this._signIn, this._token, this._accounts));
    }

    /// <summary>
    /// Checks that instance construction throws when the role parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullRole()
    {
        Should.Throw<ArgumentNullException>(() => new AccountManager(this._user, default(RoleManager<Role>), this._signIn, this._token, this._accounts));
    }

    /// <summary>
    /// Checks that instance construction throws when the signIn parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullSignIn()
    {
        Should.Throw<ArgumentNullException>(() => new AccountManager(this._user, this._role, default(SignInManager<AccountUser>), this._token, this._accounts));
    }

    /// <summary>
    /// Checks that instance construction throws when the token parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullToken()
    {
        Should.Throw<ArgumentNullException>(() => new AccountManager(this._user, this._role, this._signIn, default(AccountTokenGenerator), this._accounts));
    }

    /// <summary>
    /// Checks that instance construction throws when the accounts parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullAccounts()
    {
        Should.Throw<ArgumentNullException>(() => new AccountManager(this._user, this._role, this._signIn, this._token, default(IStoreRepository<IAccountStore, Account>)));
    }

    /// <summary>
    /// Checks that the GetToken method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetTokenWithStringAndStringAsync()
    {
        // Arrange
        var email = "TestValue1855348177";
        var password = "TestValue957888999";

        // Act
        var result = await this._testClass.GetToken(email, password);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetToken method throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetTokenWithStringAndStringWithInvalidEmailAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetToken(value, "TestValue1742705629"));
    }

    /// <summary>
    /// Checks that the GetToken method throws when the password parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetTokenWithStringAndStringWithInvalidPasswordAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetToken("TestValue1125178927", value));
    }

    /// <summary>
    /// Checks that the GetToken method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetTokenWithAuthAsync()
    {
        // Arrange
        var auth = Substitute.For<IAuthorization>();

        // Act
        var result = await this._testClass.GetToken(auth);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetToken method throws when the auth parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallGetTokenWithAuthWithNullAuthAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetToken(default(IAuthorization)));
    }

    /// <summary>
    /// Checks that the CheckToken method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallCheckTokenAsync()
    {
        // Arrange
        var token = "TestValue1567312494";

        // Act
        var result = await this._testClass.CheckToken(token);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CheckToken method throws when the token parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallCheckTokenWithInvalidTokenAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.CheckToken(value));
    }

    /// <summary>
    /// Checks that the RenewToken method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallRenewTokenAsync()
    {
        // Arrange
        var token = "TestValue1156973987";

        // Act
        var result = await this._testClass.RenewToken(token);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the RenewToken method throws when the token parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallRenewTokenWithInvalidTokenAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.RenewToken(value));
    }

    /// <summary>
    /// Checks that the CheckPassword method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallCheckPasswordAsync()
    {
        // Arrange
        var email = "TestValue1743632608";
        var password = "TestValue1463942473";

        // Act
        var result = await this._testClass.CheckPassword(email, password);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CheckPassword method throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallCheckPasswordWithInvalidEmailAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.CheckPassword(value, "TestValue157442509"));
    }

    /// <summary>
    /// Checks that the CheckPassword method throws when the password parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallCheckPasswordWithInvalidPasswordAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.CheckPassword("TestValue733112403", value));
    }

    /// <summary>
    /// Checks that the SetUser method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSetUserAsync()
    {
        // Arrange
        var username = "TestValue1348707863";
        var email = "TestValue1137590468";
        var password = "TestValue139976666";
        var roles = new[] { "TestValue1095309927", "TestValue67298885", "TestValue245467601" };
        var scopes = new[] { "TestValue1431531967", "TestValue1412283157", "TestValue2064970854" };

        // Act
        var result = await this._testClass.SetUser(username, email, password, roles, scopes);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SetUser method throws when the roles parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSetUserWithNullRolesAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetUser("TestValue942284628", "TestValue1794496439", "TestValue1526750843", default(IEnumerable<string>), new[] { "TestValue97674350", "TestValue1821243157", "TestValue1089662377" }));
    }

    /// <summary>
    /// Checks that the SetUser method throws when the username parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSetUserWithInvalidUsernameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetUser(value, "TestValue1270328518", "TestValue1671883599", new[] { "TestValue538134894", "TestValue61621691", "TestValue295635818" }, new[] { "TestValue1153180736", "TestValue361099610", "TestValue290974644" }));
    }

    /// <summary>
    /// Checks that the SetUser method throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSetUserWithInvalidEmailAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetUser("TestValue1144449533", value, "TestValue473415880", new[] { "TestValue747758692", "TestValue1076680931", "TestValue474606970" }, new[] { "TestValue834650458", "TestValue2029158325", "TestValue386253480" }));
    }

    /// <summary>
    /// Checks that the SetUser method throws when the password parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSetUserWithInvalidPasswordAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetUser("TestValue1633370443", "TestValue1358809881", value, new[] { "TestValue461823692", "TestValue691162262", "TestValue652851651" }, new[] { "TestValue1302598215", "TestValue1790150020", "TestValue663530320" }));
    }

    /// <summary>
    /// Checks that the SetUser maps values from the input to the returned instance.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task SetUserPerformsMappingAsync()
    {
        // Arrange
        var username = "TestValue1904455246";
        var email = "TestValue104650393";
        var password = "TestValue1430050278";
        var roles = new[] { "TestValue870561849", "TestValue1599135593", "TestValue848935383" };
        var scopes = new[] { "TestValue328597334", "TestValue860214649", "TestValue2068946119" };

        // Act
        var result = await this._testClass.SetUser(username, email, password, roles, scopes);

        // Assert
        result.Roles.ShouldBeSameAs(roles);
    }

    /// <summary>
    /// Checks that the Delete method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallDeleteAsync()
    {
        // Arrange
        var email = "TestValue35516550";

        // Act
        var result = await this._testClass.Delete(email);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Delete method throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallDeleteWithInvalidEmailAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Delete(value));
    }

    /// <summary>
    /// Checks that the SetUserRole method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSetUserRole()
    {
        // Arrange
        var email = "TestValue296983976";
        var current = "TestValue1632574035";
        var previous = "TestValue1205497714";

        // Act
        var result = this._testClass.SetUserRole(email, current, previous);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SetUserRole method throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallSetUserRoleWithInvalidEmail(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SetUserRole(value, "TestValue1402320782", "TestValue2009290338"));
    }

    /// <summary>
    /// Checks that the SetUserRole method throws when the current parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallSetUserRoleWithInvalidCurrent(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SetUserRole("TestValue1727415314", value, "TestValue987608253"));
    }

    /// <summary>
    /// Checks that the SetUserRole method throws when the previous parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallSetUserRoleWithInvalidPrevious(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SetUserRole("TestValue846615824", "TestValue1143010780", value));
    }

    /// <summary>
    /// Checks that the SetUserClaim method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSetUserClaimAsync()
    {
        // Arrange
        var email = "TestValue621894047";
        var claim = new Claim(new BinaryReader(new MemoryStream()));

        // Act
        var result = await this._testClass.SetUserClaim(email, claim);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SetUserClaim method throws when the claim parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSetUserClaimWithNullClaimAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetUserClaim("TestValue1467023840", default(Claim)));
    }

    /// <summary>
    /// Checks that the SetUserClaim method throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSetUserClaimWithInvalidEmailAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetUserClaim(value, new Claim(new BinaryReader(new MemoryStream()))));
    }

    /// <summary>
    /// Checks that the SetRole method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSetRoleAsync()
    {
        // Arrange
        var roleName = "TestValue1316270317";

        // Act
        var result = await this._testClass.SetRole(roleName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SetRole method throws when the roleName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSetRoleWithInvalidRoleNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetRole(value));
    }

    /// <summary>
    /// Checks that the SetRoleClaim method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSetRoleClaimAsync()
    {
        // Arrange
        var roleName = "TestValue956924997";
        var claim = new Claim(new BinaryReader(new MemoryStream()));

        // Act
        var result = await this._testClass.SetRoleClaim(roleName, claim);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SetRoleClaim method throws when the claim parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSetRoleClaimWithNullClaimAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetRoleClaim("TestValue577632699", default(Claim)));
    }

    /// <summary>
    /// Checks that the SetRoleClaim method throws when the roleName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSetRoleClaimWithInvalidRoleNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SetRoleClaim(value, new Claim(new BinaryReader(new MemoryStream()))));
    }

    /// <summary>
    /// Checks that the TryGetByEmail method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallTryGetByEmail()
    {
        // Arrange
        var email = "TestValue35812529";

        // Act
        var result = this._testClass.TryGetByEmail(email, out var account);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the TryGetByEmail method throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallTryGetByEmailWithInvalidEmail(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.TryGetByEmail(value, out _));
    }

    /// <summary>
    /// Checks that the TryGetById method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallTryGetById()
    {
        // Arrange
        var id = 556031148L;

        // Act
        var result = this._testClass.TryGetById(id, out var account);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the TryGetByName method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallTryGetByName()
    {
        // Arrange
        var name = "TestValue161125996";

        // Act
        var result = this._testClass.TryGetByName(name, out var account);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the TryGetByName method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallTryGetByNameWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.TryGetByName(value, out _));
    }

    /// <summary>
    /// Checks that the GetByName method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetByNameAsync()
    {
        // Arrange
        var name = "TestValue167940962";

        // Act
        var result = await this._testClass.GetByName(name);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetByName method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetByNameWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetByName(value));
    }

    /// <summary>
    /// Checks that the GetByEmail method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetByEmailAsync()
    {
        // Arrange
        var email = "TestValue1783334305";

        // Act
        var result = await this._testClass.GetByEmail(email);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetByEmail method throws when the email parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetByEmailWithInvalidEmailAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetByEmail(value));
    }

    /// <summary>
    /// Checks that the GetById method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetByIdAsync()
    {
        // Arrange
        var id = 1130080895L;

        // Act
        var result = await this._testClass.GetById(id);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MapAccount method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallMapAccountAsync()
    {
        // Arrange
        var account = new Account();

        // Act
        var result = await this._testClass.MapAccount(account);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the MapAccount method throws when the account parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallMapAccountWithNullAccountAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.MapAccount(default(Account)));
    }

    /// <summary>
    /// Checks that the Accounts property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void AccountsIsInitializedCorrectly()
    {
        this._testClass.Accounts.ShouldBeSameAs(this._accounts);
    }

    /// <summary>
    /// Checks that the Accounts property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccounts()
    {
        // Arrange
        var testValue = Substitute.For<IStoreRepository<IAccountStore, Account>>();

        // Act
        this._testClass.Accounts = testValue;

        // Assert
        this._testClass.Accounts.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the User property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void UserIsInitializedCorrectly()
    {
        this._testClass.User.ShouldBeSameAs(this._user);
    }

    /// <summary>
    /// Checks that the User property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetUser()
    {
        // Arrange
        var testValue = new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>());

        // Act
        this._testClass.User = testValue;

        // Assert
        this._testClass.User.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Role property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void RoleIsInitializedCorrectly()
    {
        this._testClass.Role.ShouldBeSameAs(this._role);
    }

    /// <summary>
    /// Checks that the Role property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRole()
    {
        // Arrange
        var testValue = new RoleManager<Role>(Substitute.For<IRoleStore<Role>>(), new[] { Substitute.For<IRoleValidator<Role>>(), Substitute.For<IRoleValidator<Role>>(), Substitute.For<IRoleValidator<Role>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<ILogger<RoleManager<Role>>>());

        // Act
        this._testClass.Role = testValue;

        // Assert
        this._testClass.Role.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the SignIn property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void SignInIsInitializedCorrectly()
    {
        this._testClass.SignIn.ShouldBeSameAs(this._signIn);
    }

    /// <summary>
    /// Checks that the SignIn property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSignIn()
    {
        // Arrange
        var testValue = new SignInManager<AccountUser>(new UserManager<AccountUser>(Substitute.For<IUserStore<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<AccountUser>>(), new[] { Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>(), Substitute.For<IUserValidator<AccountUser>>() }, new[] { Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>(), Substitute.For<IPasswordValidator<AccountUser>>() }, Substitute.For<ILookupNormalizer>(), new IdentityErrorDescriber(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<AccountUser>>>()), Substitute.For<IHttpContextAccessor>(), Substitute.For<IUserClaimsPrincipalFactory<AccountUser>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<ILogger<SignInManager<AccountUser>>>(), Substitute.For<IAuthenticationSchemeProvider>(), Substitute.For<IUserConfirmation<AccountUser>>());

        // Act
        this._testClass.SignIn = testValue;

        // Assert
        this._testClass.SignIn.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Token property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void TokenIsInitializedCorrectly()
    {
        this._testClass.Token.ShouldBeSameAs(this._token);
    }

    /// <summary>
    /// Checks that the Token property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetToken()
    {
        // Arrange
        var testValue = new AccountTokenGenerator(x => { });

        // Act
        this._testClass.Token = testValue;

        // Assert
        this._testClass.Token.ShouldBeSameAs(testValue);
    }
}