using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Mappings;

/// <summary>
/// Unit tests for the type <see cref="ContactMappings"/>.
/// </summary>
[TestClass]
public class ContactMappingsTests
{
    private ContactMappings _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactMappings"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ContactMappings();
    }

}