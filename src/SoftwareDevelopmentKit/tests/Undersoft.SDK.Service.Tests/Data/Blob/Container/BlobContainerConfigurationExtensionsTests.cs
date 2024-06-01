using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob.Container;
using T = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Blob.Container;

/// <summary>
/// Unit tests for the type <see cref="BlobContainerConfigurationExtensions"/>.
/// </summary>
[TestClass]
public class BlobContainerConfigurationExtensionsTests
{
    /// <summary>
    /// Checks that the GetConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetConfigurationWithTAndBlobContainerConfigurationAndString()
    {
        // Arrange
        var containerConfiguration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        var name = "TestValue535436997";

        // Act
        var result = containerConfiguration.GetConfiguration<T>(name);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetConfiguration method throws when the containerConfiguration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetConfigurationWithTAndBlobContainerConfigurationAndStringWithNullContainerConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => default(BlobContainerConfiguration).GetConfiguration<T>("TestValue643281935"));
    }

    /// <summary>
    /// Checks that the GetConfiguration method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetConfigurationWithTAndBlobContainerConfigurationAndStringWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainerConfiguration(default(BlobContainerConfiguration)).GetConfiguration<T>(value));
    }

    /// <summary>
    /// Checks that the GetConfiguration method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetConfigurationWithBlobContainerConfigurationAndString()
    {
        // Arrange
        var containerConfiguration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        var name = "TestValue99079191";

        // Act
        var result = containerConfiguration.GetConfiguration(name);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetConfiguration method throws when the containerConfiguration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetConfigurationWithBlobContainerConfigurationAndStringWithNullContainerConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => default(BlobContainerConfiguration).GetConfiguration("TestValue270754178"));
    }

    /// <summary>
    /// Checks that the GetConfiguration method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetConfigurationWithBlobContainerConfigurationAndStringWithInvalidName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainerConfiguration(default(BlobContainerConfiguration)).GetConfiguration(value));
    }
}