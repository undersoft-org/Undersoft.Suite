using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using TAuth = System.String;
using TDto = System.String;
using TStore = Undersoft.SDK.Service.Data.Store.IDataStore;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="OpenDataServerBuilder"/>.
/// </summary>
[TestClass]
public class OpenDataServerBuilder_1Tests
{
    private OpenDataServerBuilder<TStore> _testClass;
    private IServiceRegistry _registry;
    private string _routePrefix;
    private int _pageLimit;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="OpenDataServerBuilder"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._registry = Substitute.For<IServiceRegistry>();
        this._routePrefix = "TestValue1233613617";
        this._pageLimit = 1290898011;
        this._testClass = new OpenDataServerBuilder<TStore>(this._registry, this._routePrefix, this._pageLimit);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new OpenDataServerBuilder<TStore>(this._registry);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new OpenDataServerBuilder<TStore>(this._registry, this._routePrefix, this._pageLimit);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the registry parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullRegistry()
    {
        Should.Throw<ArgumentNullException>(() => new OpenDataServerBuilder<TStore>(default(IServiceRegistry)));
        Should.Throw<ArgumentNullException>(() => new OpenDataServerBuilder<TStore>(default(IServiceRegistry), this._routePrefix, this._pageLimit));
    }

    /// <summary>
    /// Checks that the constructor throws when the routePrefix parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidRoutePrefix(string value)
    {
        Should.Throw<ArgumentNullException>(() => new OpenDataServerBuilder<TStore>(this._registry, value, this._pageLimit));
    }

    /// <summary>
    /// Checks that the Build method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallBuild()
    {
        // Act
        this._testClass.Build();

        // Assert
        _registry.Received().MergeServices(Arg.Any<bool>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the EntitySet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallEntitySetWithEntityType()
    {
        // Arrange
        var entityType = typeof(string);

        // Act
        var result = this._testClass.EntitySet(entityType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the EntitySet method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallEntitySetWithEntityTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.EntitySet(default(Type)));
    }

    /// <summary>
    /// Checks that the EntitySet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallEntitySetWithTDto()
    {
        // Act
        var result = this._testClass.EntitySet<TDto>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEdm method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEdm()
    {
        // Act
        var result = this._testClass.GetEdm();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the BuildEdm method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallBuildEdm()
    {
        // Act
        this._testClass.BuildEdm();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddODataServicer method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddODataServicer()
    {
        // Arrange
        var mvc = Substitute.For<IMvcBuilder>();

        // Act
        var result = this._testClass.AddODataServicer(mvc);

        // Assert
        _registry.Received().MergeServices(Arg.Any<bool>());

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AddODataServicer method throws when the mvc parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddODataServicerWithNullMvc()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AddODataServicer(default(IMvcBuilder)));
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
}