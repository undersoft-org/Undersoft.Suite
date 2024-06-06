using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Undersoft.SCC.Service.Application.GUI.Compound.Access.Dialog;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using TDialog = Microsoft.FluentUI.AspNetCore.Components.IDialogContentComponent<Undersoft.SDK.Service.Application.GUI.View.Abstraction.IViewData<Undersoft.SCC.Service.Application.ViewModels.Contact>>;
using TModel = Undersoft.SCC.Service.Application.ViewModels.Contact;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Access.Dialog;

/// <summary>
/// Unit tests for the type <see cref="AccessDialog"/>.
/// </summary>
[TestClass]
public class AccessDialog_2Tests
{
    private AccessDialog<TDialog, TModel> _testClass;
    private IDialogService _dialogService;
    private IJSRuntime _jS;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccessDialog"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._dialogService = Substitute.For<IDialogService>();
        this._jS = Substitute.For<IJSRuntime>();
        this._testClass = new AccessDialog<TDialog, TModel>(this._dialogService, this._jS);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccessDialog<TDialog, TModel>(this._dialogService, this._jS);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the dialogService parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullDialogService()
    {
        Should.Throw<ArgumentNullException>(() => new AccessDialog<TDialog, TModel>(default(IDialogService), this._jS));
    }

    /// <summary>
    /// Checks that instance construction throws when the jS parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullJS()
    {
        Should.Throw<ArgumentNullException>(() => new AccessDialog<TDialog, TModel>(this._dialogService, default(IJSRuntime)));
    }

    /// <summary>
    /// Checks that the Show method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallShowAsync()
    {
        // Arrange
        var data = Substitute.For<IViewData<TModel>>();

        // Act
        await this._testClass.Show(data);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Show method throws when the data parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallShowWithNullDataAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Show(default(IViewData<TModel>)));
    }

    /// <summary>
    /// Checks that the JS property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void JSIsInitializedCorrectly()
    {
        this._testClass.JS.ShouldBeSameAs(this._jS);
    }
}