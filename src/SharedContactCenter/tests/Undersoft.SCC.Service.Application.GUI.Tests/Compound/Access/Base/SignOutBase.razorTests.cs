using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SCC.Service.Application.GUI.Compound.Access;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Access;

/// <summary>
/// Unit tests for the type <see cref="SignOutBase"/>.
/// </summary>
[TestClass]
public partial class SignOutBaseTests
{
    private SignOutBase _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="SignOutBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new SignOutBase();
    }
}