using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="DefaultBlobProviderSelector"/>.
/// </summary>
[TestClass]
public class DefaultBlobProviderSelectorTests
{
    private DefaultBlobProviderSelector _testClass;
    private IBlobContainerConfigurationProvider _configurationProvider;
    private IEnumerable<IBlobProvider> _blobProviders;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="DefaultBlobProviderSelector"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._configurationProvider = Substitute.For<IBlobContainerConfigurationProvider>();
        this._blobProviders = new[] { Substitute.For<IBlobProvider>(), Substitute.For<IBlobProvider>(), Substitute.For<IBlobProvider>() };
        this._testClass = new DefaultBlobProviderSelector(this._configurationProvider, this._blobProviders);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new DefaultBlobProviderSelector(this._configurationProvider, this._blobProviders);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the configurationProvider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfigurationProvider()
    {
        Should.Throw<ArgumentNullException>(() => new DefaultBlobProviderSelector(default(IBlobContainerConfigurationProvider), this._blobProviders));
    }

    /// <summary>
    /// Checks that instance construction throws when the blobProviders parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullBlobProviders()
    {
        Should.Throw<ArgumentNullException>(() => new DefaultBlobProviderSelector(this._configurationProvider, default(IEnumerable<IBlobProvider>)));
    }

    /// <summary>
    /// Checks that the Get method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGet()
    {
        // Arrange
        var containerName = "TestValue1013712496";

        // Act
        var result = this._testClass.Get(containerName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Get method throws when the containerName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetWithInvalidContainerName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Get(value));
    }
}