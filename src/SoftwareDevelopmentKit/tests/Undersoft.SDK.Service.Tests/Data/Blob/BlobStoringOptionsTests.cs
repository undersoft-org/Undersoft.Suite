using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobStoringOptions"/>.
/// </summary>
[TestClass]
public class BlobStoringOptionsTests
{
    private BlobStoringOptions _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobStoringOptions"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new BlobStoringOptions();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobStoringOptions();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the Containers property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetContainers()
    {
        // Assert
        this._testClass.Containers.ShouldBeOfType<BlobContainerConfigurations>();

        Assert.Fail("Create or modify test");
    }
}