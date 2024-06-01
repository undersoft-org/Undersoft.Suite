using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Claim"/>.
/// </summary>
[TestClass]
public class ClaimTests
{
    private Claim _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Claim"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Claim();
    }

    /// <summary>
    /// Checks that setting the ClaimType property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaimType()
    {
        this._testClass.CheckProperty(x => x.ClaimType);
    }

    /// <summary>
    /// Checks that setting the ClaimValue property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaimValue()
    {
        this._testClass.CheckProperty(x => x.ClaimValue);
    }
}