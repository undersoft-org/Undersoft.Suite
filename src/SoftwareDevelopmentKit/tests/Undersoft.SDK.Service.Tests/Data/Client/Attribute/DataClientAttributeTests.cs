using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client.Attributes;

namespace Undersoft.SDK.Service.Tests.Data.Client.Attributes;

/// <summary>
/// Unit tests for the type <see cref="DataClientAttribute"/>.
/// </summary>
[TestClass]
public class DataClientAttributeTests
{
    private class TestDataClientAttribute : DataClientAttribute
    {
        public TestDataClientAttribute() : base()
        {
        }
    }

    private TestDataClientAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="DataClientAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new TestDataClientAttribute();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new TestDataClientAttribute();

        // Assert
        instance.ShouldNotBeNull();
    }
}