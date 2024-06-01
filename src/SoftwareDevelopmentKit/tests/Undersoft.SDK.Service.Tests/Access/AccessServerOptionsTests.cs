using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Tests.Access;

/// <summary>
/// Unit tests for the type <see cref="AccessServerOptions"/>.
/// </summary>
[TestClass]
public class AccessServerOptionsTests
{
    private AccessServerOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccessServerOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccessServerOptions();
    }

    /// <summary>
    /// Checks that the ServiceName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServiceName()
    {
        // Arrange
        var testValue = "TestValue1097285072";

        // Act
        this._testClass.ServiceName = testValue;

        // Assert
        this._testClass.ServiceName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ServiceVersion property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServiceVersion()
    {
        // Arrange
        var testValue = "TestValue519999652";

        // Act
        this._testClass.ServiceVersion = testValue;

        // Assert
        this._testClass.ServiceVersion.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ServerBaseUrl property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServerBaseUrl()
    {
        // Arrange
        var testValue = "TestValue289818033";

        // Act
        this._testClass.ServerBaseUrl = testValue;

        // Assert
        this._testClass.ServerBaseUrl.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ServiceBaseUrl property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServiceBaseUrl()
    {
        // Arrange
        var testValue = "TestValue1634611238";

        // Act
        this._testClass.ServiceBaseUrl = testValue;

        // Assert
        this._testClass.ServiceBaseUrl.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ServiceClientId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServiceClientId()
    {
        // Arrange
        var testValue = "TestValue1499043938";

        // Act
        this._testClass.ServiceClientId = testValue;

        // Assert
        this._testClass.ServiceClientId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the RequireHttpsMetadata property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRequireHttpsMetadata()
    {
        // Arrange
        var testValue = true;

        // Act
        this._testClass.RequireHttpsMetadata = testValue;

        // Assert
        this._testClass.RequireHttpsMetadata.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Audience property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAudience()
    {
        // Arrange
        var testValue = "TestValue560071569";

        // Act
        this._testClass.Audience = testValue;

        // Assert
        this._testClass.Audience.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Issuer property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIssuer()
    {
        // Arrange
        var testValue = "TestValue1944873689";

        // Act
        this._testClass.Issuer = testValue;

        // Assert
        this._testClass.Issuer.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Scopes property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetScopes()
    {
        // Arrange
        var testValue = new[] { "TestValue294984560", "TestValue759568470", "TestValue1576055629" };

        // Act
        this._testClass.Scopes = testValue;

        // Assert
        this._testClass.Scopes.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Roles property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRoles()
    {
        // Arrange
        var testValue = new[] { "TestValue2124398906", "TestValue1314544461", "TestValue1373881366" };

        // Act
        this._testClass.Roles = testValue;

        // Assert
        this._testClass.Roles.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Claims property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaims()
    {
        // Arrange
        var testValue = new[] { "TestValue1793701168", "TestValue1210760164", "TestValue762981128" };

        // Act
        this._testClass.Claims = testValue;

        // Assert
        this._testClass.Claims.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the AdministrationRole property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAdministrationRole()
    {
        // Arrange
        var testValue = "TestValue1072671958";

        // Act
        this._testClass.AdministrationRole = testValue;

        // Assert
        this._testClass.AdministrationRole.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CorsAllowAnyOrigin property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCorsAllowAnyOrigin()
    {
        // Arrange
        var testValue = true;

        // Act
        this._testClass.CorsAllowAnyOrigin = testValue;

        // Assert
        this._testClass.CorsAllowAnyOrigin.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the CorsAllowOrigins property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCorsAllowOrigins()
    {
        // Arrange
        var testValue = new[] { "TestValue1371955535", "TestValue874544224", "TestValue1478985969" };

        // Act
        this._testClass.CorsAllowOrigins = testValue;

        // Assert
        this._testClass.CorsAllowOrigins.ShouldBeSameAs(testValue);
    }
}