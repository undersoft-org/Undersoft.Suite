using System;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.SDK.Service.Server.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServerHostOptions"/>.
/// </summary>
[TestClass]
public class ServerHostOptionsTests
{
    private ServerHostOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServerHostOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ServerHostOptions();
    }

    /// <summary>
    /// Checks that the ServerName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServerName()
    {
        // Arrange
        var testValue = "TestValue945110085";

        // Act
        this._testClass.ServerName = testValue;

        // Assert
        this._testClass.ServerName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Host property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetHost()
    {
        // Arrange
        var testValue = Substitute.For<IHost>();

        // Act
        this._testClass.Host = testValue;

        // Assert
        this._testClass.Host.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the HostName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetHostName()
    {
        // Arrange
        var testValue = "TestValue1375609015";

        // Act
        this._testClass.HostName = testValue;

        // Assert
        this._testClass.HostName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Port property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPort()
    {
        // Arrange
        var testValue = 1974633761;

        // Act
        this._testClass.Port = testValue;

        // Assert
        this._testClass.Port.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Route property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRoute()
    {
        // Arrange
        var testValue = "TestValue1071095584";

        // Act
        this._testClass.Route = testValue;

        // Assert
        this._testClass.Route.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the TenantId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTenantId()
    {
        // Arrange
        var testValue = 1066641645L;

        // Act
        this._testClass.TenantId = testValue;

        // Assert
        this._testClass.TenantId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the TenantName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTenantName()
    {
        // Arrange
        var testValue = "TestValue878308888";

        // Act
        this._testClass.TenantName = testValue;

        // Assert
        this._testClass.TenantName.ShouldBe(testValue);
    }
}