using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting.User;

/// <summary>
/// Unit tests for the type <see cref="AdminContacts"/>.
/// </summary>
[TestClass]
public class AdminContactsTests
{
    private AdminContacts _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AdminContacts"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AdminContacts();
    }
}