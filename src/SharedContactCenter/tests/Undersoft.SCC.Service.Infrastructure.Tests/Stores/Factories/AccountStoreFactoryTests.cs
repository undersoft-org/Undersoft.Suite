using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Infrastructure.Stores.Factories;

namespace Undersoft.SCC.Service.Infrastructure.Tests.Stores.Factories;

/// <summary>
/// Unit tests for the type <see cref="AccountStoreFactory"/>.
/// </summary>
[TestClass]
public class AccountStoreFactoryTests
{
    private AccountStoreFactory _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountStoreFactory"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountStoreFactory();
    }
}