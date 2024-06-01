using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Tests.Access;

/// <summary>
/// Unit tests for the type <see cref="SecurityString"/>.
/// </summary>
[TestClass]
public class SecurityStringTests
{
    private SecurityString _testClass;
    private string _value;
    private string _prefix;
    private string _type;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="SecurityString"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._value = "TestValue963358442";
        this._prefix = "TestValue327299100";
        this._type = "TestValue1752976849";
        this._testClass = new SecurityString(this._value, this._prefix, this._type);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new SecurityString();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new SecurityString(this._value, this._prefix, this._type);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the constructor throws when the value parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidValue(string value)
    {
        Should.Throw<ArgumentNullException>(() => new SecurityString(value, this._prefix, this._type));
    }

    /// <summary>
    /// Checks that the constructor throws when the prefix parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidPrefix(string value)
    {
        Should.Throw<ArgumentNullException>(() => new SecurityString(this._value, value, this._type));
    }

    /// <summary>
    /// Checks that the constructor throws when the type parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidType(string value)
    {
        Should.Throw<ArgumentNullException>(() => new SecurityString(this._value, this._prefix, value));
    }

    /// <summary>
    /// Checks that the Decoded property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDecoded()
    {
        // Arrange
        var testValue = "TestValue1575780136";

        // Act
        this._testClass.Decoded = testValue;

        // Assert
        this._testClass.Decoded.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Encoded property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEncoded()
    {
        // Arrange
        var testValue = "TestValue1379104836";

        // Act
        this._testClass.Encoded = testValue;

        // Assert
        this._testClass.Encoded.ShouldBe(testValue);
    }
}