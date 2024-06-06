using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System;
using Undersoft.SCC.Service.Application.Server.Controllers.Open;
using Undersoft.SDK.Service;

namespace Undersoft.SCC.Service.Application.Server.Tests.Controllers.Open;

/// <summary>
/// Unit tests for the type <see cref="GroupController"/>.
/// </summary>
[TestClass]
public class GroupControllerTests
{
    private GroupController _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="GroupController"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new GroupController(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new GroupController(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new GroupController(default(IServicer)));
    }
}