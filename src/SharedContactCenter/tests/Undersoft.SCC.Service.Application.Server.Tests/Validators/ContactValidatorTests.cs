using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System;
using Undersoft.SCC.Service.Application.Server.Validators;
using Undersoft.SDK.Service;

namespace Undersoft.SCC.Service.Application.Server.Tests.Validators;

/// <summary>
/// Unit tests for the type <see cref="ContactValidator"/>.
/// </summary>
[TestClass]
public class ContactValidatorTests
{
    private ContactValidator _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactValidator"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new ContactValidator(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ContactValidator(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new ContactValidator(default(IServicer)));
    }
}