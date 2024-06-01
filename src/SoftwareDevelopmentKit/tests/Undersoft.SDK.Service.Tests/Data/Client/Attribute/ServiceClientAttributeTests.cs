using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client.Attributes;

namespace Undersoft.SDK.Service.Tests.Data.Client.Attributes;

/// <summary>
/// Unit tests for the type <see cref="ServiceClientAttribute"/>.
/// </summary>
[TestClass]
public class ServiceClientAttributeTests
{
    private ServiceClientAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceClientAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ServiceClientAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceClientAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}