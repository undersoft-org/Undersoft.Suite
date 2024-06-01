using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Settings;

namespace Undersoft.SSC.Service.Tests.Contracts.Settings;

/// <summary>
/// Unit tests for the type <see cref="ProfileSetting"/>.
/// </summary>
[TestClass]
public class ProfileSettingTests
{
    private ProfileSetting _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ProfileSetting"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ProfileSetting();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ProfileSetting();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that setting the AvatarPath property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAvatarPath()
    {
        this._testClass.CheckProperty(x => x.AvatarPath);
    }

    /// <summary>
    /// Checks that setting the BoardPath property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBoardPath()
    {
        this._testClass.CheckProperty(x => x.BoardPath);
    }

    /// <summary>
    /// Checks that setting the LogoPath property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLogoPath()
    {
        this._testClass.CheckProperty(x => x.LogoPath);
    }

    /// <summary>
    /// Checks that setting the ProfilePath property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProfilePath()
    {
        this._testClass.CheckProperty(x => x.ProfilePath);
    }
}