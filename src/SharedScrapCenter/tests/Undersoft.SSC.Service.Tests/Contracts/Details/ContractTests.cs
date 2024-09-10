using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="Contract"/>.
/// </summary>
[TestClass]
public class ContractTests
{
    private Contract _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Contract"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Contract();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Contract();

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
        var testValue = "TestValue559609700";

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
        var testValue = "TestValue576822065";

        // Act
        this._testClass.Description = testValue;

        // Assert
        this._testClass.Description.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Type property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetType()
    {
        // Arrange
        var testValue = "TestValue507631533";

        // Act
        this._testClass.Type = testValue;

        // Assert
        this._testClass.Type.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the TextModel property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTextModel()
    {
        // Arrange
        var testValue = "TestValue1515622666";

        // Act
        this._testClass.TextModel = testValue;

        // Assert
        this._testClass.TextModel.ShouldBe(testValue);
    }
}