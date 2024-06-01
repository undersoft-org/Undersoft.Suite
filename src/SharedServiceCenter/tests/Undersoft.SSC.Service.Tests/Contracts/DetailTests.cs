using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Detail"/>.
/// </summary>
[TestClass]
public class DetailTests
{
    private Detail _testClass;
    private DetailKind _kind;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Detail"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._kind = DetailKind.Ranking;
        this._testClass = new Detail(this._kind);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Detail();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Detail(this._kind);

        // Assert
        instance.ShouldNotBeNull();
    }
}