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
/// Unit tests for the type <see cref="Location"/>.
/// </summary>
[TestClass]
public class LocationTests
{
    private Location _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Location"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Location();
    }

    /// <summary>
    /// Checks that setting the Name property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        this._testClass.CheckProperty(x => x.Name);
    }

    /// <summary>
    /// Checks that setting the LocaleType property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocaleType()
    {
        this._testClass.CheckProperty(x => x.LocaleType, LocaleType.Additional, LocaleType.Additional);
    }

    /// <summary>
    /// Checks that setting the Email property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        this._testClass.CheckProperty(x => x.Email);
    }

    /// <summary>
    /// Checks that setting the PhoneType property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneType()
    {
        this._testClass.CheckProperty(x => x.PhoneType, PhoneType.Bussines, PhoneType.Main);
    }

    /// <summary>
    /// Checks that setting the PhoneNumber property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumber()
    {
        this._testClass.CheckProperty(x => x.PhoneNumber);
    }

    /// <summary>
    /// Checks that setting the Notices property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNotices()
    {
        this._testClass.CheckProperty(x => x.Notices);
    }

    /// <summary>
    /// Checks that setting the Endpoints property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEndpoints()
    {
        this._testClass.CheckProperty(x => x.Endpoints, new EntitySet<Endpoint>(), new EntitySet<Endpoint>());
    }

    /// <summary>
    /// Checks that setting the Positions property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPositions()
    {
        this._testClass.CheckProperty(x => x.Positions, new EntitySet<Position>(), new EntitySet<Position>());
    }

    /// <summary>
    /// Checks that setting the MemberId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMemberId()
    {
        this._testClass.CheckProperty(x => x.MemberId, 537496751L, 755291448L);
    }

    /// <summary>
    /// Checks that setting the Member property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMember()
    {
        this._testClass.CheckProperty(x => x.Member, new Member
        {
            RelatedFrom = new EntitySet<Member>(),
            RelatedTo = new EntitySet<Member>(),
            Applications = new EntitySet<Application>(),
            Services = new EntitySet<Service>(),
            Activities = new EntitySet<Activity>(),
            Resources = new EntitySet<Resource>(),
            Schedules = new EntitySet<Schedule>(),
            DefaultId = 1192176030L,
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
            LocationId = 1433090415L,
            Location = new Location
            {
                Name = "TestValue1092437115",
                LocaleType = LocaleType.Private,
                Email = "TestValue147408740",
                PhoneType = PhoneType.Personal,
                PhoneNumber = "TestValue922482227",
                Notices = "TestValue1026331382",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 1933263127L,
                Member = default(Member),
                ActivityId = 723086969L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1107331124L,
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
                    LocationId = 359703294L,
                    Location = default(Location)
                },
                ResourceId = 1225755654L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1514324021L,
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
                    LocationId = 1806433282L,
                    Location = default(Location)
                },
                ScheduleId = 1706454173L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 377220703L,
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
                    LocationId = 687768156L,
                    Location = default(Location)
                },
                ServiceId = 1980540238L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 142254370L,
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
                    LocationId = 773402489L,
                    Location = default(Location)
                },
                ApplicationId = 176983765L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 506105535L,
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
                    LocationId = 1267769584L,
                    Location = default(Location)
                }
            }
        }, new Member
        {
            RelatedFrom = new EntitySet<Member>(),
            RelatedTo = new EntitySet<Member>(),
            Applications = new EntitySet<Application>(),
            Services = new EntitySet<Service>(),
            Activities = new EntitySet<Activity>(),
            Resources = new EntitySet<Resource>(),
            Schedules = new EntitySet<Schedule>(),
            DefaultId = 2040860169L,
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
            LocationId = 924642181L,
            Location = new Location
            {
                Name = "TestValue745409343",
                LocaleType = LocaleType.Additional,
                Email = "TestValue1914327023",
                PhoneType = PhoneType.Bussines,
                PhoneNumber = "TestValue1108884786",
                Notices = "TestValue304948112",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 976045861L,
                Member = default(Member),
                ActivityId = 1078460777L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 2128993827L,
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
                    LocationId = 1466533965L,
                    Location = default(Location)
                },
                ResourceId = 1848164368L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 848774437L,
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
                    LocationId = 251533740L,
                    Location = default(Location)
                },
                ScheduleId = 1306687950L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 708494187L,
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
                    LocationId = 617371046L,
                    Location = default(Location)
                },
                ServiceId = 2134867824L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1132369688L,
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
                    LocationId = 385779945L,
                    Location = default(Location)
                },
                ApplicationId = 1304699225L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 714959333L,
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
                    LocationId = 303475091L,
                    Location = default(Location)
                }
            }
        });
    }

    /// <summary>
    /// Checks that setting the ActivityId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetActivityId()
    {
        this._testClass.CheckProperty(x => x.ActivityId, 1511488894L, 113234311L);
    }

    /// <summary>
    /// Checks that setting the Activity property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetActivity()
    {
        this._testClass.CheckProperty(x => x.Activity, new Activity
        {
            RelatedFrom = new EntitySet<Activity>(),
            RelatedTo = new EntitySet<Activity>(),
            Members = new EntitySet<Member>(),
            Resources = new EntitySet<Resource>(),
            Schedules = new EntitySet<Schedule>(),
            DefaultId = 1591618319L,
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
            LocationId = 800981210L,
            Location = new Location
            {
                Name = "TestValue270265042",
                LocaleType = LocaleType.Main,
                Email = "TestValue2129372606",
                PhoneType = PhoneType.Personal,
                PhoneNumber = "TestValue926984880",
                Notices = "TestValue1972151178",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 884263408L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1370250206L,
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
                    LocationId = 69983291L,
                    Location = default(Location)
                },
                ActivityId = 1371626287L,
                Activity = default(Activity),
                ResourceId = 1259488006L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 572193323L,
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
                    LocationId = 1761685830L,
                    Location = default(Location)
                },
                ScheduleId = 802383012L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 1531141080L,
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
                    LocationId = 147294877L,
                    Location = default(Location)
                },
                ServiceId = 1532813841L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 339899317L,
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
                    LocationId = 2005773452L,
                    Location = default(Location)
                },
                ApplicationId = 1675929913L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 436234416L,
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
                    LocationId = 673852288L,
                    Location = default(Location)
                }
            }
        }, new Activity
        {
            RelatedFrom = new EntitySet<Activity>(),
            RelatedTo = new EntitySet<Activity>(),
            Members = new EntitySet<Member>(),
            Resources = new EntitySet<Resource>(),
            Schedules = new EntitySet<Schedule>(),
            DefaultId = 1581243830L,
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
            LocationId = 156031443L,
            Location = new Location
            {
                Name = "TestValue189599588",
                LocaleType = LocaleType.Bussines,
                Email = "TestValue881989639",
                PhoneType = PhoneType.Personal,
                PhoneNumber = "TestValue209682847",
                Notices = "TestValue441934252",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 186850693L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1801092712L,
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
                    LocationId = 1916935132L,
                    Location = default(Location)
                },
                ActivityId = 307903576L,
                Activity = default(Activity),
                ResourceId = 34683070L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 580803480L,
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
                    LocationId = 1096571818L,
                    Location = default(Location)
                },
                ScheduleId = 1113801562L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 539549085L,
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
                    LocationId = 2023496837L,
                    Location = default(Location)
                },
                ServiceId = 2111994676L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1028767181L,
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
                    LocationId = 1236704659L,
                    Location = default(Location)
                },
                ApplicationId = 1484351547L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1505366687L,
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
                    LocationId = 1562674501L,
                    Location = default(Location)
                }
            }
        });
    }

    /// <summary>
    /// Checks that setting the ResourceId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetResourceId()
    {
        this._testClass.CheckProperty(x => x.ResourceId, 1518167505L, 1730880580L);
    }

    /// <summary>
    /// Checks that setting the Resource property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetResource()
    {
        this._testClass.CheckProperty(x => x.Resource, new Resource
        {
            RelatedFrom = new EntitySet<Resource>(),
            RelatedTo = new EntitySet<Resource>(),
            Activities = new EntitySet<Activity>(),
            Members = new EntitySet<Member>(),
            Schedules = new EntitySet<Schedule>(),
            DefaultId = 1921041792L,
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
            LocationId = 567664456L,
            Location = new Location
            {
                Name = "TestValue918144897",
                LocaleType = LocaleType.Bussines,
                Email = "TestValue254944506",
                PhoneType = PhoneType.Fax,
                PhoneNumber = "TestValue364746794",
                Notices = "TestValue1743896401",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 961482198L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1973341163L,
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
                    LocationId = 825592677L,
                    Location = default(Location)
                },
                ActivityId = 1553249033L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1090161539L,
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
                    LocationId = 148102464L,
                    Location = default(Location)
                },
                ResourceId = 1160567358L,
                Resource = default(Resource),
                ScheduleId = 1775532686L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 1184775594L,
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
                    LocationId = 1605878941L,
                    Location = default(Location)
                },
                ServiceId = 802741838L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1453782254L,
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
                    LocationId = 767699942L,
                    Location = default(Location)
                },
                ApplicationId = 950337600L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1198206706L,
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
                    LocationId = 419012279L,
                    Location = default(Location)
                }
            }
        }, new Resource
        {
            RelatedFrom = new EntitySet<Resource>(),
            RelatedTo = new EntitySet<Resource>(),
            Activities = new EntitySet<Activity>(),
            Members = new EntitySet<Member>(),
            Schedules = new EntitySet<Schedule>(),
            DefaultId = 1947833879L,
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
            LocationId = 2129760262L,
            Location = new Location
            {
                Name = "TestValue1711418884",
                LocaleType = LocaleType.Bussines,
                Email = "TestValue1584631276",
                PhoneType = PhoneType.Main,
                PhoneNumber = "TestValue798148403",
                Notices = "TestValue774408734",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 3051989L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1298592706L,
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
                    LocationId = 962602799L,
                    Location = default(Location)
                },
                ActivityId = 1789502039L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1671273002L,
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
                    LocationId = 1162744763L,
                    Location = default(Location)
                },
                ResourceId = 1546148206L,
                Resource = default(Resource),
                ScheduleId = 2001246726L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 2090640429L,
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
                    LocationId = 438270316L,
                    Location = default(Location)
                },
                ServiceId = 1766804929L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1270714302L,
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
                    LocationId = 288208885L,
                    Location = default(Location)
                },
                ApplicationId = 1133783699L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 933335298L,
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
                    LocationId = 1963892212L,
                    Location = default(Location)
                }
            }
        });
    }

    /// <summary>
    /// Checks that setting the ScheduleId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetScheduleId()
    {
        this._testClass.CheckProperty(x => x.ScheduleId, 2015683470L, 1608655620L);
    }

    /// <summary>
    /// Checks that setting the Schedule property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSchedule()
    {
        this._testClass.CheckProperty(x => x.Schedule, new Schedule
        {
            RelatedFrom = new EntitySet<Schedule>(),
            RelatedTo = new EntitySet<Schedule>(),
            Activities = new EntitySet<Activity>(),
            Members = new EntitySet<Member>(),
            Resources = new EntitySet<Resource>(),
            DefaultId = 299575953L,
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
            LocationId = 2046971393L,
            Location = new Location
            {
                Name = "TestValue759932663",
                LocaleType = LocaleType.Main,
                Email = "TestValue963180638",
                PhoneType = PhoneType.Personal,
                PhoneNumber = "TestValue1516941397",
                Notices = "TestValue499132618",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 1371239345L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 272667891L,
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
                    LocationId = 351753169L,
                    Location = default(Location)
                },
                ActivityId = 957279559L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 159265125L,
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
                    LocationId = 864589915L,
                    Location = default(Location)
                },
                ResourceId = 1175192760L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 51183943L,
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
                    LocationId = 1550197044L,
                    Location = default(Location)
                },
                ScheduleId = 1939052480L,
                Schedule = default(Schedule),
                ServiceId = 1332983312L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1518548966L,
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
                    LocationId = 104259684L,
                    Location = default(Location)
                },
                ApplicationId = 22030831L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 59730735L,
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
                    LocationId = 948978759L,
                    Location = default(Location)
                }
            }
        }, new Schedule
        {
            RelatedFrom = new EntitySet<Schedule>(),
            RelatedTo = new EntitySet<Schedule>(),
            Activities = new EntitySet<Activity>(),
            Members = new EntitySet<Member>(),
            Resources = new EntitySet<Resource>(),
            DefaultId = 1510625472L,
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
            LocationId = 329429626L,
            Location = new Location
            {
                Name = "TestValue1331016318",
                LocaleType = LocaleType.Additional,
                Email = "TestValue130803394",
                PhoneType = PhoneType.Personal,
                PhoneNumber = "TestValue1196424964",
                Notices = "TestValue1895010319",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 1686751059L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 2123459303L,
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
                    LocationId = 1944799977L,
                    Location = default(Location)
                },
                ActivityId = 898660657L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 14476071L,
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
                    LocationId = 2086150385L,
                    Location = default(Location)
                },
                ResourceId = 335412068L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 2139382254L,
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
                    LocationId = 272560642L,
                    Location = default(Location)
                },
                ScheduleId = 1172140384L,
                Schedule = default(Schedule),
                ServiceId = 1938989065L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1273480315L,
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
                    LocationId = 1649493557L,
                    Location = default(Location)
                },
                ApplicationId = 1133360870L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 279005191L,
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
                    LocationId = 902215014L,
                    Location = default(Location)
                }
            }
        });
    }

    /// <summary>
    /// Checks that setting the ServiceId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetServiceId()
    {
        this._testClass.CheckProperty(x => x.ServiceId, 95521542L, 237024942L);
    }

    /// <summary>
    /// Checks that setting the Service property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetService()
    {
        this._testClass.CheckProperty(x => x.Service, new Service
        {
            RelatedFrom = new EntitySet<Service>(),
            RelatedTo = new EntitySet<Service>(),
            Members = new EntitySet<Member>(),
            Applications = new RemoteSet<Application>(),
            ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
            DefaultId = 1731070302L,
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
            LocationId = 1141766465L,
            Location = new Location
            {
                Name = "TestValue630908900",
                LocaleType = LocaleType.Main,
                Email = "TestValue1504395936",
                PhoneType = PhoneType.Main,
                PhoneNumber = "TestValue1987240658",
                Notices = "TestValue1958437551",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 701243426L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 633751012L,
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
                    LocationId = 1787171521L,
                    Location = default(Location)
                },
                ActivityId = 1589448993L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 368329224L,
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
                    LocationId = 557189165L,
                    Location = default(Location)
                },
                ResourceId = 1223726574L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 604226497L,
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
                    LocationId = 1418012147L,
                    Location = default(Location)
                },
                ScheduleId = 183289469L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 1067273585L,
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
                    LocationId = 276532103L,
                    Location = default(Location)
                },
                ServiceId = 36707872L,
                Service = default(Service),
                ApplicationId = 1611530306L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 1603640412L,
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
                    LocationId = 1341084705L,
                    Location = default(Location)
                }
            }
        }, new Service
        {
            RelatedFrom = new EntitySet<Service>(),
            RelatedTo = new EntitySet<Service>(),
            Members = new EntitySet<Member>(),
            Applications = new RemoteSet<Application>(),
            ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
            DefaultId = 1245988324L,
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
            LocationId = 1079602947L,
            Location = new Location
            {
                Name = "TestValue230525413",
                LocaleType = LocaleType.Private,
                Email = "TestValue1446968849",
                PhoneType = PhoneType.Main,
                PhoneNumber = "TestValue50424435",
                Notices = "TestValue428801304",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 1979454509L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 2041262099L,
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
                    LocationId = 1230463525L,
                    Location = default(Location)
                },
                ActivityId = 54658499L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1264101419L,
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
                    LocationId = 1189616555L,
                    Location = default(Location)
                },
                ResourceId = 619063367L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1667254855L,
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
                    LocationId = 1058903646L,
                    Location = default(Location)
                },
                ScheduleId = 203522167L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 1384906959L,
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
                    LocationId = 1849144703L,
                    Location = default(Location)
                },
                ServiceId = 352210733L,
                Service = default(Service),
                ApplicationId = 830595296L,
                Application = new Application
                {
                    RelatedFrom = new EntitySet<Application>(),
                    RelatedTo = new EntitySet<Application>(),
                    Members = new EntitySet<Member>(),
                    Services = new RemoteSet<Service>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 803811160L,
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
                    LocationId = 1381799900L,
                    Location = default(Location)
                }
            }
        });
    }

    /// <summary>
    /// Checks that setting the ApplicationId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetApplicationId()
    {
        this._testClass.CheckProperty(x => x.ApplicationId, 49753741L, 1045267060L);
    }

    /// <summary>
    /// Checks that setting the Application property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetApplication()
    {
        this._testClass.CheckProperty(x => x.Application, new Application
        {
            RelatedFrom = new EntitySet<Application>(),
            RelatedTo = new EntitySet<Application>(),
            Members = new EntitySet<Member>(),
            Services = new RemoteSet<Service>(),
            ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
            DefaultId = 1862832370L,
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
            LocationId = 95715722L,
            Location = new Location
            {
                Name = "TestValue1982425076",
                LocaleType = LocaleType.Additional,
                Email = "TestValue200317070",
                PhoneType = PhoneType.Main,
                PhoneNumber = "TestValue1685609700",
                Notices = "TestValue1437307842",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 1398629827L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 424792989L,
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
                    LocationId = 47019709L,
                    Location = default(Location)
                },
                ActivityId = 1053506591L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 511468702L,
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
                    LocationId = 323978824L,
                    Location = default(Location)
                },
                ResourceId = 583326577L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1358370217L,
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
                    LocationId = 1757478131L,
                    Location = default(Location)
                },
                ScheduleId = 474550772L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 1474209287L,
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
                    LocationId = 1169068075L,
                    Location = default(Location)
                },
                ServiceId = 1487608725L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 228395592L,
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
                    LocationId = 1711709749L,
                    Location = default(Location)
                },
                ApplicationId = 1547502377L,
                Application = default(Application)
            }
        }, new Application
        {
            RelatedFrom = new EntitySet<Application>(),
            RelatedTo = new EntitySet<Application>(),
            Members = new EntitySet<Member>(),
            Services = new RemoteSet<Service>(),
            ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
            DefaultId = 1365112104L,
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
            LocationId = 1980669352L,
            Location = new Location
            {
                Name = "TestValue226623347",
                LocaleType = LocaleType.Additional,
                Email = "TestValue988873972",
                PhoneType = PhoneType.Main,
                PhoneNumber = "TestValue275791787",
                Notices = "TestValue996209160",
                Endpoints = new EntitySet<Endpoint>(),
                Positions = new EntitySet<Position>(),
                MemberId = 883980326L,
                Member = new Member
                {
                    RelatedFrom = new EntitySet<Member>(),
                    RelatedTo = new EntitySet<Member>(),
                    Applications = new EntitySet<Application>(),
                    Services = new EntitySet<Service>(),
                    Activities = new EntitySet<Activity>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 401701789L,
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
                    LocationId = 661915879L,
                    Location = default(Location)
                },
                ActivityId = 2102192360L,
                Activity = new Activity
                {
                    RelatedFrom = new EntitySet<Activity>(),
                    RelatedTo = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 593859875L,
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
                    LocationId = 12981423L,
                    Location = default(Location)
                },
                ResourceId = 1840945029L,
                Resource = new Resource
                {
                    RelatedFrom = new EntitySet<Resource>(),
                    RelatedTo = new EntitySet<Resource>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Schedules = new EntitySet<Schedule>(),
                    DefaultId = 1110923529L,
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
                    LocationId = 516532446L,
                    Location = default(Location)
                },
                ScheduleId = 1974277224L,
                Schedule = new Schedule
                {
                    RelatedFrom = new EntitySet<Schedule>(),
                    RelatedTo = new EntitySet<Schedule>(),
                    Activities = new EntitySet<Activity>(),
                    Members = new EntitySet<Member>(),
                    Resources = new EntitySet<Resource>(),
                    DefaultId = 1938470375L,
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
                    LocationId = 194270378L,
                    Location = default(Location)
                },
                ServiceId = 1620235146L,
                Service = new Service
                {
                    RelatedFrom = new EntitySet<Service>(),
                    RelatedTo = new EntitySet<Service>(),
                    Members = new EntitySet<Member>(),
                    Applications = new RemoteSet<Application>(),
                    ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                    DefaultId = 5397055L,
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
                    LocationId = 1839537112L,
                    Location = default(Location)
                },
                ApplicationId = 1060928135L,
                Application = default(Application)
            }
        });
    }
}