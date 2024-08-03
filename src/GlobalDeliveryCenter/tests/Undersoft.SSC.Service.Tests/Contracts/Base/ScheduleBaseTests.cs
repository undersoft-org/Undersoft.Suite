using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.GDC.Service.Contracts.Base;

namespace Undersoft.GDC.Service.Tests.Contracts.Base;

/// <summary>
/// Unit tests for the type <see cref="ScheduleBase"/>.
/// </summary>
[TestClass]
public class ScheduleBaseTests
{
    private ScheduleBase _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ScheduleBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ScheduleBase();
    }
}