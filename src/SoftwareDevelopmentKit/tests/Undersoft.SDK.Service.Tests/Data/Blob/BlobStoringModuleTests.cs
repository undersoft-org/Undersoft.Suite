using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Configuration;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobStoringModule"/>.
/// </summary>
[TestClass]
public class BlobStoringModuleTests
{
    private BlobStoringModule _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobStoringModule"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new BlobStoringModule();
    }

    /// <summary>
    /// Checks that the ConfigureServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConfigureServices()
    {
        // Arrange
        var context = new ServiceConfigurationContext(Substitute.For<IServiceRegistry>());

        // Act
        this._testClass.ConfigureServices(context);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ConfigureServices method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallConfigureServicesWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.ConfigureServices(default(ServiceConfigurationContext)));
    }
}