using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Tests.Contracts;

/// <summary>
/// Unit tests for the type <see cref="Role"/>.
/// </summary>
[TestClass]
public class RoleTests
{
    private Role _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Role"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Role();
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
    /// Checks that setting the NormalizedName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNormalizedName()
    {
        this._testClass.CheckProperty(x => x.NormalizedName);
    }

    /// <summary>
    /// Checks that setting the Claims property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetClaims()
    {
        this._testClass.CheckProperty(x => x.Claims, new ObjectSet<Claim>(), new ObjectSet<Claim>());
    }
}