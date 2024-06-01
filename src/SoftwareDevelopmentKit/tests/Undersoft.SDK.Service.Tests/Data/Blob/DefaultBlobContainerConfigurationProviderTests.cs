using System;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="DefaultBlobContainerConfigurationProvider"/>.
/// </summary>
[TestClass]
public class DefaultBlobContainerConfigurationProviderTests
{
    private DefaultBlobContainerConfigurationProvider _testClass;
    private IOptions<BlobStoringOptions> _options;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="DefaultBlobContainerConfigurationProvider"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = Substitute.For<IOptions<BlobStoringOptions>>();
        this._testClass = new DefaultBlobContainerConfigurationProvider(this._options);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new DefaultBlobContainerConfigurationProvider(this._options);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new DefaultBlobContainerConfigurationProvider(default(IOptions<BlobStoringOptions>)));
    }

    /// <summary>
    /// Checks that the Get method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGet()
    {
        // Arrange
        var name = "TestValue324231794";

        // Act
        var result = this._testClass.Get(name);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Get method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Get(value));
    }
}