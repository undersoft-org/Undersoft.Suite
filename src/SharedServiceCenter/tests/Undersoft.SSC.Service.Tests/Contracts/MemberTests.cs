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
/// Unit tests for the type <see cref="Member"/>.
/// </summary>
[TestClass]
public class MemberTests
{
    private Member _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Member"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Member();
    }

    /// <summary>
    /// Checks that setting the RelatedFrom property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedFrom()
    {
        this._testClass.CheckProperty(x => x.RelatedFrom, new ObjectSet<MemberBase>(), new ObjectSet<MemberBase>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new ObjectSet<MemberBase>(), new ObjectSet<MemberBase>());
    }

    /// <summary>
    /// Checks that setting the Activities property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetActivities()
    {
        this._testClass.CheckProperty(x => x.Activities, new ObjectSet<ActivityBase>(), new ObjectSet<ActivityBase>());
    }

    /// <summary>
    /// Checks that setting the Resources property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetResources()
    {
        this._testClass.CheckProperty(x => x.Resources, new ObjectSet<ResourceBase>(), new ObjectSet<ResourceBase>());
    }

    /// <summary>
    /// Checks that setting the Schedules property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSchedules()
    {
        this._testClass.CheckProperty(x => x.Schedules, new ObjectSet<ScheduleBase>(), new ObjectSet<ScheduleBase>());
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
            Name = "TestValue72368909",
            LocaleType = LocaleType.Additional,
            Email = "TestValue1708663552",
            PhoneType = PhoneType.Bussines,
            PhoneNumber = "TestValue1729079053",
            Notices = "TestValue654471633",
            Website = "TestValue989772172",
            SocialMedia = "TestValue1563910624",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        }, new Location
        {
            Name = "TestValue1685218924",
            LocaleType = LocaleType.Bussines,
            Email = "TestValue1637527801",
            PhoneType = PhoneType.Bussines,
            PhoneNumber = "TestValue2070746570",
            Notices = "TestValue396989724",
            Website = "TestValue1069257135",
            SocialMedia = "TestValue1056672630",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        });
    }
}