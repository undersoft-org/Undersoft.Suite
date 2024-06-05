using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Application.Server.Controllers.Open;
using Undersoft.SDK.Service;

namespace Undersoft.SCC.Service.Application.Server.Tests.Controllers.Open;

/// <summary>
/// Unit tests for the type <see cref="EventController"/>.
/// </summary>
[TestClass]
public class EventControllerTests
{
    private EventController _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventController"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new EventController(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventController(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new EventController(default(IServicer)));
    }
}