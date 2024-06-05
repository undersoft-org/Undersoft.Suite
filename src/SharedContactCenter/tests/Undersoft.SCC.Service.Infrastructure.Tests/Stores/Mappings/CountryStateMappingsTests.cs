using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Mappings;

/// <summary>
/// Unit tests for the type <see cref="CountryStateMappings"/>.
/// </summary>
[TestClass]
public class CountryStateMappingsTests
{
    private CountryStateMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="CountryStateMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new CountryStateMappings();
    }

}