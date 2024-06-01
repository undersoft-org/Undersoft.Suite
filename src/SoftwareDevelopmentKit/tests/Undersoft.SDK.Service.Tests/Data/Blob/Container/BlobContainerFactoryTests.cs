using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;

namespace Undersoft.SDK.Service.Tests.Data.Blob.Container;

/// <summary>
/// Unit tests for the type <see cref="BlobContainerFactory"/>.
/// </summary>
[TestClass]
public class BlobContainerFactoryTests
{
    private BlobContainerFactory _testClass;
    private IBlobContainerConfigurationProvider _configurationProvider;
    private IBlobProviderSelector _providerSelector;
    private IServiceProvider _serviceProvider;
    private IBlobNormalizeNamingService _blobNormalizeNamingService;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobContainerFactory"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._configurationProvider = Substitute.For<IBlobContainerConfigurationProvider>();
        this._providerSelector = Substitute.For<IBlobProviderSelector>();
        this._serviceProvider = Substitute.For<IServiceProvider>();
        this._blobNormalizeNamingService = Substitute.For<IBlobNormalizeNamingService>();
        this._testClass = new BlobContainerFactory(this._configurationProvider, this._providerSelector, this._serviceProvider, this._blobNormalizeNamingService);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobContainerFactory(this._configurationProvider, this._providerSelector, this._serviceProvider, this._blobNormalizeNamingService);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the configurationProvider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfigurationProvider()
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainerFactory(default(IBlobContainerConfigurationProvider), this._providerSelector, this._serviceProvider, this._blobNormalizeNamingService));
    }

    /// <summary>
    /// Checks that instance construction throws when the providerSelector parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullProviderSelector()
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainerFactory(this._configurationProvider, default(IBlobProviderSelector), this._serviceProvider, this._blobNormalizeNamingService));
    }

    /// <summary>
    /// Checks that instance construction throws when the serviceProvider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServiceProvider()
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainerFactory(this._configurationProvider, this._providerSelector, default(IServiceProvider), this._blobNormalizeNamingService));
    }

    /// <summary>
    /// Checks that instance construction throws when the blobNormalizeNamingService parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullBlobNormalizeNamingService()
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainerFactory(this._configurationProvider, this._providerSelector, this._serviceProvider, default(IBlobNormalizeNamingService)));
    }

    /// <summary>
    /// Checks that the Create method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreate()
    {
        // Arrange
        var name = "TestValue241266993";

        // Act
        var result = this._testClass.Create(name);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Create method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallCreateWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Create(value));
    }
}