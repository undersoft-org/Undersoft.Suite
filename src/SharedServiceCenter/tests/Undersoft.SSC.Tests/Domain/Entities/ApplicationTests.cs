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
        this._testClass.CheckProperty(x => x.RelatedFrom, new EntitySet<Application>(), new EntitySet<Application>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new EntitySet<Application>(), new EntitySet<Application>());
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
    /// Checks that setting the Services property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServices()
    {
        this._testClass.CheckProperty(x => x.Services, new RemoteSet<Service>(), new RemoteSet<Service>());
    }

    /// <summary>
    /// Checks that setting the ServicesToApplications property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServicesToApplications()
    {
        this._testClass.CheckProperty(x => x.ServicesToApplications, new RemoteSet<RemoteLink<Service, Application>>(), new RemoteSet<RemoteLink<Service, Application>>());
    }

    /// <summary>
    /// Checks that setting the DefaultId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDefaultId()
    {
        this._testClass.CheckProperty(x => x.DefaultId, 1053790891L, 2110924185L);
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
        this._testClass.CheckProperty(x => x.LocationId, 894847229L, 498803381L);
    }

    /// <summary>
    /// Checks that setting the Location property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocation()
    {
        this._testClass.CheckProperty(x => x.Location, new Location
        {
            Name = "TestValue828832909",
            LocaleType = LocaleType.Additional,
            Email = "TestValue1711670853",
            PhoneType = PhoneType.Main,
            PhoneNumber = "TestValue112707259",
            Notices = "TestValue420584530",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 1936111621L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 764577548L,
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
                LocationId = 1122397397L,
                Location = default(Location)
            },
            ActivityId = 1575502412L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 103187391L,
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
                LocationId = 767563031L,
                Location = default(Location)
            },
            ResourceId = 229790907L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 471399400L,
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
                LocationId = 303943380L,
                Location = default(Location)
            },
            ScheduleId = 10470811L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 1094304184L,
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
                LocationId = 667015959L,
                Location = default(Location)
            },
            ServiceId = 1467737739L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 970016535L,
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
                LocationId = 998298131L,
                Location = default(Location)
            },
            ApplicationId = 1449235515L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1102957676L,
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
                LocationId = 1429330948L,
                Location = default(Location)
            }
        }, new Location
        {
            Name = "TestValue1812642128",
            LocaleType = LocaleType.Bussines,
            Email = "TestValue720336387",
            PhoneType = PhoneType.Personal,
            PhoneNumber = "TestValue1778967155",
            Notices = "TestValue1168550142",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 1923797967L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1010663682L,
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
                LocationId = 1653235733L,
                Location = default(Location)
            },
            ActivityId = 930350545L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 850106853L,
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
                LocationId = 1015627284L,
                Location = default(Location)
            },
            ResourceId = 1548397541L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1443641468L,
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
                LocationId = 1700767279L,
                Location = default(Location)
            },
            ScheduleId = 1261361860L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 2095404381L,
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
                LocationId = 788169356L,
                Location = default(Location)
            },
            ServiceId = 1777233580L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 54882L,
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
                LocationId = 1085196287L,
                Location = default(Location)
            },
            ApplicationId = 1259040436L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1270895555L,
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
                LocationId = 2134314495L,
                Location = default(Location)
            }
        });
    }
}