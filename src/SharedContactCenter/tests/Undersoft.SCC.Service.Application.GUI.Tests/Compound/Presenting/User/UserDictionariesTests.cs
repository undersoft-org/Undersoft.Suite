using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting.User;

/// <summary>
/// Unit tests for the type <see cref="UserDictionaries"/>.
/// </summary>
[TestClass]
public class UserDictionariesTests
{
    private UserDictionaries _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="UserDictionaries"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new UserDictionaries();
    }

    /// <summary>
    /// Checks that the Groups property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGroups()
    {
        // Arrange
        var testValue = "TestValue1068543983";

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
        var testValue = "TestValue1569427061";

        // Act
        this._testClass.Countries = testValue;

        // Assert
        this._testClass.Countries.ShouldBe(testValue);
    }
}