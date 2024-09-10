using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Base;

namespace Undersoft.SSC.Service.Tests.Contracts.Base;

/// <summary>
/// Unit tests for the type <see cref="MemberBase"/>.
/// </summary>
[TestClass]
public class MemberBaseTests
{
    private MemberBase _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="MemberBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new MemberBase();
    }
}