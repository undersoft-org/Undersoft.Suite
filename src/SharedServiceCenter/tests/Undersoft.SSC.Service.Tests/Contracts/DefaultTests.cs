using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Default"/>.
/// </summary>
[TestClass]
public class DefaultTests
{
    private Default _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Default"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Default();
    }

    /// <summary>
    /// Checks that setting the Settings property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSettings()
    {
        this._testClass.CheckProperty(x => x.Settings, new SettingSet<Setting>(), new SettingSet<Setting>());
    }
}