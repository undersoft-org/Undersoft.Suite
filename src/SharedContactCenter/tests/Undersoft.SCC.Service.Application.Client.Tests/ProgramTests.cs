using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.Client;

namespace Undersoft.SCC.Service.Application.Client.Tests;

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
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallMainAsync()
    {
        // Arrange
        var args = new[] { "TestValue964074729", "TestValue1708693943", "TestValue1550926412" };

        // Act
        await Program.Main(args);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Main method throws when the args parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallMainWithNullArgsAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => Program.Main(default(string[])));
    }
}