using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Event.Handler;
using TEvent = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Event.Handler;

/// <summary>
/// Unit tests for the type <see cref="ActionEventHandler"/>.
/// </summary>
[TestClass]
public class ActionEventHandler_1Tests
{
    private ActionEventHandler<TEvent> _testClass;
    private Func<TEvent, Task> _handler;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ActionEventHandler"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._handler = x => Task.CompletedTask;
        this._testClass = new ActionEventHandler<TEvent>(this._handler);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ActionEventHandler<TEvent>(this._handler);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the handler parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullHandler()
    {
        Should.Throw<ArgumentNullException>(() => new ActionEventHandler<TEvent>(default(Func<TEvent, Task>)));
    }

    /// <summary>
    /// Checks that the HandleEventAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallHandleEventAsync()
    {
        // Arrange
        var eventData = "TestValue2096356645";

        // Act
        await this._testClass.HandleEventAsync(eventData);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Action property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetAction()
    {
        // Assert
        this._testClass.Action.ShouldBeOfType<Func<TEvent, Task>>();

        Assert.Fail("Create or modify test");
    }
}