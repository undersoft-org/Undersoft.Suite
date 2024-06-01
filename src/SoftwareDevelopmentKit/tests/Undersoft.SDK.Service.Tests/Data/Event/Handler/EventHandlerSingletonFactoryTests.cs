using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Event.Handler;

namespace Undersoft.SDK.Service.Tests.Data.Event.Handler;

/// <summary>
/// Unit tests for the type <see cref="EventHandlerSingletonFactory"/>.
/// </summary>
[TestClass]
public class EventHandlerSingletonFactoryTests
{
    private EventHandlerSingletonFactory _testClass;
    private IEventHandler _handler;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventHandlerSingletonFactory"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._handler = Substitute.For<IEventHandler>();
        this._testClass = new EventHandlerSingletonFactory(this._handler);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventHandlerSingletonFactory(this._handler);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the handler parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullHandler()
    {
        Should.Throw<ArgumentNullException>(() => new EventHandlerSingletonFactory(default(IEventHandler)));
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
    /// Checks that the HandlerType property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetHandlerType()
    {
        // Assert
        this._testClass.HandlerType.ShouldBeOfType<Type>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the HandlerInstance property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetHandlerInstance()
    {
        // Assert
        this._testClass.HandlerInstance.ShouldBeOfType<IEventHandler>();

        Assert.Fail("Create or modify test");
    }
}