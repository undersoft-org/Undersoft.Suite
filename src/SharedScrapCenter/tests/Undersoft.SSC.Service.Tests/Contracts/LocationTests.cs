using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Locations;

namespace Undersoft.SSC.Service.Tests.Contracts;

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
        this._testClass.CheckProperty(x => x.PhoneType, PhoneType.Personal, PhoneType.Fax);
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
    /// Checks that setting the Website property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetWebsite()
    {
        this._testClass.CheckProperty(x => x.Website);
    }

    /// <summary>
    /// Checks that setting the SocialMedia property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSocialMedia()
    {
        this._testClass.CheckProperty(x => x.SocialMedia);
    }

    /// <summary>
    /// Checks that setting the Endpoints property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEndpoints()
    {
        this._testClass.CheckProperty(x => x.Endpoints, new ObjectSet<Endpoint>(), new ObjectSet<Endpoint>());
    }

    /// <summary>
    /// Checks that setting the Positions property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPositions()
    {
        this._testClass.CheckProperty(x => x.Positions, new ObjectSet<Position>(), new ObjectSet<Position>());
    }
}