using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Infrastructure.Stores.Factories;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Factories;

/// <summary>
/// Unit tests for the type <see cref="EventStoreFactory"/>.
/// </summary>
[TestClass]
public class EventStoreFactoryTests
{
    private EventStoreFactory _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventStoreFactory"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new EventStoreFactory();
    }
}