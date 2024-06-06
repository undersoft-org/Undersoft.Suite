using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Undersoft.SCC.Service.Application.GUI.Compound.Access;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Access;

/// <summary>
/// Unit tests for the type <see cref="AccountPanel"/>.
/// </summary>
[TestClass]
public class AccountPanelTests
{
    private AccountPanel _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountPanel"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountPanel();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountPanel();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that the Open method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallOpenAsync()
    {
        // Arrange
        var _panel = Substitute.For<IViewPanel<Account>>();

        // Act
        await this._testClass.Open(_panel);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Open method throws when the _panel parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallOpenWithNull_panelAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Open(default(IViewPanel<Account>)));
    }

    /// <summary>
    /// Checks that the HandlePanel method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallHandlePanel()
    {
        // Arrange
        var result = Substitute.For<IViewData<Account>>();

        // Act
        this._testClass.HandlePanel(result);

        // Assert
        Assert.Fail("Create or modify test");
    }
}