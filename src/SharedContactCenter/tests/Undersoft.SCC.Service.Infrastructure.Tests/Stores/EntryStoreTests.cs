using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Infrastructure.Stores;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores;

/// <summary>
/// Unit tests for the type <see cref="EntryStore"/>.
/// </summary>
[TestClass]
public class EntryStoreTests
{
    private EntryStore _testClass;
    private DbContextOptions<EntryStore> _options;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EntryStore"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = new DbContextOptions<EntryStore>();
        this._testClass = new EntryStore(this._options);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EntryStore(this._options);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new EntryStore(default(DbContextOptions<EntryStore>)));
    }
}