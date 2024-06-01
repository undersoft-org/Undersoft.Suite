using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="Registration"/>.
/// </summary>
[TestClass]
public class RegistrationTests
{
    private Registration _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Registration"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Registration();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Registration();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that setting the Kind property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetKind()
    {
        this._testClass.CheckProperty(x => x.Kind, new RegistrationKind?(), new RegistrationKind?());
    }

    /// <summary>
    /// Checks that setting the Completed property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCompleted()
    {
        this._testClass.CheckProperty(x => x.Completed, false, false);
    }

    /// <summary>
    /// Checks that setting the Approved property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetApproved()
    {
        this._testClass.CheckProperty(x => x.Approved, false, true);
    }
}