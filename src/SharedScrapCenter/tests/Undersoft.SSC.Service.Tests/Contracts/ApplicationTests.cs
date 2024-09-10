using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Base;
using Undersoft.SSC.Service.Contracts.Locations;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Application"/>.
/// </summary>
[TestClass]
public class ApplicationTests
{
    private Application _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Application"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Application();
    }

    /// <summary>
    /// Checks that setting the RelatedFrom property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedFrom()
    {
        this._testClass.CheckProperty(x => x.RelatedFrom, new ObjectSet<ApplicationBase>(), new ObjectSet<ApplicationBase>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new ObjectSet<ApplicationBase>(), new ObjectSet<ApplicationBase>());
    }

    /// <summary>
    /// Checks that setting the Services property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServices()
    {
        this._testClass.CheckProperty(x => x.Services, new ObjectSet<ServiceBase>(), new ObjectSet<ServiceBase>());
    }

    /// <summary>
    /// Checks that setting the Default property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDefault()
    {
        this._testClass.CheckProperty(x => x.Default, new Default { Settings = new SettingSet<Setting>() }, new Default { Settings = new SettingSet<Setting>() });
    }

    /// <summary>
    /// Checks that setting the Location property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocation()
    {
        this._testClass.CheckProperty(x => x.Location, new Location
        {
            Name = "TestValue1006709420",
            LocaleType = LocaleType.Private,
            Email = "TestValue1060739688",
            PhoneType = PhoneType.Main,
            PhoneNumber = "TestValue372898143",
            Notices = "TestValue480477024",
            Website = "TestValue1764885663",
            SocialMedia = "TestValue753128331",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        }, new Location
        {
            Name = "TestValue826995339",
            LocaleType = LocaleType.Main,
            Email = "TestValue1208264625",
            PhoneType = PhoneType.Personal,
            PhoneNumber = "TestValue1152360381",
            Notices = "TestValue552840419",
            Website = "TestValue891642642",
            SocialMedia = "TestValue1617757871",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        });
    }
}