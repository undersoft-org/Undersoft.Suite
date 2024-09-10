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
/// Unit tests for the type <see cref="Position"/>.
/// </summary>
[TestClass]
public class PositionTests
{
    private Position _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Position"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Position();
    }

    /// <summary>
    /// Checks that setting the Place property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPlace()
    {
        this._testClass.CheckProperty(x => x.Place);
    }

    /// <summary>
    /// Checks that setting the Height property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetHeight()
    {
        this._testClass.CheckProperty(x => x.Height, 1348711314, 218435626);
    }

    /// <summary>
    /// Checks that setting the Width property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetWidth()
    {
        this._testClass.CheckProperty(x => x.Width, 1323345752, 406118897);
    }

    /// <summary>
    /// Checks that setting the Length property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLength()
    {
        this._testClass.CheckProperty(x => x.Length, 1649579574, 1704920367);
    }

    /// <summary>
    /// Checks that setting the X property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetX()
    {
        this._testClass.CheckProperty(x => x.X, 218958484, 477118300);
    }

    /// <summary>
    /// Checks that setting the Y property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetY()
    {
        this._testClass.CheckProperty(x => x.Y, 562315560, 1355396409);
    }

    /// <summary>
    /// Checks that setting the Z property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetZ()
    {
        this._testClass.CheckProperty(x => x.Z, 1787099004, 1458031453);
    }

    /// <summary>
    /// Checks that setting the Size property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSize()
    {
        this._testClass.CheckProperty(x => x.Size, 2083739852, 1338537529);
    }

    /// <summary>
    /// Checks that setting the Latitue property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLatitue()
    {
        this._testClass.CheckProperty(x => x.Latitue, 599999778.18, 375284026.26);
    }

    /// <summary>
    /// Checks that setting the Longitude property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLongitude()
    {
        this._testClass.CheckProperty(x => x.Longitude, 719073320.13, 1349945284.05);
    }

    /// <summary>
    /// Checks that setting the Altitude property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAltitude()
    {
        this._testClass.CheckProperty(x => x.Altitude, 1850559560.19, 1947791028.15);
    }

    /// <summary>
    /// Checks that setting the Volume property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetVolume()
    {
        this._testClass.CheckProperty(x => x.Volume);
    }

    /// <summary>
    /// Checks that setting the Block property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBlock()
    {
        this._testClass.CheckProperty(x => x.Block);
    }

    /// <summary>
    /// Checks that setting the Sector property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSector()
    {
        this._testClass.CheckProperty(x => x.Sector);
    }

    /// <summary>
    /// Checks that setting the Cluster property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCluster()
    {
        this._testClass.CheckProperty(x => x.Cluster);
    }

    /// <summary>
    /// Checks that setting the Level property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLevel()
    {
        this._testClass.CheckProperty(x => x.Level);
    }

    /// <summary>
    /// Checks that setting the LocationId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocationId()
    {
        this._testClass.CheckProperty(x => x.LocationId, 1234288952L, 1403863785L);
    }

    /// <summary>
    /// Checks that setting the Location property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocation()
    {
        this._testClass.CheckProperty(x => x.Location, new Location
        {
            Name = "TestValue1578782698",
            LocaleType = LocaleType.Private,
            Email = "TestValue1537851425",
            PhoneType = PhoneType.Bussines,
            PhoneNumber = "TestValue130859027",
            Notices = "TestValue1489606739",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 1604849264L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 327961984L,
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
                LocationId = 240102863L,
                Location = default(Location)
            },
            ActivityId = 374376524L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 953797541L,
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
                LocationId = 2032215226L,
                Location = default(Location)
            },
            ResourceId = 458897001L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 1320549565L,
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
                LocationId = 1064940254L,
                Location = default(Location)
            },
            ScheduleId = 738107339L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 1509062213L,
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
                LocationId = 1804760051L,
                Location = default(Location)
            },
            ServiceId = 1258378898L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1765986039L,
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
                LocationId = 1381505261L,
                Location = default(Location)
            },
            ApplicationId = 1499290005L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 812191246L,
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
                LocationId = 1374164167L,
                Location = default(Location)
            }
        }, new Location
        {
            Name = "TestValue1821368364",
            LocaleType = LocaleType.Private,
            Email = "TestValue924895548",
            PhoneType = PhoneType.Bussines,
            PhoneNumber = "TestValue1386231953",
            Notices = "TestValue1472254375",
            Endpoints = new EntitySet<Endpoint>(),
            Positions = new EntitySet<Position>(),
            MemberId = 94418235L,
            Member = new Member
            {
                RelatedFrom = new EntitySet<Member>(),
                RelatedTo = new EntitySet<Member>(),
                Applications = new EntitySet<Application>(),
                Services = new EntitySet<Service>(),
                Activities = new EntitySet<Activity>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 722827691L,
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
                LocationId = 456047759L,
                Location = default(Location)
            },
            ActivityId = 2092331176L,
            Activity = new Activity
            {
                RelatedFrom = new EntitySet<Activity>(),
                RelatedTo = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 787136575L,
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
                LocationId = 694642003L,
                Location = default(Location)
            },
            ResourceId = 1015751119L,
            Resource = new Resource
            {
                RelatedFrom = new EntitySet<Resource>(),
                RelatedTo = new EntitySet<Resource>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Schedules = new EntitySet<Schedule>(),
                DefaultId = 83276276L,
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
                LocationId = 1574061340L,
                Location = default(Location)
            },
            ScheduleId = 876835392L,
            Schedule = new Schedule
            {
                RelatedFrom = new EntitySet<Schedule>(),
                RelatedTo = new EntitySet<Schedule>(),
                Activities = new EntitySet<Activity>(),
                Members = new EntitySet<Member>(),
                Resources = new EntitySet<Resource>(),
                DefaultId = 1019752683L,
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
                LocationId = 234353576L,
                Location = default(Location)
            },
            ServiceId = 1115293546L,
            Service = new Service
            {
                RelatedFrom = new EntitySet<Service>(),
                RelatedTo = new EntitySet<Service>(),
                Members = new EntitySet<Member>(),
                Applications = new RemoteSet<Application>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 1412722480L,
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
                LocationId = 504233912L,
                Location = default(Location)
            },
            ApplicationId = 51524626L,
            Application = new Application
            {
                RelatedFrom = new EntitySet<Application>(),
                RelatedTo = new EntitySet<Application>(),
                Members = new EntitySet<Member>(),
                Services = new RemoteSet<Service>(),
                ServicesToApplications = new RemoteSet<RemoteLink<Service, Application>>(),
                DefaultId = 879640528L,
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
                LocationId = 1432994464L,
                Location = default(Location)
            }
        });
    }
}