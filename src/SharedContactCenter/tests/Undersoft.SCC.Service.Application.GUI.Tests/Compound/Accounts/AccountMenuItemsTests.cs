using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Accounts;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountMenuItems"/>.
/// </summary>
[TestClass]
public class AccountMenuItemsTests
{
    private AccountMenuItems _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountMenuItems"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountMenuItems();
    }

    /// <summary>
    /// Checks that the Account property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccount()
    {
        // Arrange
        var testValue = Substitute.For<IViewPanel<Account>>();

        // Act
        this._testClass.Account = testValue;

        // Assert
        this._testClass.Account.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the SignUp property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSignUp()
    {
        // Arrange
        var testValue = "TestValue81181257";

        // Act
        this._testClass.SignUp = testValue;

        // Assert
        this._testClass.SignUp.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the SignIn property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSignIn()
    {
        // Arrange
        var testValue = "TestValue1080099202";

        // Act
        this._testClass.SignIn = testValue;

        // Assert
        this._testClass.SignIn.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the SignOut property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSignOut()
    {
        // Arrange
        var testValue = "TestValue1865599214";

        // Act
        this._testClass.SignOut = testValue;

        // Assert
        this._testClass.SignOut.ShouldBe(testValue);
    }
}