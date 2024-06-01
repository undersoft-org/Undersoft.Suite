using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SDK.Service.Server.Documentation;

namespace Undersoft.SDK.Service.Server.Tests.Documentation;

/// <summary>
/// Unit tests for the type <see cref="SwaggerExcludeAttribute"/>.
/// </summary>
[TestClass]
public class SwaggerExcludeAttributeTests
{
    private SwaggerExcludeAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="SwaggerExcludeAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new SwaggerExcludeAttribute();
    }
}
