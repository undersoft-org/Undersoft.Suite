using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServiceHost"/>.
/// </summary>
[TestClass]
public class ServiceHostTests
{
    private ServiceHost _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceHost"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ServiceHost();
    }

    /// <summary>
    /// Checks that the Dispose method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallDispose()
    {
        // Act
        this._testClass.Dispose();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the StartAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallStartAsync()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        // Act
        await this._testClass.StartAsync(cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the StopAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallStopAsync()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        // Act
        await this._testClass.StopAsync(cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue1870097144";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
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
        var testValue = "TestValue104444608";

        // Act
        this._testClass.HostName = testValue;

        // Assert
        this._testClass.HostName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Address property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAddress()
    {
        // Arrange
        var testValue = "TestValue512023292";

        // Act
        this._testClass.Address = testValue;

        // Assert
        this._testClass.Address.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Port property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPort()
    {
        // Arrange
        var testValue = 356653742;

        // Act
        this._testClass.Port = testValue;

        // Assert
        this._testClass.Port.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Certificate property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCertificate()
    {
        // Arrange
        var testValue = new SslCertificate
        {
            Port = 2127264761,
            Protocols = "TestValue1583095540",
            Path = "TestValue993849136",
            KeyPath = "TestValue343320333",
            Password = "TestValue1723017390"
        };

        // Act
        this._testClass.Certificate = testValue;

        // Assert
        this._testClass.Certificate.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Route property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRoute()
    {
        // Arrange
        var testValue = "TestValue191588795";

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
        var testValue = 1183214053L;

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
        var testValue = "TestValue1081914565";

        // Act
        this._testClass.TenantName = testValue;

        // Assert
        this._testClass.TenantName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Services property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetServices()
    {
        // Assert
        this._testClass.Services.ShouldBeOfType<IServiceProvider>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Servicer property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServicer()
    {
        // Arrange
        var testValue = Substitute.For<IServicer>();

        // Act
        this._testClass.Servicer = testValue;

        // Assert
        this._testClass.Servicer.ShouldBeSameAs(testValue);
    }
}