using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Remote;
using Undersoft.SSC.Domain.Entities;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Domain.Entities.Locations;

namespace Undersoft.SSC.Tests.Domain.Entities;

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
        this._testClass.CheckProperty(x => x.RelatedFrom, new EntitySet<Resource>(), new EntitySet<Resource>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new EntitySet<Resource>(), new EntitySet<Resource>());
    }

    /// <summary>
    /// Checks that setting the Activities property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetActivities()
    {
        this._testClass.CheckProperty(x => x.Activities, new EntitySet<Activity>(), new EntitySet<Activity>());
    }

    /// <summary>
    /// Checks that setting the Members property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMembers()
    {
        this._testClass.CheckProperty(x => x.Members, new EntitySet<Member>(), new EntitySet<Member>());
    }

    /// <summary>
    /// Checks that setting the Schedules property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSchedules()
    {
        this._testClass.CheckProperty(x => x.Schedules, new EntitySet<Schedule>(), new EntitySet<Schedule>());
    }

    /// <summary>
    /// Checks that setting the DefaultId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDefaultId()
    {
        this._testClass.CheckProperty(x => x.DefaultId, 1825361322L, 289441195L);
    }

    /// <summary>
    /// Checks that setting the Default property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDefault()
    {
        this._testClass.CheckProperty(x => x.Default, new Default
        {
            Activities = new EntitySet<Activity>(),
            Members = new EntitySet<Member>(),
            Resources = new EntitySet<Resource>(),
            Schedules = new EntitySet<Schedule>(),
            Applications = new EntitySet<Application>(),
            Services = new EntitySet<Service>(),
            Details = new EntitySet<Detail>(),
            Settings = new EntitySet<Setting>()
        }, new Default
        {
            Activities = new EntitySet<Activity>(),
            Members = new EntitySet<Member>(),
            Resources = new EntitySet<Resource>(),
            Schedules = new EntitySet<Schedule>(),
            Applications = new EntitySet<Application>(),
            Services = new EntitySet<Service>(),
            Details = new EntitySet<Detail>(),
            Settings = new EntitySet<Setting>()
        });
    }

    /// <summary>
    /// Checks that setting the LocationId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocationId()
    {
        this._testClass.CheckProperty(x => x.LocationId, 1942294867L, 1391999816L);
    }

    /// <summary>
    /// Checks that setting the Location property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocation()
    {
        this._testClass.CheckProperty(x => x.Location, new Location
        {
            Name = "TestValue38689971",
            LocaleType = LocaleType.Additional,
            Email = "TestValue836101262",
            PhoneType = PhoneType.Fax,
            PhoneNumber = "TestValue107632335",
            Notices = "TestValue783055211",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 964432844L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1422777019L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 33950438L,
                Location = default(Location)
            },
            ActivityId = 1744532542L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 2121799618L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 699342539L,
                Location = default(Location)
            },
            ResourceId = 1399466331L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 207476526L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1809268207L,
                Location = default(Location)
            },
            ScheduleId = 11080247L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 1572354489L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1662875445L,
                Location = default(Location)
            },
            ServiceId = 728832316L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1430652740L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1641779843L,
                Location = default(Location)
            },
            ApplicationId = 1794127920L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1715041535L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1809360751L,
                Location = default(Location)
            }
        }, new Location
        {
            Name = "TestValue1870612553",
            LocaleType = LocaleType.Main,
            Email = "TestValue76424783",
            PhoneType = PhoneType.Main,
            PhoneNumber = "TestValue1858212572",
            Notices = "TestValue773767379",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 1124099407L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 192242991L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1562684928L,
                Location = default(Location)
            },
            ActivityId = 1070870126L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1590680851L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1741124930L,
                Location = default(Location)
            },
            ResourceId = 1165587459L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 539209988L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1189883634L,
                Location = default(Location)
            },
            ScheduleId = 1931587913L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 882546136L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1152683256L,
                Location = default(Location)
            },
            ServiceId = 123514691L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1548238820L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 1197344848L,
                Location = default(Location)
            },
            ApplicationId = 191019041L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1158583171L,
                Default = new Default
                {
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Details = new EntitySet<Detail>(),
                    Settings = new EntitySet<Setting>()
                },
                LocationId = 512328617L,
                Location = default(Location)
            }
        });
    }
}