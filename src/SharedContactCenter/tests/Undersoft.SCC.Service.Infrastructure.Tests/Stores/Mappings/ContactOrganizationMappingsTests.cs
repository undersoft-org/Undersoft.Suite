using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Mappings;

/// <summary>
/// Unit tests for the type <see cref="OrganizationMappings"/>.
/// </summary>
[TestClass]
public class ContactOrganizationMappingsTests
{
    private OrganizationMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="OrganizationMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new OrganizationMappings();
    }

}