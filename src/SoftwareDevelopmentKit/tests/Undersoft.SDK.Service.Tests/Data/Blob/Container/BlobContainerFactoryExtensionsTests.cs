using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;
using TContainer = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Blob.Container;

/// <summary>
/// Unit tests for the type <see cref="BlobContainerFactoryExtensions"/>.
/// </summary>
[TestClass]
public class BlobContainerFactoryExtensionsTests
{
    /// <summary>
    /// Checks that the Create method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreate()
    {
        // Arrange
        var blobContainerFactory = Substitute.For<IBlobContainerFactory>();

        // Act
        var result = blobContainerFactory
        .Create<TContainer>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Create method throws when the blobContainerFactory parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCreateWithNullBlobContainerFactory()
    {
        Should.Throw<ArgumentNullException>(() => default(IBlobContainerFactory).Create<TContainer>());
    }
}