using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Server.Validators;
using Undersoft.SDK.Service;

namespace Undersoft.SCC.Service.Server.Tests.Validators;

/// <summary>
/// Unit tests for the type <see cref="GroupsValidator"/>.
/// </summary>
[TestClass]
public class GroupsValidatorTests
{
    private GroupsValidator _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="GroupsValidator"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new GroupsValidator(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new GroupsValidator(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new GroupsValidator(default(IServicer)));
    }
}