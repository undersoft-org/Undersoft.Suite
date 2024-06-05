using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Application.Server.Controllers.Open;
using Undersoft.SDK.Service;

namespace Undersoft.SCC.Service.Application.Server.Tests.Controllers.Open;

/// <summary>
/// Unit tests for the type <see cref="ContactController"/>.
/// </summary>
[TestClass]
public class ContactControllerTests
{
    private ContactController _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactController"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new ContactController(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ContactController(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new ContactController(default(IServicer)));
    }
}