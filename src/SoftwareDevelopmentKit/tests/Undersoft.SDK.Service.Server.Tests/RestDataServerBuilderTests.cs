using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using TStore = Undersoft.SDK.Service.Data.Store.IDataStore;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="RestDataServerBuilder"/>.
/// </summary>
[TestClass]
public class RestDataServerBuilder_1Tests
{
    private RestDataServerBuilder<TStore> _testClass;
    private IServiceRegistry _registry;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="RestDataServerBuilder"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._registry = Substitute.For<IServiceRegistry>();
        this._testClass = new RestDataServerBuilder<TStore>(this._registry);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new RestDataServerBuilder<TStore>(this._registry);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the registry parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullRegistry()
    {
        Should.Throw<ArgumentNullException>(() => new RestDataServerBuilder<TStore>(default(IServiceRegistry)));
    }

    /// <summary>
    /// Checks that the AddControllers method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddControllers()
    {
        // Act
        this._testClass.AddControllers();

        // Assert
        Assert.Fail("Create or modify test");
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
}