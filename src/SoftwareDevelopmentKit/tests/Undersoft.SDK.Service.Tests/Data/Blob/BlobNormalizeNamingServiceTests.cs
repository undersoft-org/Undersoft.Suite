using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobNormalizeNamingService"/>.
/// </summary>
[TestClass]
public class BlobNormalizeNamingServiceTests
{
    private BlobNormalizeNamingService _testClass;
    private IServiceProvider _serviceProvider;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobNormalizeNamingService"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._serviceProvider = Substitute.For<IServiceProvider>();
        this._testClass = new BlobNormalizeNamingService(this._serviceProvider);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobNormalizeNamingService(this._serviceProvider);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the serviceProvider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServiceProvider()
    {
        Should.Throw<ArgumentNullException>(() => new BlobNormalizeNamingService(default(IServiceProvider)));
    }

    /// <summary>
    /// Checks that the NormalizeNaming method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallNormalizeNaming()
    {
        // Arrange
        var configuration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        var containerName = "TestValue1308362153";
        var blobName = "TestValue767234095";

        // Act
        var result = this._testClass.NormalizeNaming(configuration, containerName, blobName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the NormalizeNaming method throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallNormalizeNamingWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.NormalizeNaming(default(BlobContainerConfiguration), "TestValue1873735008", "TestValue520166163"));
    }

    /// <summary>
    /// Checks that the NormalizeNaming method throws when the containerName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallNormalizeNamingWithInvalidContainerName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.NormalizeNaming(new BlobContainerConfiguration(default(BlobContainerConfiguration)), value, "TestValue1795584368"));
    }

    /// <summary>
    /// Checks that the NormalizeNaming method throws when the blobName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallNormalizeNamingWithInvalidBlobName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.NormalizeNaming(new BlobContainerConfiguration(default(BlobContainerConfiguration)), "TestValue77152845", value));
    }

    /// <summary>
    /// Checks that the NormalizeNaming maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void NormalizeNamingPerformsMapping()
    {
        // Arrange
        var configuration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        var containerName = "TestValue1592849709";
        var blobName = "TestValue1918807777";

        // Act
        var result = this._testClass.NormalizeNaming(configuration, containerName, blobName);

        // Assert
        result.ContainerName.ShouldBeSameAs(containerName);
        result.BlobName.ShouldBeSameAs(blobName);
    }

    /// <summary>
    /// Checks that the NormalizeContainerName method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallNormalizeContainerName()
    {
        // Arrange
        var configuration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        var containerName = "TestValue1169675490";

        // Act
        var result = this._testClass.NormalizeContainerName(configuration, containerName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the NormalizeContainerName method throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallNormalizeContainerNameWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.NormalizeContainerName(default(BlobContainerConfiguration), "TestValue2134827385"));
    }

    /// <summary>
    /// Checks that the NormalizeContainerName method throws when the containerName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallNormalizeContainerNameWithInvalidContainerName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.NormalizeContainerName(new BlobContainerConfiguration(default(BlobContainerConfiguration)), value));
    }

    /// <summary>
    /// Checks that the NormalizeBlobName method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallNormalizeBlobName()
    {
        // Arrange
        var configuration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        var blobName = "TestValue877734742";

        // Act
        var result = this._testClass.NormalizeBlobName(configuration, blobName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the NormalizeBlobName method throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallNormalizeBlobNameWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.NormalizeBlobName(default(BlobContainerConfiguration), "TestValue43754306"));
    }

    /// <summary>
    /// Checks that the NormalizeBlobName method throws when the blobName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallNormalizeBlobNameWithInvalidBlobName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.NormalizeBlobName(new BlobContainerConfiguration(default(BlobContainerConfiguration)), value));
    }
}