using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Event.Bus;
using Undersoft.SDK.Service.Data.Event.Handler;

namespace Undersoft.SDK.Service.Tests.Data.Event.Handler;

/// <summary>
/// Unit tests for the type <see cref="EventHandlerFactoryUnregistrar"/>.
/// </summary>
[TestClass]
public class EventHandlerFactoryUnregistrarTests
{
    private EventHandlerFactoryUnregistrar _testClass;
    private IEventBus _eventBus;
    private Type _eventType;
    private IEventHandlerFactory _factory;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventHandlerFactoryUnregistrar"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._eventBus = Substitute.For<IEventBus>();
        this._eventType = typeof(string);
        this._factory = Substitute.For<IEventHandlerFactory>();
        this._testClass = new EventHandlerFactoryUnregistrar(this._eventBus, this._eventType, this._factory);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventHandlerFactoryUnregistrar(this._eventBus, this._eventType, this._factory);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the eventBus parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullEventBus()
    {
        Should.Throw<ArgumentNullException>(() => new EventHandlerFactoryUnregistrar(default(IEventBus), this._eventType, this._factory));
    }

    /// <summary>
    /// Checks that instance construction throws when the eventType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullEventType()
    {
        Should.Throw<ArgumentNullException>(() => new EventHandlerFactoryUnregistrar(this._eventBus, default(Type), this._factory));
    }

    /// <summary>
    /// Checks that instance construction throws when the factory parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullFactory()
    {
        Should.Throw<ArgumentNullException>(() => new EventHandlerFactoryUnregistrar(this._eventBus, this._eventType, default(IEventHandlerFactory)));
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
        _eventBus.Received().Unsubscribe(Arg.Any<Type>(), Arg.Any<IEventHandlerFactory>());

        Assert.Fail("Create or modify test");
    }
}