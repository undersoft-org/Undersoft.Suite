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
/// Unit tests for the type <see cref="Activity"/>.
/// </summary>
[TestClass]
public class ActivityTests
{
    private Activity _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Activity"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Activity();
    }

    /// <summary>
    /// Checks that setting the RelatedFrom property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedFrom()
    {
        this._testClass.CheckProperty(x => x.RelatedFrom, new ObjectSet<ActivityBase>(), new ObjectSet<ActivityBase>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new ObjectSet<ActivityBase>(), new ObjectSet<ActivityBase>());
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
            Name = "TestValue935720152",
            LocaleType = LocaleType.Additional,
            Email = "TestValue1357589132",
            PhoneType = PhoneType.Bussines,
            PhoneNumber = "TestValue1489632506",
            Notices = "TestValue21534943",
            Website = "TestValue1101177804",
            SocialMedia = "TestValue349862581",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        }, new Location
        {
            Name = "TestValue1590088093",
            LocaleType = LocaleType.Main,
            Email = "TestValue321348927",
            PhoneType = PhoneType.Personal,
            PhoneNumber = "TestValue94451137",
            Notices = "TestValue1622951225",
            Website = "TestValue1773670007",
            SocialMedia = "TestValue1389286613",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        });
    }
}