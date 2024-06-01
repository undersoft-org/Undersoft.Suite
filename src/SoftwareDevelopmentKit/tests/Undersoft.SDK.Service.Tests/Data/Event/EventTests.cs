using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Tests.Data.Event;

/// <summary>
/// Unit tests for the type <see cref="Event"/>.
/// </summary>
[TestClass]
public class EventTests
{
    private Undersoft.SDK.Service.Data.Event.Event _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Event"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Undersoft.SDK.Service.Data.Event.Event();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Undersoft.SDK.Service.Data.Event.Event();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that setting the Version property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetVersion()
    {
        this._testClass.CheckProperty(x => x.Version, (uint)704375663, (uint)966275170);
    }

    /// <summary>
    /// Checks that setting the EventType property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEventType()
    {
        this._testClass.CheckProperty(x => x.EventType);
    }

    /// <summary>
    /// Checks that setting the EntityId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEntityId()
    {
        this._testClass.CheckProperty(x => x.EntityId);
    }

    /// <summary>
    /// Checks that setting the EntityTypeName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEntityTypeName()
    {
        this._testClass.CheckProperty(x => x.EntityTypeName);
    }

    /// <summary>
    /// Checks that setting the Data property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetData()
    {
        this._testClass.CheckProperty(x => x.Data, new byte[] { 83, 129, 242, 135 }, new byte[] { 180, 224, 78, 75 });
    }

    /// <summary>
    /// Checks that setting the PublishTime property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPublishTime()
    {
        this._testClass.CheckProperty(x => x.PublishTime);
    }

    /// <summary>
    /// Checks that setting the PublishStatus property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPublishStatus()
    {
        this._testClass.CheckProperty(x => x.PublishStatus, EventPublishStatus.Error, EventPublishStatus.None);
    }
}