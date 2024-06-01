using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Base;

namespace Undersoft.SSC.Service.Tests.Contracts.Base;

/// <summary>
/// Unit tests for the type <see cref="ResourceBase"/>.
/// </summary>
[TestClass]
public class ResourceBaseTests
{
    private ResourceBase _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ResourceBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ResourceBase();
    }

    /// <summary>
    /// Checks that setting the Path property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPath()
    {
        this._testClass.CheckProperty(x => x.Path);
    }
}