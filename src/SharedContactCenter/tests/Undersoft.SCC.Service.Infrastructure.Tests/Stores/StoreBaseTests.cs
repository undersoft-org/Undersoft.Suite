using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Service.Infrastructure.Stores;
using TContext = Microsoft.EntityFrameworkCore.DbContext;
using TStore = Undersoft.SDK.Service.Data.Store.IDataServerStore;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores;

/// <summary>
/// Unit tests for the type <see cref="StoreBase"/>.
/// </summary>
[TestClass]
public class StoreBase_2Tests
{
    private StoreBase<TStore, TContext> _testClass;
    private DbContextOptions<TContext> _options;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="StoreBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = new DbContextOptions<TContext>();
        this._testClass = new StoreBase<TStore, TContext>(this._options);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new StoreBase<TStore, TContext>(this._options);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new StoreBase<TStore, TContext>(default(DbContextOptions<TContext>)));
    }


}