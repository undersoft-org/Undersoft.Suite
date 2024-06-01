using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Configuration;

namespace Undersoft.SDK.Service.Tests.Configuration;

/// <summary>
/// Unit tests for the type <see cref="ServiceConfigurationExtensions"/>.
/// </summary>
[TestClass]
public class ServiceConfigurationExtensionsTests
{
    /// <summary>
    /// Checks that the BuildConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallBuildConfigurationWithConfig()
    {
        // Arrange
        var config = Substitute.For<IConfiguration>();

        // Act
        var result = config.BuildConfiguration();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the BuildConfiguration method throws when the config parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallBuildConfigurationWithConfigWithNullConfig()
    {
        Should.Throw<ArgumentNullException>(() => default(IConfiguration).BuildConfiguration());
    }

    /// <summary>
    /// Checks that the BuildConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallBuildConfigurationWithConfigAndServices()
    {
        // Arrange
        var config = Substitute.For<IConfiguration>();
        var services = Substitute.For<IServiceCollection>();

        // Act
        var result = config.BuildConfiguration(services
        );

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the BuildConfiguration method throws when the config parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallBuildConfigurationWithConfigAndServicesWithNullConfig()
    {
        Should.Throw<ArgumentNullException>(() => default(IConfiguration).BuildConfiguration(Substitute.For<IServiceCollection>()));
    }

    /// <summary>
    /// Checks that the BuildConfiguration method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallBuildConfigurationWithConfigAndServicesWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IConfiguration>().BuildConfiguration(default(IServiceCollection)));
    }

    /// <summary>
    /// Checks that the ReplaceConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallReplaceConfiguration()
    {
        // Arrange
        var services = Substitute.For<IServiceCollection>();
        var configuration = Substitute.For<IConfiguration>();

        // Act
        var result = services.ReplaceConfiguration(configuration);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ReplaceConfiguration method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallReplaceConfigurationWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceCollection).ReplaceConfiguration(Substitute.For<IConfiguration>()));
    }

    /// <summary>
    /// Checks that the ReplaceConfiguration method throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallReplaceConfigurationWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IServiceCollection>().ReplaceConfiguration(default(IConfiguration)));
    }

    /// <summary>
    /// Checks that the GetConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetConfiguration()
    {
        // Arrange
        var services = Substitute.For<IServiceRegistry>();

        // Act
        var result = services.GetConfiguration();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetConfiguration method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetConfigurationWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceRegistry).GetConfiguration());
    }

    /// <summary>
    /// Checks that the OnRegistred method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallOnRegistred()
    {
        // Arrange
        var services = Substitute.For<IServiceRegistry>();
        Action<IOnServiceRegistredContext> registrationAction = x => { };

        // Act
        services.OnRegistred(registrationAction);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the OnRegistred method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallOnRegistredWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceRegistry).OnRegistred(x => { }));
    }

    /// <summary>
    /// Checks that the OnRegistred method throws when the registrationAction parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallOnRegistredWithNullRegistrationAction()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IServiceRegistry>().OnRegistred(default(Action<IOnServiceRegistredContext>)));
    }

    /// <summary>
    /// Checks that the GetRegistrationActionList method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRegistrationActionList()
    {
        // Arrange
        var services = Substitute.For<IServiceRegistry>();

        // Act
        var result = services.GetRegistrationActionList();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRegistrationActionList method throws when the services parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetRegistrationActionListWithNullServices()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceRegistry).GetRegistrationActionList());
    }
}

/// <summary>
/// Unit tests for the type <see cref="ServiceRegistrationActionList"/>.
/// </summary>
[TestClass]
public class ServiceRegistrationActionListTests
{
    private ServiceRegistrationActionList _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceRegistrationActionList"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ServiceRegistrationActionList();
    }

    /// <summary>
    /// Checks that the IsClassInterceptorsDisabled property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIsClassInterceptorsDisabled()
    {
        // Arrange
        var testValue = true;

        // Act
        this._testClass.IsClassInterceptorsDisabled = testValue;

        // Assert
        this._testClass.IsClassInterceptorsDisabled.ShouldBe(testValue);
    }
}