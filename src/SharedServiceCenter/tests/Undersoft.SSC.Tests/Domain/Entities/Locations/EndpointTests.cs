using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Remote;
using Undersoft.SSC.Domain.Entities;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Domain.Entities.Locations;

namespace Undersoft.SSC.Tests.Domain.Entities.Locations;

/// <summary>
/// Unit tests for the type <see cref="Endpoint"/>.
/// </summary>
[TestClass]
public class EndpointTests
{
    private Endpoint _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Endpoint"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Endpoint();
    }

    /// <summary>
    /// Checks that setting the Host property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetHost()
    {
        this._testClass.CheckProperty(x => x.Host);
    }

    /// <summary>
    /// Checks that setting the IP property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIP()
    {
        this._testClass.CheckProperty(x => x.IP);
    }

    /// <summary>
    /// Checks that setting the Port property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPort()
    {
        this._testClass.CheckProperty(x => x.Port, 1526086685, 371180777);
    }

    /// <summary>
    /// Checks that setting the URI property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetURI()
    {
        this._testClass.CheckProperty(x => x.URI);
    }

    /// <summary>
    /// Checks that setting the OS property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetOS()
    {
        this._testClass.CheckProperty(x => x.OS);
    }

    /// <summary>
    /// Checks that setting the Protocol property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetProtocol()
    {
        this._testClass.CheckProperty(x => x.Protocol);
    }

    /// <summary>
    /// Checks that setting the Method property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMethod()
    {
        this._testClass.CheckProperty(x => x.Method);
    }

    /// <summary>
    /// Checks that setting the Parameters property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetParameters()
    {
        this._testClass.CheckProperty(x => x.Parameters, new[] { "TestValue813214145", "TestValue1768041978", "TestValue1271952807" }, new[] { "TestValue1488273494", "TestValue676516113", "TestValue1202878770" });
    }

    /// <summary>
    /// Checks that setting the Return property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetReturn()
    {
        this._testClass.CheckProperty(x => x.Return);
    }

    /// <summary>
    /// Checks that setting the LocationId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocationId()
    {
        this._testClass.CheckProperty(x => x.LocationId, 1340791250L, 785653616L);
    }

    /// <summary>
    /// Checks that setting the Location property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocation()
    {
        this._testClass.CheckProperty(x => x.Location, new Location
        {
            Name = "TestValue1795103852",
            LocaleType = LocaleType.Bussines,
            Email = "TestValue1507588553",
            PhoneType = PhoneType.Personal,
            PhoneNumber = "TestValue685077639",
            Notices = "TestValue623352114",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 1411856163L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 2003436432L,
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
                LocationId = 105845649L,
                Location = default(Location)
            },
            ActivityId = 1339009477L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1618867962L,
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
                LocationId = 2048145448L,
                Location = default(Location)
            },
            ResourceId = 828966137L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 26495463L,
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
                LocationId = 1346150737L,
                Location = default(Location)
            },
            ScheduleId = 1471188727L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 836573783L,
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
                LocationId = 1980429545L,
                Location = default(Location)
            },
            ServiceId = 920893617L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1996407848L,
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
                LocationId = 1811671846L,
                Location = default(Location)
            },
            ApplicationId = 1060215491L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1893484561L,
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
                LocationId = 1117051949L,
                Location = default(Location)
            }
        }, new Location
        {
            Name = "TestValue1348504160",
            LocaleType = LocaleType.Additional,
            Email = "TestValue1870683887",
            PhoneType = PhoneType.Fax,
            PhoneNumber = "TestValue1130673724",
            Notices = "TestValue1851900670",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 982072033L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1362495505L,
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
                LocationId = 1566946800L,
                Location = default(Location)
            },
            ActivityId = 150425990L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 601973552L,
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
                LocationId = 254113231L,
                Location = default(Location)
            },
            ResourceId = 1766045339L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 2128694893L,
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
                LocationId = 1844062097L,
                Location = default(Location)
            },
            ScheduleId = 1090715067L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 1189252772L,
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
                LocationId = 300347351L,
                Location = default(Location)
            },
            ServiceId = 61553294L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1895922635L,
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
                LocationId = 497174816L,
                Location = default(Location)
            },
            ApplicationId = 1625424884L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1689698287L,
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
                LocationId = 786718682L,
                Location = default(Location)
            }
        });
    }
}