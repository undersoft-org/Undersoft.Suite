using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System;
using Undersoft.SCC.Service.Server.Controllers.Api;
using Undersoft.SDK.Service;

namespace Undersoft.SCC.Service.Server.Tests.Controllers.Api;

/// <summary>
/// Unit tests for the type <see cref="EventsController"/>.
/// </summary>
[TestClass]
public class EventsControllerTests
{
    private EventsController _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventsController"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new EventsController(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventsController(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new EventsController(default(IServicer)));
    }
}