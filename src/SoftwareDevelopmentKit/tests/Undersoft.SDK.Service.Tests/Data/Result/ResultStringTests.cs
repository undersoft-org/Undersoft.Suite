using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Response;

namespace Undersoft.SDK.Service.Tests.Data.Response;

/// <summary>
/// Unit tests for the type <see cref="ResultString"/>.
/// </summary>
[TestClass]
public class ResultStringTests
{
    private ResultString _testClass;
    private string _value;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ResultString"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._value = "TestValue1222226074";
        this._testClass = new ResultString(this._value);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ResultString();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ResultString(this._value);

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
        Should.Throw<ArgumentNullException>(() => new ResultString(value));
    }

    /// <summary>
    /// Checks that the Value property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ValueIsInitializedCorrectly()
    {
        this._testClass.Value.ShouldBe(this._value);
    }

    /// <summary>
    /// Checks that the Value property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetValue()
    {
        // Arrange
        var testValue = "TestValue649258893";

        // Act
        this._testClass.Value = testValue;

        // Assert
        this._testClass.Value.ShouldBe(testValue);
    }
}