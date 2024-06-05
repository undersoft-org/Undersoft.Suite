using System;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Access;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Access;

/// <summary>
/// Unit tests for the type <see cref="ConfirmPasswordResetBase"/>.
/// </summary>
[TestClass]
public partial class ConfirmPasswordResetBaseTests
{
    private ConfirmPasswordResetBase _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ConfirmPasswordResetBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ConfirmPasswordResetBase();
    }

    /// <summary>
    /// Checks that the DialogService property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDialogService()
    {
        // Arrange
        var testValue = Substitute.For<IDialogService>();

        // Act
        this._testClass.DialogService = testValue;

        // Assert
        this._testClass.DialogService.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the Email property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        // Arrange
        var testValue = "TestValue1216690859";

        // Act
        this._testClass.Email = testValue;

        // Assert
        this._testClass.Email.ShouldBe(testValue);
    }
}