using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="Expression"/>.
/// </summary>
[TestClass]
public class ExpressionTests
{
    private Expression _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Expression"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Expression();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Expression();

        // Assert
        instance.ShouldNotBeNull();
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
    /// Checks that setting the ShortName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetShortName()
    {
        this._testClass.CheckProperty(x => x.ShortName);
    }

    /// <summary>
    /// Checks that setting the Description property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDescription()
    {
        this._testClass.CheckProperty(x => x.Description);
    }

    /// <summary>
    /// Checks that setting the TaxId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTaxId()
    {
        this._testClass.CheckProperty(x => x.TaxId);
    }

    /// <summary>
    /// Checks that setting the CompanySize property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCompanySize()
    {
        this._testClass.CheckProperty(x => x.CompanySize);
    }

    /// <summary>
    /// Checks that setting the Revenue property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetRevenue()
    {
        this._testClass.CheckProperty(x => x.Revenue);
    }

    /// <summary>
    /// Checks that setting the Website property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetWebsite()
    {
        this._testClass.CheckProperty(x => x.Website);
    }
}