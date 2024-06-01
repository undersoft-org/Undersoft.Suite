using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client.Attributes;

namespace Undersoft.SDK.Service.Tests.Data.Client.Attributes;

/// <summary>
/// Unit tests for the type <see cref="StreamDataRemoteAttribute"/>.
/// </summary>
[TestClass]
public class StreamDataRemoteAttributeTests
{
    private StreamDataRemoteAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="StreamDataRemoteAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new StreamDataRemoteAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new StreamDataRemoteAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}