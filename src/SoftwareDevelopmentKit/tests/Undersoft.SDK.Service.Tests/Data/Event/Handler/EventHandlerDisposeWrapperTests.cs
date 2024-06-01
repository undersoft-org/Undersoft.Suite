using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Event.Handler;

namespace Undersoft.SDK.Service.Tests.Data.Event.Handler;

/// <summary>
/// Unit tests for the type <see cref="EventHandlerDisposeWrapper"/>.
/// </summary>
[TestClass]
public class EventHandlerDisposeWrapperTests
{
    private EventHandlerDisposeWrapper _testClass;
    private IEventHandler _eventHandler;
    private Action _disposeAction;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventHandlerDisposeWrapper"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._eventHandler = Substitute.For<IEventHandler>();
        this._disposeAction = () => { };
        this._testClass = new EventHandlerDisposeWrapper(this._eventHandler, this._disposeAction);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventHandlerDisposeWrapper(this._eventHandler, this._disposeAction);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the eventHandler parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullEventHandler()
    {
        Should.Throw<ArgumentNullException>(() => new EventHandlerDisposeWrapper(default(IEventHandler), this._disposeAction));
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
    /// Checks that the EventHandler property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void EventHandlerIsInitializedCorrectly()
    {
        this._testClass.EventHandler.ShouldBeSameAs(this._eventHandler);
    }
}