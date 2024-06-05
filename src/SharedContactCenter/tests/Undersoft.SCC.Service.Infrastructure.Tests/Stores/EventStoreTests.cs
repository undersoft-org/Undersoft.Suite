using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Service.Infrastructure.Stores;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores;

/// <summary>
/// Unit tests for the type <see cref="EventStore"/>.
/// </summary>
[TestClass]
public class EventStoreTests
{
    private EventStore _testClass;
    private DbContextOptions<EventStore> _options;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventStore"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = new DbContextOptions<EventStore>();
        this._testClass = new EventStore(this._options);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventStore(this._options);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new EventStore(default(DbContextOptions<EventStore>)));
    }
}