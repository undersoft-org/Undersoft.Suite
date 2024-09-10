using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SSC.Domain.Entities;

namespace Undersoft.SSC.Tests.Domain.Entities;

/// <summary>
/// Unit tests for the type <see cref="Setting"/>.
/// </summary>
[TestClass]
public class SettingTests
{
    private Setting _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Setting"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Setting();
    }

    /// <summary>
    /// Checks that setting the RelatedFrom property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedFrom()
    {
        this._testClass.CheckProperty(x => x.RelatedFrom, new EntitySet<Setting>(), new EntitySet<Setting>());
    }

    /// <summary>
    /// Checks that setting the RelatedTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRelatedTo()
    {
        this._testClass.CheckProperty(x => x.RelatedTo, new EntitySet<Setting>(), new EntitySet<Setting>());
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
    /// Checks that setting the DefaultId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDefaultId()
    {
        this._testClass.CheckProperty(x => x.DefaultId);
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
}