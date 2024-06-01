using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;
using TContainer = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Blob.Container;

/// <summary>
/// Unit tests for the type <see cref="BlobContainerConfigurationProviderExtensions"/>.
/// </summary>
[TestClass]
public class BlobContainerConfigurationProviderExtensionsTests
{
    /// <summary>
    /// Checks that the Get method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGet()
    {
        // Arrange
        var configurationProvider = Substitute.For<IBlobContainerConfigurationProvider>();

        // Act
        var result = configurationProvider.Get<TContainer>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Get method throws when the configurationProvider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetWithNullConfigurationProvider()
    {
        Should.Throw<ArgumentNullException>(() => default(IBlobContainerConfigurationProvider).Get<TContainer>());
    }
}