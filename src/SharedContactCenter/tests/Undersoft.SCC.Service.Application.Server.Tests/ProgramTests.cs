using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace Undersoft.SCC.Service.Application.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="Program"/>.
/// </summary>
[TestClass]
public class ProgramTests
{
    private Program _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Program"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Program();
    }

    /// <summary>
    /// Checks that the Main method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallMain()
    {
        // Arrange
        var args = new[] { "TestValue1114210086", "TestValue991112277", "TestValue1890448379" };

        // Act
        Program.Main(args);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Main method throws when the args parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallMainWithNullArgs()
    {
        Should.Throw<ArgumentNullException>(() => Program.Main(default(string[])));
    }

    /// <summary>
    /// Checks that the Launch method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLaunch()
    {
        // Act
        Program.Launch();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Restart method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRestart()
    {
        // Act
        Program.Restart();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Shutdown method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallShutdown()
    {
        // Act
        Program.Shutdown();

        // Assert
        Assert.Fail("Create or modify test");
    }
}