using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using TAuth = System.String;
using TContext = Undersoft.SDK.Service.Data.Store.DataStoreContext;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="DataServerBuilder"/>.
/// </summary>
[TestClass]
public class DataServerBuilderTests
{
    private class TestDataServerBuilder : DataServerBuilder
    {
        public TestDataServerBuilder() : base()
        {
        }

        public override void Build()
        {
        }
    }

    private TestDataServerBuilder _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="DataServerBuilder"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new TestDataServerBuilder();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new TestDataServerBuilder();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the AddDataServices method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddDataServices()
    {
        // Act
        var result = this._testClass.AddDataServices<TContext>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddInvocations method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddInvocations()
    {
        // Act
        var result = this._testClass.AddInvocations<TAuth>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ServiceTypes property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServiceTypes()
    {
        // Arrange
        var testValue = DataServerTypes.OData;

        // Act
        TestDataServerBuilder.ServiceTypes = testValue;

        // Assert
        TestDataServerBuilder.ServiceTypes.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the RoutePrefix property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRoutePrefix()
    {
        // Arrange
        var testValue = "TestValue554710357";

        // Act
        this._testClass.RoutePrefix = testValue;

        // Assert
        this._testClass.RoutePrefix.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PageLimit property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPageLimit()
    {
        // Arrange
        var testValue = 1707429220;

        // Act
        this._testClass.PageLimit = testValue;

        // Assert
        this._testClass.PageLimit.ShouldBe(testValue);
    }
}