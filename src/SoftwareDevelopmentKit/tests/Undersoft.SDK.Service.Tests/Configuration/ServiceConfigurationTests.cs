using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Configuration;
using Undersoft.SDK.Service.Data.Repository;
using TOptions = System.String;

namespace Undersoft.SDK.Service.Tests.Configuration;

/// <summary>
/// Unit tests for the type <see cref="ServiceConfiguration"/>.
/// </summary>
[TestClass]
public class ServiceConfigurationTests
{
    private ServiceConfiguration _testClass;
    private IConfiguration _config;
    private IServiceCollection _services;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceConfiguration"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._config = Substitute.For<IConfiguration>();
        this._services = Substitute.For<IServiceCollection>();
        this._testClass = new ServiceConfiguration(this._config, this._services);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceConfiguration();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceConfiguration(this._config);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceConfiguration(this._services);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceConfiguration(this._config, this._services);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the config parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfig()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceConfiguration(default(IConfiguration)));
        Should.Throw<ArgumentNullException>(() => new ServiceConfiguration(default(IConfiguration), this._services));
    }

    /// <summary>
    /// Checks that instance construction throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceConfiguration(default(IServiceCollection)));
        Should.Throw<ArgumentNullException>(() => new ServiceConfiguration(this._config, default(IServiceCollection)));
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigureWithSectionName()
    {
        // Arrange
        var sectionName = "TestValue1959965429";

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.Configure<TOptions>(sectionName);

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the sectionName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallConfigureWithSectionNameWithInvalidSectionName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure<TOptions>(value));
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigureWithSectionNameAndConfigureOptions()
    {
        // Arrange
        var sectionName = "TestValue1060425958";
        Action<BinderOptions> configureOptions = x => { };

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.Configure<TOptions>(sectionName, configureOptions
        );

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the configureOptions parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithSectionNameAndConfigureOptionsWithNullConfigureOptions()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure<TOptions>("TestValue2096226892", default(Action<BinderOptions>)));
    }

    /// <summary>
    /// Checks that the Configure method throws when the sectionName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallConfigureWithSectionNameAndConfigureOptionsWithInvalidSectionName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure<TOptions>(value, x => { }));
    }

    /// <summary>
    /// Checks that the Configure method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigureWithConfigureOptions()
    {
        // Arrange
        Action<TOptions> configureOptions = x => { };

        // Act
        var result = this._testClass.Configure<TOptions>(configureOptions);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Configure method throws when the configureOptions parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureWithConfigureOptionsWithNullConfigureOptions()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Configure<TOptions>(default(Action<TOptions>)));
    }

    /// <summary>
    /// Checks that the StoreRoutes method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallStoreRoutes()
    {
        // Arrange
        var name = "TestValue2003379633";

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.StoreRoutes(name);

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the StoreRoutes method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallStoreRoutesWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.StoreRoutes(value));
    }

    /// <summary>
    /// Checks that the Repository method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRepository()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.Repository();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the DataCacheLifeTime method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallDataCacheLifeTime()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.DataCacheLifeTime();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Sources method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSources()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.Sources();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Clients method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallClients()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.Clients();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Source method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSource()
    {
        // Arrange
        var name = "TestValue1579207935";

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.Source(name);

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Source method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallSourceWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Source(value));
    }

    /// <summary>
    /// Checks that the SourceConnectionString method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSourceConnectionStringWithString()
    {
        // Arrange
        var name = "TestValue699544317";

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.SourceConnectionString(name);

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SourceConnectionString method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallSourceConnectionStringWithStringWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SourceConnectionString(value));
    }

    /// <summary>
    /// Checks that the ClientConnectionString method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallClientConnectionStringWithString()
    {
        // Arrange
        var name = "TestValue1670773946";

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.ClientConnectionString(name);

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ClientConnectionString method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallClientConnectionStringWithStringWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ClientConnectionString(value));
    }

    /// <summary>
    /// Checks that the SourceConnectionString method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSourceConnectionStringWithIConfigurationSection()
    {
        // Arrange
        var endpoint = Substitute.For<IConfigurationSection>();

        // Act
        var result = this._testClass.SourceConnectionString(endpoint);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SourceConnectionString method throws when the endpoint parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSourceConnectionStringWithIConfigurationSectionWithNullEndpoint()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SourceConnectionString(default(IConfigurationSection)));
    }

    /// <summary>
    /// Checks that the ClientConnectionString method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallClientConnectionStringWithIConfigurationSection()
    {
        // Arrange
        var client = Substitute.For<IConfigurationSection>();

        // Act
        var result = this._testClass.ClientConnectionString(client);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ClientConnectionString method throws when the client parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallClientConnectionStringWithIConfigurationSectionWithNullClient()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ClientConnectionString(default(IConfigurationSection)));
    }

    /// <summary>
    /// Checks that the Client method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallClient()
    {
        // Arrange
        var name = "TestValue2082883904";

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.Client(name);

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Client method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallClientWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Client(value));
    }

    /// <summary>
    /// Checks that the SourceProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSourceProviderWithString()
    {
        // Arrange
        var name = "TestValue1515848272";

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.SourceProvider(name);

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SourceProvider method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallSourceProviderWithStringWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SourceProvider(value));
    }

    /// <summary>
    /// Checks that the ClientProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallClientProviderWithString()
    {
        // Arrange
        var name = "TestValue8698069";

        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.ClientProvider(name);

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ClientProvider method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallClientProviderWithStringWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ClientProvider(value));
    }

    /// <summary>
    /// Checks that the SourceProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSourceProviderWithSource()
    {
        // Arrange
        var source = Substitute.For<IConfigurationSection>();

        // Act
        var result = this._testClass.SourceProvider(source);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SourceProvider method throws when the source parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSourceProviderWithSourceWithNullSource()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SourceProvider(default(IConfigurationSection)));
    }

    /// <summary>
    /// Checks that the ClientProvider method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallClientProviderWithIConfigurationSection()
    {
        // Arrange
        var client = Substitute.For<IConfigurationSection>();

        // Act
        var result = this._testClass.ClientProvider(client);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ClientProvider method throws when the client parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallClientProviderWithIConfigurationSectionWithNullClient()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ClientProvider(default(IConfigurationSection)));
    }

    /// <summary>
    /// Checks that the SourcePoolSize method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSourcePoolSize()
    {
        // Arrange
        var endpoint = Substitute.For<IConfigurationSection>();

        // Act
        var result = this._testClass.SourcePoolSize(endpoint);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SourcePoolSize method throws when the endpoint parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSourcePoolSizeWithNullEndpoint()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SourcePoolSize(default(IConfigurationSection)));
    }

    /// <summary>
    /// Checks that the ClientPoolSize method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallClientPoolSize()
    {
        // Arrange
        var client = Substitute.For<IConfigurationSection>();

        // Act
        var result = this._testClass.ClientPoolSize(client);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ClientPoolSize method throws when the client parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallClientPoolSizeWithNullClient()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ClientPoolSize(default(IConfigurationSection)));
    }

    /// <summary>
    /// Checks that the GetChildren method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetChildren()
    {
        // Arrange
        var expectedReturnValue = new[] { Substitute.For<IConfigurationSection>(), Substitute.For<IConfigurationSection>(), Substitute.For<IConfigurationSection>() };
        _config.GetChildren().Returns(expectedReturnValue);

        // Act
        var result = this._testClass.GetChildren();

        // Assert
        _config.Received().GetChildren();
        result.ShouldBeSameAs(expectedReturnValue);
    }

    /// <summary>
    /// Checks that the GetReloadToken method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetReloadToken()
    {
        // Arrange
        var expectedReturnValue = Substitute.For<IChangeToken>();
        _config.GetReloadToken().Returns(expectedReturnValue);

        // Act
        var result = this._testClass.GetReloadToken();

        // Assert
        _config.Received().GetReloadToken();
        result.ShouldBeSameAs(expectedReturnValue);
    }

    /// <summary>
    /// Checks that the GetSection method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetSection()
    {
        // Arrange
        var key = "TestValue2129646441";

        var expectedReturnValue = Substitute.For<IConfigurationSection>();
        _config.GetSection(key).Returns(expectedReturnValue);

        // Act
        var result = this._testClass.GetSection(key);

        // Assert
        _config.Received().GetSection(key);
        result.ShouldBeSameAs(expectedReturnValue);
    }

    /// <summary>
    /// Checks that the GetSection method throws when the key parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetSectionWithInvalidKey(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetSection(value));
    }

    /// <summary>
    /// Checks that the GetSection maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void GetSectionPerformsMapping()
    {
        // Arrange
        var key = "TestValue1928325569";

        // Act
        var result = this._testClass.GetSection(key);

        // Assert
        result.Key.ShouldBeSameAs(key);
    }

    /// <summary>
    /// Checks that the AccessServer method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAccessServer()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.AccessServer();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IdentityServerBaseUrl method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallIdentityServerBaseUrl()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.IdentityServerBaseUrl();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IdentityServiceName method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallIdentityServiceName()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.IdentityServiceName();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IdentityServerScopes method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallIdentityServerScopes()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.IdentityServerScopes();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IdentityServerClaims method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallIdentityServerClaims()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.IdentityServerClaims();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IdentityServerRoles method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallIdentityServerRoles()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Act
        var result = this._testClass.IdentityServerRoles();

        // Assert
        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetAccessServerConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetAccessServerConfiguration()
    {
        // Act
        var result = this._testClass.GetAccessServerConfiguration();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetOpenApiConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetOpenApiConfiguration()
    {
        // Act
        var result = this._testClass.GetOpenApiConfiguration();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRepositoryConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRepositoryConfiguration()
    {
        // Act
        var result = this._testClass.GetRepositoryConfiguration();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AccessOptions property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetAccessOptions()
    {
        // Assert
        this._testClass.AccessOptions.ShouldBeOfType<AccessServerOptions>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Repositories property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetRepositories()
    {
        // Assert
        this._testClass.Repositories.ShouldBeOfType<RepositoryOptions>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Version property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetVersion()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Assert
        this._testClass.Version.ShouldBeOfType<string>();

        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Name property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetName()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Assert
        this._testClass.Name.ShouldBeOfType<string>();

        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the TypeName property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetTypeName()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Assert
        this._testClass.TypeName.ShouldBeOfType<string>();

        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the UserSecretsId property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetUserSecretsId()
    {
        // Arrange
        _config.GetSection(Arg.Any<string>()).Returns(Substitute.For<IConfigurationSection>());

        // Assert
        this._testClass.UserSecretsId.ShouldBeOfType<string>();

        _config.Received().GetSection(Arg.Any<string>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the indexer functions correctly.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIndexer()
    {
        var testValue = "TestValue602440788";
        this._testClass["TestValue966351416"].ShouldBeOfType<string>();
        this._testClass["TestValue966351416"] = testValue;
        this._testClass["TestValue966351416"].ShouldBe(testValue);
    }
}