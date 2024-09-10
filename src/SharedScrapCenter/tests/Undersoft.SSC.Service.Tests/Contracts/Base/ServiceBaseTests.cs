using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Base;

namespace Undersoft.SSC.Service.Tests.Contracts.Base;

/// <summary>
/// Unit tests for the type <see cref="ServiceBase"/>.
/// </summary>
[TestClass]
public class ServiceBaseTests
{
    private ServiceBase _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ServiceBase();
    }

    /// <summary>
    /// Checks that setting the Members property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetMembers()
    {
        this._testClass.CheckProperty(x => x.Members, new ObjectSet<Member>(), new ObjectSet<Member>());
    }
}