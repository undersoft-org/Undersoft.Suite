using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Infrastructure.Stores.Factories;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Factories;

/// <summary>
/// Unit tests for the type <see cref="ReportStoreFactory"/>.
/// </summary>
[TestClass]
public class ReportStoreFactoryTests
{
    private ReportStoreFactory _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ReportStoreFactory"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ReportStoreFactory();
    }
}