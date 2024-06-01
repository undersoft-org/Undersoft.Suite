using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;
using T = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Blob.Container;

/// <summary>
/// Unit tests for the type <see cref="BlobContainerConfiguration"/>.
/// </summary>
[TestClass]
public class BlobContainerConfigurationTests
{
    private BlobContainerConfiguration _testClass;
    private BlobContainerConfiguration _fallbackConfiguration;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobContainerConfiguration"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._fallbackConfiguration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        this._testClass = new BlobContainerConfiguration(this._fallbackConfiguration);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobContainerConfiguration(this._fallbackConfiguration);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the GetConfigurationOrDefault method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetConfigurationOrDefault()
    {
        // Arrange
        var name = "TestValue1089400545";
        var defaultValue = "TestValue1980347911";

        // Act
        var result = this._testClass.GetConfigurationOrDefault<T>(name, defaultValue);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetConfigurationOrDefault method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetConfigurationOrDefaultWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetConfigurationOrDefault<T>(value, "TestValue594893911"));
    }

    /// <summary>
    /// Checks that the GetConfigurationOrNull method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetConfigurationOrNull()
    {
        // Arrange
        var name = "TestValue413507039";
        var defaultValue = new object();

        // Act
        var result = this._testClass.GetConfigurationOrNull(name, defaultValue);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetConfigurationOrNull method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetConfigurationOrNullWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetConfigurationOrNull(value, new object()));
    }

    /// <summary>
    /// Checks that the SetConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallSetConfiguration()
    {
        // Arrange
        var name = "TestValue603959251";
        var value = new object();

        // Act
        var result = this._testClass.SetConfiguration(name, value);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SetConfiguration method throws when the value parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallSetConfigurationWithNullValue()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SetConfiguration("TestValue348472007", default(object)));
    }

    /// <summary>
    /// Checks that the SetConfiguration method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallSetConfigurationWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.SetConfiguration(value, new object()));
    }

    /// <summary>
    /// Checks that the ClearConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallClearConfiguration()
    {
        // Arrange
        var name = "TestValue1171706957";

        // Act
        var result = this._testClass.ClearConfiguration(name);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ClearConfiguration method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallClearConfigurationWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ClearConfiguration(value));
    }

    /// <summary>
    /// Checks that the ProviderType property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProviderType()
    {
        // Arrange
        var testValue = typeof(string);

        // Act
        this._testClass.ProviderType = testValue;

        // Assert
        this._testClass.ProviderType.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the NamingNormalizers property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetNamingNormalizers()
    {
        // Assert
        this._testClass.NamingNormalizers.ShouldBeOfType<IList<IBlobNamingNormalizer>>();

        Assert.Fail("Create or modify test");
    }
}