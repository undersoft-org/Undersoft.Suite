using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Accounts;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountMenu"/>.
/// </summary>
[TestClass]
public class AccountMenuTests
{
    private AccountMenu _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountMenu"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountMenu();
    }

    /// <summary>
    /// Checks that the Account property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccount()
    {
        // Arrange
        var testValue = new AccountMenuItems
        {
            Account = Substitute.For<IViewPanel<Account>>(),
            SignUp = "TestValue375221683",
            SignIn = "TestValue1553466670",
            SignOut = "TestValue386157913"
        };

        // Act
        this._testClass.Account = testValue;

        // Assert
        this._testClass.Account.ShouldBeSameAs(testValue);
    }
}