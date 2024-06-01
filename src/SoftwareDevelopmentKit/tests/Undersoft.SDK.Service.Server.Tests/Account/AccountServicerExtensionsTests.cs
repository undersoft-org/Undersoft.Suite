using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountServicerExtensions"/>.
/// </summary>
[TestClass]
public class AccountServicerExtensionsTests
{
    /// <summary>
    /// Checks that the GetIdentityManager method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetIdentityManager()
    {
        // Arrange
        var servicer = Substitute.For<IServicer>();

        // Act
        var result = servicer.GetIdentityManager();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetIdentityManager method throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetIdentityManagerWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => default(IServicer).GetIdentityManager());
    }
}