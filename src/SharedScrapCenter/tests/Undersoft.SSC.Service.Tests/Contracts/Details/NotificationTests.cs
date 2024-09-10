using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="Notification"/>.
/// </summary>
[TestClass]
public class NotificationTests
{
    private Notification _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Notification"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Notification();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Notification();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that setting the Name property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        this._testClass.CheckProperty(x => x.Name);
    }

    /// <summary>
    /// Checks that setting the Description property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDescription()
    {
        this._testClass.CheckProperty(x => x.Description);
    }
}