using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobProviderArgs"/>.
/// </summary>
[TestClass]
public class BlobProviderArgsTests
{
    private BlobProviderArgs _testClass;
    private string _containerName;
    private BlobContainerConfiguration _configuration;
    private string _blobName;
    private CancellationToken _cancellationToken;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobProviderArgs"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._containerName = "TestValue2055173715";
        this._configuration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        this._blobName = "TestValue2117550555";
        this._cancellationToken = CancellationToken.None;
        this._testClass = new BlobProviderArgs(this._containerName, this._configuration, this._blobName, this._cancellationToken);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobProviderArgs(this._containerName, this._configuration, this._blobName, this._cancellationToken);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => new BlobProviderArgs(this._containerName, default(BlobContainerConfiguration), this._blobName, this._cancellationToken));
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
        Should.Throw<ArgumentNullException>(() => new BlobProviderArgs(value, this._configuration, this._blobName, this._cancellationToken));
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
        Should.Throw<ArgumentNullException>(() => new BlobProviderArgs(this._containerName, this._configuration, value, this._cancellationToken));
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
    /// Checks that the Configuration property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ConfigurationIsInitializedCorrectly()
    {
        this._testClass.Configuration.ShouldBeSameAs(this._configuration);
    }

    /// <summary>
    /// Checks that the BlobName property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void BlobNameIsInitializedCorrectly()
    {
        this._testClass.BlobName.ShouldBe(this._blobName);
    }

    /// <summary>
    /// Checks that the CancellationToken property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void CancellationTokenIsInitializedCorrectly()
    {
        this._testClass.CancellationToken.ShouldBe(this._cancellationToken);
    }
}