using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;
using Undersoft.SDK.Service.Server.Resource.Container;

namespace Undersoft.SDK.Service.Server.Tests.Resource.Container;

/// <summary>
/// Unit tests for the type <see cref="ResourceFileContainer"/>.
/// </summary>
[TestClass]
public class ResourceFileContainerTests
{
    private ResourceFileContainer _testClass;
    private string _containerName;
    private BlobContainerConfiguration _configuration;
    private IBlobProvider _provider;
    private IBlobNormalizeNamingService _blobNormalizeNamingService;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ResourceFileContainer"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._containerName = "TestValue2125359212";
        this._configuration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        this._provider = Substitute.For<IBlobProvider>();
        this._blobNormalizeNamingService = Substitute.For<IBlobNormalizeNamingService>();
        this._testClass = new ResourceFileContainer(this._containerName, this._configuration, this._provider, this._blobNormalizeNamingService);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ResourceFileContainer(this._containerName);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ResourceFileContainer(this._containerName, this._configuration, this._provider, this._blobNormalizeNamingService);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => new ResourceFileContainer(this._containerName, default(BlobContainerConfiguration), this._provider, this._blobNormalizeNamingService));
    }

    /// <summary>
    /// Checks that instance construction throws when the provider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullProvider()
    {
        Should.Throw<ArgumentNullException>(() => new ResourceFileContainer(this._containerName, this._configuration, default(IBlobProvider), this._blobNormalizeNamingService));
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
        Should.Throw<ArgumentNullException>(() => new ResourceFileContainer(value));
        Should.Throw<ArgumentNullException>(() => new ResourceFileContainer(value, this._configuration, this._provider, this._blobNormalizeNamingService));
    }

    /// <summary>
    /// Checks that the Get method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGet()
    {
        // Arrange
        var filename = "TestValue1244263314";

        // Act
        var result = this._testClass.Get(filename);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Get method throws when the filename parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetWithInvalidFilename(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Get(value));
    }

    /// <summary>
    /// Checks that the Get maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void GetPerformsMapping()
    {
        // Arrange
        var filename = "TestValue344729402";

        // Act
        var result = this._testClass.Get(filename);

        // Assert
        result.FileName.ShouldBeSameAs(filename);
    }
}