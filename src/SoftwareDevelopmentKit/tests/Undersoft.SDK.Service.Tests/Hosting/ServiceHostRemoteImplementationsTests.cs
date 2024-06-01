using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServiceHostRemoteImplementations"/>.
/// </summary>
[TestClass]
public class ServiceHostRemoteImplementationsTests
{
    /// <summary>
    /// Checks that the AddOpenDataRemoteImplementations method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddOpenDataRemoteImplementations()
    {
        // Arrange
        var reg = Substitute.For<IServiceRegistry>();

        // Act
        reg.AddOpenDataRemoteImplementations();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddOpenDataRemoteImplementations method throws when the reg parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddOpenDataRemoteImplementationsWithNullReg()
    {
        Should.Throw<ArgumentNullException>(() => default(IServiceRegistry).AddOpenDataRemoteImplementations());
    }
}