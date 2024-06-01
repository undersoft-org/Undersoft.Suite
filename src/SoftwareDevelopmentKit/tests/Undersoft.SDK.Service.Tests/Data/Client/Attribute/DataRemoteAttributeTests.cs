using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client.Attributes;

namespace Undersoft.SDK.Service.Tests.Data.Client.Attributes;

/// <summary>
/// Unit tests for the type <see cref="DataRemoteAttribute"/>.
/// </summary>
[TestClass]
public class DataRemoteAttributeTests
{
    private DataRemoteAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="DataRemoteAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new DataRemoteAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new DataRemoteAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}