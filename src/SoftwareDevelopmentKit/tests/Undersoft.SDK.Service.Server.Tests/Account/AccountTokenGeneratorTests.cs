using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountTokenGenerator"/>.
/// </summary>
[TestClass]
public class AccountTokenGeneratorTests
{
    private AccountTokenGenerator _testClass;
    private Action<AccountTokenOptions> _builder;
    private int _minutesToExpire;
    private AccountTokenOptions _jwtOptions;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountTokenGenerator"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._builder = x => { };
        this._minutesToExpire = 521110805;
        this._jwtOptions = new AccountTokenOptions
        {
            SecurityKey = new byte[] { 105, 77, 164, 92 },
            Issuer = "TestValue986088330",
            Audience = "TestValue693798404",
            MinutesToExpire = 211074873
        };
        this._testClass = new AccountTokenGenerator(this._minutesToExpire, this._jwtOptions);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountTokenGenerator(this._builder);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new AccountTokenGenerator(this._minutesToExpire, this._jwtOptions);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the Generate method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGenerateWithClaims()
    {
        // Arrange
        var claims = new[] { new Claim(new BinaryReader(new MemoryStream())), new Claim(new BinaryReader(new MemoryStream())), new Claim(new BinaryReader(new MemoryStream())) };

        // Act
        var result = this._testClass.Generate(claims);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Generate method throws when the claims parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGenerateWithClaimsWithNullClaims()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Generate(default(IEnumerable<Claim>)));
    }

    /// <summary>
    /// Checks that the Generate method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGenerateWithDictionaryOfStringAndString()
    {
        // Arrange
        var claimsIdentity = new Dictionary<string, string>();

        // Act
        var result = this._testClass.Generate(claimsIdentity);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Generate method throws when the claimsIdentity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGenerateWithDictionaryOfStringAndStringWithNullClaimsIdentity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Generate(default(Dictionary<string, string>)));
    }

    /// <summary>
    /// Checks that the Generate method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGenerateWithClaimsIdentity()
    {
        // Arrange
        var claimsIdentity = new ClaimsIdentity();

        // Act
        var result = this._testClass.Generate(claimsIdentity);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Generate method throws when the claimsIdentity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGenerateWithClaimsIdentityWithNullClaimsIdentity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Generate(default(ClaimsIdentity)));
    }

    /// <summary>
    /// Checks that the Validate method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallValidateAsync()
    {
        // Arrange
        var token = "TestValue1239791867";

        // Act
        var result = await this._testClass.Validate(token);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Validate method throws when the token parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallValidateWithInvalidTokenAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Validate(value));
    }
}