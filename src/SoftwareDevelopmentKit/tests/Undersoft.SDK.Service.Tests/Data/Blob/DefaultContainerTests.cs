using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="DefaultContainer"/>.
/// </summary>
[TestClass]
public class DefaultContainerTests
{
    private DefaultContainer _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="DefaultContainer"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new DefaultContainer();
    }
}