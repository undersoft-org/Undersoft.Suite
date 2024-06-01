using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client.Attributes;

namespace Undersoft.SDK.Service.Tests.Data.Client.Attributes;

/// <summary>
/// Unit tests for the type <see cref="ApiDataRemoteAttribute"/>.
/// </summary>
[TestClass]
public class ApiDataRemoteAttributeTests
{
    private ApiDataRemoteAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ApiDataRemoteAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ApiDataRemoteAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ApiDataRemoteAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}