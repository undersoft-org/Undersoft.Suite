using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Application.Server.Pages;

namespace Undersoft.SCC.Service.Application.Server.Tests.Pages;

/// <summary>
/// Unit tests for the type <see cref="ErrorModel"/>.
/// </summary>
[TestClass]
public class ErrorModelTests
{
    private ErrorModel _testClass;
    private ILogger<ErrorModel> _logger;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ErrorModel"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._logger = Substitute.For<ILogger<ErrorModel>>();
        this._testClass = new ErrorModel(this._logger);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ErrorModel(this._logger);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the logger parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullLogger()
    {
        Should.Throw<ArgumentNullException>(() => new ErrorModel(default(ILogger<ErrorModel>)));
    }

    /// <summary>
    /// Checks that the OnGet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallOnGet()
    {
        // Act
        this._testClass.OnGet();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the RequestId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRequestId()
    {
        // Arrange
        var testValue = "TestValue1302288357";

        // Act
        this._testClass.RequestId = testValue;

        // Assert
        this._testClass.RequestId.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the ShowRequestId property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetShowRequestId()
    {
        // Assert
        this._testClass.ShowRequestId.ShouldBeOfType<bool>();

        Assert.Fail("Create or modify test");
    }
}