using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Setting"/>.
/// </summary>
[TestClass]
public class SettingTests
{
    private Setting _testClass;
    private SettingKind _kind;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Setting"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._kind = SettingKind.Licence;
        this._testClass = new Setting(this._kind);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Setting();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Setting(this._kind);

        // Assert
        instance.ShouldNotBeNull();
    }
}