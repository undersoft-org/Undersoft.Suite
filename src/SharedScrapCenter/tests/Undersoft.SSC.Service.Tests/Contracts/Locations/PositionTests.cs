using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK;
using Undersoft.SSC.Service.Contracts.Locations;

namespace Undersoft.SSC.Service.Tests.Contracts.Locations;

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
        this._testClass.CheckProperty(x => x.Height, 128586238, 1207582532);
    }

    /// <summary>
    /// Checks that setting the Width property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetWidth()
    {
        this._testClass.CheckProperty(x => x.Width, 592824811, 1229052550);
    }

    /// <summary>
    /// Checks that setting the Length property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLength()
    {
        this._testClass.CheckProperty(x => x.Length, 690099596, 645146670);
    }

    /// <summary>
    /// Checks that setting the GeoPoint property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGeoPoint()
    {
        this._testClass.CheckProperty(x => x.GeoPoint, new GeoPoint?(), new GeoPoint?());
    }

    /// <summary>
    /// Checks that setting the Point property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPoint()
    {
        this._testClass.CheckProperty(x => x.Point, new Point?(), new Point?());
    }

    /// <summary>
    /// Checks that setting the PrecisePoint property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPrecisePoint()
    {
        this._testClass.CheckProperty(x => x.PrecisePoint, new PointF?(), new PointF?());
    }

    /// <summary>
    /// Checks that setting the Size property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetSize()
    {
        this._testClass.CheckProperty(x => x.Size, new Size?(), new Size?());
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
        this._testClass.CheckProperty(x => x.LocationId, 166937273L, 906889792L);
    }
}