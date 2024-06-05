using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting.User;

/// <summary>
/// Unit tests for the type <see cref="UserNavMenu"/>.
/// </summary>
[TestClass]
public class UserNavMenuTests
{
    private UserNavMenu _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="UserNavMenu"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new UserNavMenu();
    }

    /// <summary>
    /// Checks that the Contacts property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContacts()
    {
        // Arrange
        var testValue = new UserContacts();

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
        var testValue = new UserDictionaries
        {
            Groups = "TestValue2102484736",
            Countries = "TestValue151985136"
        };

        // Act
        this._testClass.Dictionaries = testValue;

        // Assert
        this._testClass.Dictionaries.ShouldBeSameAs(testValue);
    }
}