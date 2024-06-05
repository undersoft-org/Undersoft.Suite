using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

namespace Undersoft.SCC.Service.Application.GUI.Tests.Compound.Presenting.User;

/// <summary>
/// Unit tests for the type <see cref="UserContacts"/>.
/// </summary>
[TestClass]
public class UserContactsTests
{
    private UserContacts _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="UserContacts"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new UserContacts();
    }
}