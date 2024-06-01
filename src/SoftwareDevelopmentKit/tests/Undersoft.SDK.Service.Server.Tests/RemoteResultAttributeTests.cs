using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="RemoteResultAttribute"/>.
/// </summary>
[TestClass]
public class RemoteResultAttributeTests
{
    private RemoteResultAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="RemoteResultAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new RemoteResultAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new RemoteResultAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}