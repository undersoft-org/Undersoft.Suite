using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobNormalizeNaming"/>.
/// </summary>
[TestClass]
public class BlobNormalizeNamingTests
{
    private BlobNormalizeNaming _testClass;
    private string _containerName;
    private string _blobName;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobNormalizeNaming"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._containerName = "TestValue368760742";
        this._blobName = "TestValue676200592";
        this._testClass = new BlobNormalizeNaming(this._containerName, this._blobName);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobNormalizeNaming(this._containerName, this._blobName);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the constructor throws when the containerName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidContainerName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new BlobNormalizeNaming(value, this._blobName));
    }

    /// <summary>
    /// Checks that the constructor throws when the blobName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidBlobName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new BlobNormalizeNaming(this._containerName, value));
    }

    /// <summary>
    /// Checks that the ContainerName property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ContainerNameIsInitializedCorrectly()
    {
        this._testClass.ContainerName.ShouldBe(this._containerName);
    }

    /// <summary>
    /// Checks that the BlobName property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void BlobNameIsInitializedCorrectly()
    {
        this._testClass.BlobName.ShouldBe(this._blobName);
    }
}