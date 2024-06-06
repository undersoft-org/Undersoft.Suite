using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Graphic;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Graphic;

/// <summary>
/// Unit tests for the type <see cref="LogoSCC"/>.
/// </summary>
[TestClass]
public class LogoSCCTests
{
    private LogoSCC _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="LogoSCC"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new LogoSCC();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new LogoSCC();

        // Assert
        instance.ShouldNotBeNull();
    }
}