using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service;
using T = System.String;

namespace Undersoft.SDK.Service.Tests;

/// <summary>
/// Unit tests for the type <see cref="ServiceObject"/>.
/// </summary>
[TestClass]
public class ServiceObject_1Tests
{
    private ServiceObject<T> _testClass;
    private T _obj;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceObject"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._obj = "TestValue1281208333";
        this._testClass = new ServiceObject<T>(this._obj);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceObject<T>();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceObject<T>(this._obj);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the obj parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullObj()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceObject<T>(default(T)));
    }

    /// <summary>
    /// Checks that the Value property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetValue()
    {
        // Arrange
        var testValue = "TestValue1529629684";

        // Act
        this._testClass.Value = testValue;

        // Assert
        this._testClass.Value.ShouldBeSameAs(testValue);
    }
}

/// <summary>
/// Unit tests for the type <see cref="ServiceObject"/>.
/// </summary>
[TestClass]
public class ServiceObjectTests
{
    private ServiceObject _testClass;
    private object _obj;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceObject"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._obj = new object();
        this._testClass = new ServiceObject(this._obj);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceObject();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceObject(this._obj);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the obj parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullObj()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceObject(default(object)));
    }

    /// <summary>
    /// Checks that the Value property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetValue()
    {
        // Arrange
        var testValue = new object();

        // Act
        this._testClass.Value = testValue;

        // Assert
        this._testClass.Value.ShouldBeSameAs(testValue);
    }
}