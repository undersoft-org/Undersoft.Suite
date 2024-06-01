using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Event.Handler;

namespace Undersoft.SDK.Service.Tests.Data.Event.Handler;

/// <summary>
/// Unit tests for the type <see cref="EventHandlerScopeFactory"/>.
/// </summary>
[TestClass]
public class EventHandlerScopeFactoryTests
{
    private EventHandlerScopeFactory _testClass;
    private IServiceScopeFactory _scopeFactory;
    private Type _handlerType;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventHandlerScopeFactory"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._scopeFactory = Substitute.For<IServiceScopeFactory>();
        this._handlerType = typeof(string);
        this._testClass = new EventHandlerScopeFactory(this._scopeFactory, this._handlerType);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventHandlerScopeFactory(this._scopeFactory, this._handlerType);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the scopeFactory parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullScopeFactory()
    {
        Should.Throw<ArgumentNullException>(() => new EventHandlerScopeFactory(default(IServiceScopeFactory), this._handlerType));
    }

    /// <summary>
    /// Checks that instance construction throws when the handlerType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullHandlerType()
    {
        Should.Throw<ArgumentNullException>(() => new EventHandlerScopeFactory(this._scopeFactory, default(Type)));
    }

    /// <summary>
    /// Checks that the GetHandler method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetHandler()
    {
        // Act
        var result = this._testClass.GetHandler();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IsInFactories method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallIsInFactories()
    {
        // Arrange
        var handlerFactories = new List<IEventHandlerFactory>();

        // Act
        var result = this._testClass.IsInFactories(handlerFactories);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IsInFactories method throws when the handlerFactories parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallIsInFactoriesWithNullHandlerFactories()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.IsInFactories(default(List<IEventHandlerFactory>)));
    }

    /// <summary>
    /// Checks that the Dispose method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallDispose()
    {
        // Act
        this._testClass.Dispose();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the HandlerType property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void HandlerTypeIsInitializedCorrectly()
    {
        this._testClass.HandlerType.ShouldBeSameAs(this._handlerType);
    }
}