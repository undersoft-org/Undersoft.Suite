using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting;

/// <summary>
/// Unit tests for the type <see cref="NavMenu"/>.
/// </summary>
[TestClass]
public class NavMenuTests
{
    private NavMenu _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="NavMenu"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new NavMenu();
    }

    /// <summary>
    /// Checks that the Contacts property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContacts()
    {
        // Arrange
        var testValue = new Contacts();

        // Act
        this._testClass.Contacts = testValue;

        // Assert
        this._testClass.Contacts.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Dictionaries property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDictionaries()
    {
        // Arrange
        var testValue = new Dictionaries
        {
            Groups = "TestValue1135288293",
            Countries = "TestValue1702133369"
        };

        // Act
        this._testClass.Dictionaries = testValue;

        // Assert
        this._testClass.Dictionaries.ShouldBeSameAs(testValue);
    }
}