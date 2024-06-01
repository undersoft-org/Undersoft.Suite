using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountMappings"/>.
/// </summary>
[TestClass]
public class AccountMappingsTests
{
    private AccountMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<Account>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<Account>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="RolemMappings"/>.
/// </summary>
[TestClass]
public class RolemMappingsTests
{
    private RolemMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="RolemMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new RolemMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<Role>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<Role>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountPersonalMappings"/>.
/// </summary>
[TestClass]
public class AccountPersonalMappingsTests
{
    private AccountPersonalMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountPersonalMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountPersonalMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<AccountPersonal>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<AccountPersonal>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountAddressMappings"/>.
/// </summary>
[TestClass]
public class AccountAddressMappingsTests
{
    private AccountAddressMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountAddressMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountAddressMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<AccountAddress>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<AccountAddress>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountTokenMappings"/>.
/// </summary>
[TestClass]
public class AccountTokenMappingsTests
{
    private AccountTokenMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountTokenMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountTokenMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<AccountToken>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<AccountToken>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountProffesionalMappings"/>.
/// </summary>
[TestClass]
public class AccountProffesionalMappingsTests
{
    private AccountProffesionalMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountProffesionalMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountProffesionalMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<AccountProfessional>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<AccountProfessional>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountOrganizationsMappings"/>.
/// </summary>
[TestClass]
public class AccountOrganizationsMappingsTests
{
    private AccountOrganizationsMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountOrganizationsMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountOrganizationsMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<AccountOrganization>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<AccountOrganization>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountSubscriptionsMappings"/>.
/// </summary>
[TestClass]
public class AccountSubscriptionsMappingsTests
{
    private AccountSubscriptionsMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountSubscriptionsMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountSubscriptionsMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<AccountSubscription>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<AccountSubscription>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountConsentsMappings"/>.
/// </summary>
[TestClass]
public class AccountConsentsMappingsTests
{
    private AccountConsentsMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountConsentsMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountConsentsMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<AccountConsent>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<AccountConsent>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountPaymentsMappings"/>.
/// </summary>
[TestClass]
public class AccountPaymentsMappingsTests
{
    private AccountPaymentsMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountPaymentsMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountPaymentsMappings();
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigure()
    {
        // Arrange
        var builder = new EntityTypeBuilder<AccountPayment>(Substitute.For<IMutableEntityType>());

        // Act
        this._testClass.Configure(builder);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the builder parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithNullBuilder()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure(default(EntityTypeBuilder<AccountPayment>)));
    }
}