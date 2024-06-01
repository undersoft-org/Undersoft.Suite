using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Event.Handler;
using TEvent = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Event.Handler;

/// <summary>
/// Unit tests for the type <see cref="EventHandlerMethodExecutor"/>.
/// </summary>
[TestClass]
public class EventHandlerMethodExecutor_1Tests
{
    private EventHandlerMethodExecutor<TEvent> _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventHandlerMethodExecutor"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new EventHandlerMethodExecutor<TEvent>();
    }

    /// <summary>
    /// Checks that the ExecuteAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallExecuteAsync()
    {
        // Arrange
        var target = Substitute.For<IEventHandler>();
        var parameters = "TestValue1300983231";

        // Act
        await this._testClass.ExecuteAsync(target, parameters);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ExecuteAsync method throws when the target parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallExecuteAsyncWithNullTargetAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.ExecuteAsync(default(IEventHandler), "TestValue319544425"));
    }

    /// <summary>
    /// Checks that the ExecuteAsync method throws when the parameters parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallExecuteAsyncWithNullParametersAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.ExecuteAsync(Substitute.For<IEventHandler>(), default(TEvent)));
    }

    /// <summary>
    /// Checks that the ExecutorAsync property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetExecutorAsync()
    {
        // Assert
        this._testClass.ExecutorAsync.ShouldBeOfType<EventHandlerMethodExecutorAsync>();

        Assert.Fail("Create or modify test");
    }
}