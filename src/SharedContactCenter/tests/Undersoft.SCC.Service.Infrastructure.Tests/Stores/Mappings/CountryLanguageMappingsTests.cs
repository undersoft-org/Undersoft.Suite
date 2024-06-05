using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Mappings;

/// <summary>
/// Unit tests for the type <see cref="CountryLanguageMappings"/>.
/// </summary>
[TestClass]
public class CountryLanguageMappingsTests
{
    private CountryLanguageMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="CountryLanguageMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new CountryLanguageMappings();
    }

}