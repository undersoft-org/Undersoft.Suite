using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="Personal"/>.
/// </summary>
[TestClass]
public class PersonalTests
{
    private Personal _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Personal"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Personal();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Personal();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that setting the FirstName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetFirstName()
    {
        this._testClass.CheckProperty(x => x.FirstName);
    }

    /// <summary>
    /// Checks that setting the SecondName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSecondName()
    {
        this._testClass.CheckProperty(x => x.SecondName);
    }

    /// <summary>
    /// Checks that setting the LastName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLastName()
    {
        this._testClass.CheckProperty(x => x.LastName);
    }

    /// <summary>
    /// Checks that setting the BirthDate property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBirthDate()
    {
        this._testClass.CheckProperty(x => x.BirthDate);
    }

    /// <summary>
    /// Checks that setting the Age property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAge()
    {
        this._testClass.CheckProperty(x => x.Age);
    }

    /// <summary>
    /// Checks that setting the Gender property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGender()
    {
        this._testClass.CheckProperty(x => x.Gender);
    }
}