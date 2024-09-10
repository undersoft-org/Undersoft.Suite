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
/// Unit tests for the type <see cref="Resource"/>.
/// </summary>
[TestClass]
public class ResourceTests
{
    private Resource _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Resource"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Resource();
    }

    /// <summary>
    /// Checks that setting the RelatedFrom property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedFrom()
    {
        this._testClass.CheckProperty(x => x.RelatedFrom, new ObjectSet<ResourceBase>(), new ObjectSet<ResourceBase>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new ObjectSet<ResourceBase>(), new ObjectSet<ResourceBase>());
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
            Name = "TestValue1877380488",
            LocaleType = LocaleType.Additional,
            Email = "TestValue1013804139",
            PhoneType = PhoneType.Main,
            PhoneNumber = "TestValue148856903",
            Notices = "TestValue1668239497",
            Website = "TestValue158359342",
            SocialMedia = "TestValue1043769132",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        }, new Location
        {
            Name = "TestValue904434999",
            LocaleType = LocaleType.Additional,
            Email = "TestValue1475790165",
            PhoneType = PhoneType.Personal,
            PhoneNumber = "TestValue1330674884",
            Notices = "TestValue331903903",
            Website = "TestValue2039084874",
            SocialMedia = "TestValue588680079",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        });
    }
}