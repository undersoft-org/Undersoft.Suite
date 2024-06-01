using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;

namespace Undersoft.SDK.Service.Tests.Data.Blob.Container;

/// <summary>
/// Unit tests for the type <see cref="BlobContainerExtensions"/>.
/// </summary>
[TestClass]
public class BlobContainerExtensionsTests
{
    /// <summary>
    /// Checks that the SaveAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveAsync()
    {
        // Arrange
        var container = Substitute.For<IBlobContainer>();
        var name = "TestValue1728721559";
        var bytes = new byte[] { 171, 214, 83, 54 };
        var overrideExisting = true;
        var cancellationToken = CancellationToken.None;

        // Act
        await container.SaveAsync(name, bytes, overrideExisting, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SaveAsync method throws when the container parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSaveAsyncWithNullContainerAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => default(IBlobContainer).SaveAsync("TestValue327755061", new byte[] { 148, 200, 169, 133 }, false, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the SaveAsync method throws when the bytes parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSaveAsyncWithNullBytesAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => Substitute.For<IBlobContainer>().SaveAsync("TestValue9681567", default(byte[]), false, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the SaveAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSaveAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => Substitute.For<IBlobContainer>().SaveAsync(value, new byte[] { 185, 154, 105, 228 }, true, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the GetAllBytesAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetAllBytesAsync()
    {
        // Arrange
        var container = Substitute.For<IBlobContainer>();
        var name = "TestValue2145664724";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await container.GetAllBytesAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetAllBytesAsync method throws when the container parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallGetAllBytesAsyncWithNullContainerAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => default(IBlobContainer).GetAllBytesAsync("TestValue1144374224", CancellationToken.None));
    }

    /// <summary>
    /// Checks that the GetAllBytesAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetAllBytesAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => Substitute.For<IBlobContainer>().GetAllBytesAsync(value, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the GetAllBytesOrNullAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetAllBytesOrNullAsync()
    {
        // Arrange
        var container = Substitute.For<IBlobContainer>();
        var name = "TestValue1601558395";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await container.GetAllBytesOrNullAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetAllBytesOrNullAsync method throws when the container parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallGetAllBytesOrNullAsyncWithNullContainerAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => default(IBlobContainer).GetAllBytesOrNullAsync("TestValue1533470363", CancellationToken.None));
    }

    /// <summary>
    /// Checks that the GetAllBytesOrNullAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetAllBytesOrNullAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => Substitute.For<IBlobContainer>().GetAllBytesOrNullAsync(value, CancellationToken.None));
    }
}