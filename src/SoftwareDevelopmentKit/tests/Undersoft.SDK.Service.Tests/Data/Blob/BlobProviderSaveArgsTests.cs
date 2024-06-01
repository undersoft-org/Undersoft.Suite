using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobProviderSaveArgs"/>.
/// </summary>
[TestClass]
public class BlobProviderSaveArgsTests
{
    private BlobProviderSaveArgs _testClass;
    private string _containerName;
    private BlobContainerConfiguration _configuration;
    private string _blobName;
    private Stream _blobStream;
    private bool _overrideExisting;
    private CancellationToken _cancellationToken;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobProviderSaveArgs"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._containerName = "TestValue527048149";
        this._configuration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        this._blobName = "TestValue267403737";
        this._blobStream = new MemoryStream();
        this._overrideExisting = true;
        this._cancellationToken = CancellationToken.None;
        this._testClass = new BlobProviderSaveArgs(this._containerName, this._configuration, this._blobName, this._blobStream, this._overrideExisting, this._cancellationToken);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobProviderSaveArgs(this._containerName, this._configuration, this._blobName, this._blobStream, this._overrideExisting, this._cancellationToken);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => new BlobProviderSaveArgs(this._containerName, default(BlobContainerConfiguration), this._blobName, this._blobStream, this._overrideExisting, this._cancellationToken));
    }

    /// <summary>
    /// Checks that instance construction throws when the blobStream parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullBlobStream()
    {
        Should.Throw<ArgumentNullException>(() => new BlobProviderSaveArgs(this._containerName, this._configuration, this._blobName, default(Stream), this._overrideExisting, this._cancellationToken));
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
        Should.Throw<ArgumentNullException>(() => new BlobProviderSaveArgs(value, this._configuration, this._blobName, this._blobStream, this._overrideExisting, this._cancellationToken));
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
        Should.Throw<ArgumentNullException>(() => new BlobProviderSaveArgs(this._containerName, this._configuration, value, this._blobStream, this._overrideExisting, this._cancellationToken));
    }

    /// <summary>
    /// Checks that the BlobStream property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void BlobStreamIsInitializedCorrectly()
    {
        this._testClass.BlobStream.ShouldBeSameAs(this._blobStream);
    }

    /// <summary>
    /// Checks that the OverrideExisting property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void OverrideExistingIsInitializedCorrectly()
    {
        this._testClass.OverrideExisting.ShouldBe(this._overrideExisting);
    }
}