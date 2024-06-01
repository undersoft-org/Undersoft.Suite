using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client.Attributes;

namespace Undersoft.SDK.Service.Tests.Data.Client.Attributes;

/// <summary>
/// Unit tests for the type <see cref="StreamDataAttribute"/>.
/// </summary>
[TestClass]
public class StreamDataAttributeTests
{
    private StreamDataAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="StreamDataAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new StreamDataAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new StreamDataAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}