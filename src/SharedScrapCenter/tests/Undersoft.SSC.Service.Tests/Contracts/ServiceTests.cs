using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Base;
using Undersoft.SSC.Service.Contracts.Locations;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Service"/>.
/// </summary>
[TestClass]
public class ServiceTests
{
    private Undersoft.SSC.Service.Contracts.Service _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Service"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Undersoft.SSC.Service.Contracts.Service();
    }

    /// <summary>
    /// Checks that setting the RelatedFrom property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedFrom()
    {
        this._testClass.CheckProperty(x => x.RelatedFrom, new ObjectSet<ServiceBase>(), new ObjectSet<ServiceBase>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new ObjectSet<ServiceBase>(), new ObjectSet<ServiceBase>());
    }

    /// <summary>
    /// Checks that setting the Applications property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetApplications()
    {
        this._testClass.CheckProperty(x => x.Applications, new ObjectSet<ApplicationBase>(), new ObjectSet<ApplicationBase>());
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
            Name = "TestValue1765126229",
            LocaleType = LocaleType.Private,
            Email = "TestValue343345005",
            PhoneType = PhoneType.Personal,
            PhoneNumber = "TestValue678731968",
            Notices = "TestValue1964529918",
            Website = "TestValue1205494494",
            SocialMedia = "TestValue37583256",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        }, new Location
        {
            Name = "TestValue1658220636",
            LocaleType = LocaleType.Bussines,
            Email = "TestValue1388054213",
            PhoneType = PhoneType.Main,
            PhoneNumber = "TestValue51594149",
            Notices = "TestValue1441510094",
            Website = "TestValue2068953296",
            SocialMedia = "TestValue424557954",
            Endpoints = new ObjectSet<Endpoint>(),
            Positions = new ObjectSet<Position>()
        });
    }
}