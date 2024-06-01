using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using TContainer = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobProviderSelectorExtensions"/>.
/// </summary>
[TestClass]
public class BlobProviderSelectorExtensionsTests
{
    /// <summary>
    /// Checks that the Get method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGet()
    {
        // Arrange
        var selector = Substitute.For<IBlobProviderSelector>();

        // Act
        var result = selector.Get<TContainer>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Get method throws when the selector parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetWithNullSelector()
    {
        Should.Throw<ArgumentNullException>(() => default(IBlobProviderSelector).Get<TContainer>());
    }
}