using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Infrastructure.Stores.Factories;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Factories;

/// <summary>
/// Unit tests for the type <see cref="EntryStoreFactory"/>.
/// </summary>
[TestClass]
public class EntryStoreFactoryTests
{
    private EntryStoreFactory _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EntryStoreFactory"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new EntryStoreFactory();
    }
}