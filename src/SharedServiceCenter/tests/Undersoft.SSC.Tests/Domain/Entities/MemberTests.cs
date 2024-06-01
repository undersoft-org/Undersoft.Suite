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
        this._testClass.CheckProperty(x => x.RelatedFrom, new EntitySet<Member>(), new EntitySet<Member>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new EntitySet<Member>(), new EntitySet<Member>());
    }

    /// <summary>
    /// Checks that setting the Applications property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetApplications()
    {
        this._testClass.CheckProperty(x => x.Applications, new EntitySet<Application>(), new EntitySet<Application>());
    }

    /// <summary>
    /// Checks that setting the Services property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServices()
    {
        this._testClass.CheckProperty(x => x.Services, new EntitySet<Service>(), new EntitySet<Service>());
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
    /// Checks that setting the Resources property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetResources()
    {
        this._testClass.CheckProperty(x => x.Resources, new EntitySet<Resource>(), new EntitySet<Resource>());
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
        this._testClass.CheckProperty(x => x.DefaultId, 1983354437L, 1729770199L);
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
        this._testClass.CheckProperty(x => x.LocationId, 1398865190L, 637200262L);
    }

    /// <summary>
    /// Checks that setting the Location property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocation()
    {
        this._testClass.CheckProperty(x => x.Location, new Location
        {
            Name = "TestValue376813821",
            LocaleType = LocaleType.Additional,
            Email = "TestValue1695654645",
            PhoneType = PhoneType.Fax,
            PhoneNumber = "TestValue375633684",
            Notices = "TestValue44326834",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 1197027914L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1161533896L,
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
                LocationId = 507655468L,
                Location = default(Location)
            },
            ActivityId = 1389129580L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 225999125L,
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
                LocationId = 727221437L,
                Location = default(Location)
            },
            ResourceId = 1294952761L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 600884061L,
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
                LocationId = 1788444583L,
                Location = default(Location)
            },
            ScheduleId = 202469803L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 109675649L,
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
                LocationId = 313715252L,
                Location = default(Location)
            },
            ServiceId = 1413207106L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 634583844L,
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
                LocationId = 1205398771L,
                Location = default(Location)
            },
            ApplicationId = 605573166L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 185392620L,
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
                LocationId = 1454759373L,
                Location = default(Location)
            }
        }, new Location
        {
            Name = "TestValue1292762525",
            LocaleType = LocaleType.Additional,
            Email = "TestValue134971794",
            PhoneType = PhoneType.Fax,
            PhoneNumber = "TestValue1472170195",
            Notices = "TestValue1652809928",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 1523844259L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 558845688L,
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
                LocationId = 2061797316L,
                Location = default(Location)
            },
            ActivityId = 2146444615L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1618219635L,
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
                LocationId = 1866171759L,
                Location = default(Location)
            },
            ResourceId = 398360401L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1544313692L,
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
                LocationId = 1627660976L,
                Location = default(Location)
            },
            ScheduleId = 799716620L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 2125418481L,
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
                LocationId = 409168844L,
                Location = default(Location)
            },
            ServiceId = 1798765355L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 169974508L,
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
                LocationId = 1247814539L,
                Location = default(Location)
            },
            ApplicationId = 490785443L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1585432988L,
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
                LocationId = 924959293L,
                Location = default(Location)
            }
        });
    }
}