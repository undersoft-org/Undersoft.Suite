using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting;

/// <summary>
/// Unit tests for the type <see cref="Dictionaries"/>.
/// </summary>
[TestClass]
public class DictionariesTests
{
    private Dictionaries _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Dictionaries"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Dictionaries();
    }

    /// <summary>
    /// Checks that the Groups property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGroups()
    {
        // Arrange
        var testValue = "TestValue569594273";

        // Act
        this._testClass.Groups = testValue;

        // Assert
        this._testClass.Groups.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Countries property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountries()
    {
        // Arrange
        var testValue = "TestValue1050350599";

        // Act
        this._testClass.Countries = testValue;

        // Assert
        this._testClass.Countries.ShouldBe(testValue);
    }
}