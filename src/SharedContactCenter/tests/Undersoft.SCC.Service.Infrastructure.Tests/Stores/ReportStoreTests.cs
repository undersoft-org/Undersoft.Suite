using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Undersoft.SCC.Service.Infrastructure.Stores;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores;

/// <summary>
/// Unit tests for the type <see cref="ReportStore"/>.
/// </summary>
[TestClass]
public class ReportStoreTests
{
    private ReportStore _testClass;
    private DbContextOptions<ReportStore> _options;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ReportStore"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = new DbContextOptions<ReportStore>();
        this._testClass = new ReportStore(this._options);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ReportStore(this._options);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new ReportStore(default(DbContextOptions<ReportStore>)));
    }
}