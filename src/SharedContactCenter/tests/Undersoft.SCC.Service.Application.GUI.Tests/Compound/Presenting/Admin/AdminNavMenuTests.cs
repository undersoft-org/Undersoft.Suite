using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Admin;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting.Admin;

/// <summary>
/// Unit tests for the type <see cref="AdminNavMenu"/>.
/// </summary>
[TestClass]
public class AdminNavMenuTests
{
    private AdminNavMenu _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AdminNavMenu"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AdminNavMenu();
    }

    /// <summary>
    /// Checks that the Contacts property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContacts()
    {
        // Arrange
        var testValue = new AdminContacts();

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
        var testValue = new AdminDictionaries
        {
            Groups = "TestValue182756895",
            Countries = "TestValue196589908"
        };

        // Act
        this._testClass.Dictionaries = testValue;

        // Assert
        this._testClass.Dictionaries.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Administration property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAdministration()
    {
        // Arrange
        var testValue = new AdminAdministration
        {
            Accounts = "TestValue646128676",
            Events = "TestValue1254610175"
        };

        // Act
        this._testClass.Administration = testValue;

        // Assert
        this._testClass.Administration.ShouldBeSameAs(testValue);
    }
}