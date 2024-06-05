using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Mappings;

/// <summary>
/// Unit tests for the type <see cref="CountryMappings"/>.
/// </summary>
[TestClass]
public class CountryMappingsTests
{
    private CountryMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="CountryMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new CountryMappings();
    }

}