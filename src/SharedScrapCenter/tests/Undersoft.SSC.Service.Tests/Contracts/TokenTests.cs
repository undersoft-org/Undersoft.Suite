using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Token"/>.
/// </summary>
[TestClass]
public class TokenTests
{
    private Token _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Token"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Token();
    }

    /// <summary>
    /// Checks that setting the UserId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetUserId()
    {
        this._testClass.CheckProperty(x => x.UserId);
    }

    /// <summary>
    /// Checks that setting the LoginProvider property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLoginProvider()
    {
        this._testClass.CheckProperty(x => x.LoginProvider);
    }

    /// <summary>
    /// Checks that setting the Name property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetName()
    {
        this._testClass.CheckProperty(x => x.Name);
    }

    /// <summary>
    /// Checks that setting the Value property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetValue()
    {
        this._testClass.CheckProperty(x => x.Value);
    }
}