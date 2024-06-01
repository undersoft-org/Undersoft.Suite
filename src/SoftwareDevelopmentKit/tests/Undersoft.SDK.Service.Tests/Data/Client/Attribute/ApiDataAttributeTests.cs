using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client.Attributes;

namespace Undersoft.SDK.Service.Tests.Data.Client.Attributes;

/// <summary>
/// Unit tests for the type <see cref="ApiDataAttribute"/>.
/// </summary>
[TestClass]
public class ApiDataAttributeTests
{
    private ApiDataAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ApiDataAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ApiDataAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ApiDataAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}