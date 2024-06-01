using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.SDK.Service.Server.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServerHostJwtMiddleware"/>.
/// </summary>
[TestClass]
public class ServerHostJwtMiddlewareTests
{
    private ServerHostJwtMiddleware _testClass;
    private RequestDelegate _next;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServerHostJwtMiddleware"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._next = x => Task.CompletedTask;
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new ServerHostJwtMiddleware(this._next, this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServerHostJwtMiddleware(this._next, this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the next parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullNext()
    {
        Should.Throw<ArgumentNullException>(() => new ServerHostJwtMiddleware(default(RequestDelegate), this._servicer));
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new ServerHostJwtMiddleware(this._next, default(IServicer)));
    }

    /// <summary>
    /// Checks that the Invoke method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallInvokeAsync()
    {
        // Arrange
        var context = new DefaultHttpContext();

        // Act
        await this._testClass.Invoke(context);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Invoke method throws when the context parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallInvokeWithNullContextAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Invoke(default(HttpContext)));
    }
}