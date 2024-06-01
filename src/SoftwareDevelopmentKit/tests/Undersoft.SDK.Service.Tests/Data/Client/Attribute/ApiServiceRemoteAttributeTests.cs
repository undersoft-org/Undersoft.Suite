using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client.Attributes;

namespace Undersoft.SDK.Service.Tests.Data.Client.Attributes;

/// <summary>
/// Unit tests for the type <see cref="ApiServiceRemoteAttribute"/>.
/// </summary>
[TestClass]
public class ApiServiceRemoteAttributeTests
{
    private ApiServiceRemoteAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ApiServiceRemoteAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ApiServiceRemoteAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ApiServiceRemoteAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}