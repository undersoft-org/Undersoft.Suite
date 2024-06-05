using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Access;
using Undersoft.SDK.Service;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Access;

/// <summary>
/// Unit tests for the type <see cref="AccountValidator"/>.
/// </summary>
[TestClass]
public class AccountValidatorTests
{
    private AccountValidator _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountValidator"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new AccountValidator(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountValidator(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new AccountValidator(default(IServicer)));
    }
}