using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System;
using Undersoft.SCC.Service.Server.Controllers.Api;
using Undersoft.SDK.Service;

namespace Undersoft.SCC.Service.Server.Tests.Controllers.Api;

/// <summary>
/// Unit tests for the type <see cref="CountriesController"/>.
/// </summary>
[TestClass]
public class CountriesControllerTests
{
    private CountriesController _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="CountriesController"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new CountriesController(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new CountriesController(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new CountriesController(default(IServicer)));
    }
}