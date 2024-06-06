using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Admin;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting.Admin;

/// <summary>
/// Unit tests for the type <see cref="AdminAdministration"/>.
/// </summary>
[TestClass]
public class AdminAdministrationTests
{
    private AdminAdministration _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AdminAdministration"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AdminAdministration();
    }

    /// <summary>
    /// Checks that the Accounts property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccounts()
    {
        // Arrange
        var testValue = "TestValue1552999790";

        // Act
        this._testClass.Accounts = testValue;

        // Assert
        this._testClass.Accounts.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Events property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEvents()
    {
        // Arrange
        var testValue = "TestValue1587669512";

        // Act
        this._testClass.Events = testValue;

        // Assert
        this._testClass.Events.ShouldBe(testValue);
    }
}