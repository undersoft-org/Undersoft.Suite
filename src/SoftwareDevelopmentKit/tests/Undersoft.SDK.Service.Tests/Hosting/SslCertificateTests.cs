using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="SslCertificate"/>.
/// </summary>
[TestClass]
public class SslCertificateTests
{
    private SslCertificate _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="SslCertificate"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new SslCertificate();
    }

    /// <summary>
    /// Checks that the Port property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPort()
    {
        // Arrange
        var testValue = 767498740;

        // Act
        this._testClass.Port = testValue;

        // Assert
        this._testClass.Port.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Protocols property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProtocols()
    {
        // Arrange
        var testValue = "TestValue1418672283";

        // Act
        this._testClass.Protocols = testValue;

        // Assert
        this._testClass.Protocols.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Path property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPath()
    {
        // Arrange
        var testValue = "TestValue1046550181";

        // Act
        this._testClass.Path = testValue;

        // Assert
        this._testClass.Path.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the KeyPath property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetKeyPath()
    {
        // Arrange
        var testValue = "TestValue2103226397";

        // Act
        this._testClass.KeyPath = testValue;

        // Assert
        this._testClass.KeyPath.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Password property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPassword()
    {
        // Arrange
        var testValue = "TestValue463208696";

        // Act
        this._testClass.Password = testValue;

        // Assert
        this._testClass.Password.ShouldBe(testValue);
    }
}