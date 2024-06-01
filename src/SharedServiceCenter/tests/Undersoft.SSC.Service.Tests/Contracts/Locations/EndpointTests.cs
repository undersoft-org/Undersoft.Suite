using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Locations;

namespace Undersoft.SSC.Service.Tests.Contracts.Locations;

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
        this._testClass.CheckProperty(x => x.Port, 321011521, 974437318);
    }

    /// <summary>
    /// Checks that setting the URI property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetURI()
    {
        this._testClass.CheckProperty(x => x.URI);
    }
}