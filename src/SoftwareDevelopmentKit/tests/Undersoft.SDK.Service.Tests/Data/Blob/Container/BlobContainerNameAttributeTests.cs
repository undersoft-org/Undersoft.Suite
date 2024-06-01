using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob.Container;
using T = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Blob.Container;

/// <summary>
/// Unit tests for the type <see cref="BlobContainerNameAttribute"/>.
/// </summary>
[TestClass]
public class BlobContainerNameAttributeTests
{
    private BlobContainerNameAttribute _testClass;
    private string _name;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobContainerNameAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._name = "TestValue496104619";
        this._testClass = new BlobContainerNameAttribute(this._name);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobContainerNameAttribute(this._name);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the constructor throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainerNameAttribute(value));
    }

    /// <summary>
    /// Checks that the GetName method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetName()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = this._testClass.GetName(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetName method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetNameWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.GetName(default(Type)));
    }

    /// <summary>
    /// Checks that the GetContainerName method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetContainerNameWithNoParameters()
    {
        // Act
        var result = BlobContainerNameAttribute.GetContainerName<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetContainerName method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetContainerNameWithType()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = BlobContainerNameAttribute.GetContainerName(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetContainerName method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetContainerNameWithTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => BlobContainerNameAttribute.GetContainerName(default(Type)));
    }

    /// <summary>
    /// Checks that the Name property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void NameIsInitializedCorrectly()
    {
        this._testClass.Name.ShouldBe(this._name);
    }
}