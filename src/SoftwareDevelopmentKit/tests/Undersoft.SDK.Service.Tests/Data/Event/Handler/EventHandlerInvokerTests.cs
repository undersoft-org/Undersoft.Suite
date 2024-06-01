using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Event.Handler;

namespace Undersoft.SDK.Service.Tests.Data.Event.Handler;

/// <summary>
/// Unit tests for the type <see cref="EventHandlerInvoker"/>.
/// </summary>
[TestClass]
public class EventHandlerInvokerTests
{
    private EventHandlerInvoker _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventHandlerInvoker"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new EventHandlerInvoker();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventHandlerInvoker();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the InvokeAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallInvokeAsync()
    {
        // Arrange
        var eventHandler = Substitute.For<IEventHandler>();
        var eventData = new object();
        var eventType = typeof(string);

        // Act
        await this._testClass.InvokeAsync(eventHandler, eventData, eventType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the InvokeAsync method throws when the eventHandler parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallInvokeAsyncWithNullEventHandlerAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.InvokeAsync(default(IEventHandler), new object(), typeof(string)));
    }

    /// <summary>
    /// Checks that the InvokeAsync method throws when the eventData parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallInvokeAsyncWithNullEventDataAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.InvokeAsync(Substitute.For<IEventHandler>(), default(object), typeof(string)));
    }

    /// <summary>
    /// Checks that the InvokeAsync method throws when the eventType parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallInvokeAsyncWithNullEventTypeAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.InvokeAsync(Substitute.For<IEventHandler>(), new object(), default(Type)));
    }
}