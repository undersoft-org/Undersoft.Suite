using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Documentation;

namespace Undersoft.SDK.Service.Server.Tests.Documentation;

/// <summary>
/// Unit tests for the type <see cref="IgnoreApiAttribute"/>.
/// </summary>
[TestClass]
public class IgnoreApiAttributeTests
{
    private IgnoreApiAttribute _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="IgnoreApiAttribute"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new IgnoreApiAttribute();
    }
}