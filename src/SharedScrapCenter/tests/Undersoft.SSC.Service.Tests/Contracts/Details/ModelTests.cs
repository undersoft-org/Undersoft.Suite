using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="Model"/>.
/// </summary>
[TestClass]
public class ModelTests
{
    private Model _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Model"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Model();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Model();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the Name property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        // Arrange
        var testValue = "TestValue204248979";

        // Act
        this._testClass.Name = testValue;

        // Assert
        this._testClass.Name.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Description property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDescription()
    {
        // Arrange
        var testValue = "TestValue1936137215";

        // Act
        this._testClass.Description = testValue;

        // Assert
        this._testClass.Description.ShouldBe(testValue);
    }
}