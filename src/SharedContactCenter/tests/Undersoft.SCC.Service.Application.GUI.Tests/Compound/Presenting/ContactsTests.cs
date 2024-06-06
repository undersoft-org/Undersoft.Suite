using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting;

/// <summary>
/// Unit tests for the type <see cref="Contacts"/>.
/// </summary>
[TestClass]
public class ContactsTests
{
    private Contacts _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Contacts"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Contacts();
    }
}