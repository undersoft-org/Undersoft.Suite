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
/// Unit tests for the type <see cref="Schedule"/>.
/// </summary>
[TestClass]
public class ScheduleTests
{
    private Schedule _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Schedule"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Schedule();
    }

    /// <summary>
    /// Checks that setting the RelatedFrom property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedFrom()
    {
        this._testClass.CheckProperty(x => x.RelatedFrom, new ObjectSet<ScheduleBase>(), new ObjectSet<ScheduleBase>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new ObjectSet<ScheduleBase>(), new ObjectSet<ScheduleBase>());
    }

    /// <summary>
    /// Checks that setting the Members property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMembers()
    {
        this._testClass.CheckProperty(x => x.Members, new ObjectSet<MemberBase>(), new ObjectSet<MemberBase>());
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
            Name = "TestValue507386043",
            LocaleType = LocaleType.Main,
            Email = "TestValue1800655582",
            PhoneType = PhoneType.Main,
            PhoneNumber = "TestValue1530669232",
            Notices = "TestValue614249565",
            Website = "TestValue907815727",
            SocialMedia = "TestValue842136326",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        }, new Location
        {
            Name = "TestValue109647454",
            LocaleType = LocaleType.Additional,
            Email = "TestValue1976802148",
            PhoneType = PhoneType.Personal,
            PhoneNumber = "TestValue411296371",
            Notices = "TestValue177319680",
            Website = "TestValue194678564",
            SocialMedia = "TestValue1161388043",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        });
    }
}