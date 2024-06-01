using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="Amount"/>.
/// </summary>
[TestClass]
public class AmountTests
{
    private Amount _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Amount"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Amount();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Amount();

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
    /// Checks that setting the Description property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDescription()
    {
        this._testClass.CheckProperty(x => x.Description);
    }

    /// <summary>
    /// Checks that setting the Kind property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetKind()
    {
        this._testClass.CheckProperty(x => x.Kind, new AmountKind?(), new AmountKind?());
    }

    /// <summary>
    /// Checks that setting the DateFrom property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDateFrom()
    {
        this._testClass.CheckProperty(x => x.DateFrom);
    }

    /// <summary>
    /// Checks that setting the DateTo property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDateTo()
    {
        this._testClass.CheckProperty(x => x.DateTo);
    }

    /// <summary>
    /// Checks that setting the Deadline property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDeadline()
    {
        this._testClass.CheckProperty(x => x.Deadline);
    }

    /// <summary>
    /// Checks that setting the Interval property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetInterval()
    {
        this._testClass.CheckProperty(x => x.Interval, TimeSpan.FromSeconds(294), TimeSpan.FromSeconds(233));
    }

    /// <summary>
    /// Checks that setting the Duration property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDuration()
    {
        this._testClass.CheckProperty(x => x.Duration, 1764197172.54, 1124337624.3);
    }

    /// <summary>
    /// Checks that setting the Unit property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetUnit()
    {
        this._testClass.CheckProperty(x => x.Unit);
    }

    /// <summary>
    /// Checks that setting the Quantity property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetQuantity()
    {
        this._testClass.CheckProperty(x => x.Quantity, 636077584.89, 1649284424.37);
    }

    /// <summary>
    /// Checks that setting the Value property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetValue()
    {
        this._testClass.CheckProperty(x => x.Value, 1176675756.3, 1783115373.6);
    }

    /// <summary>
    /// Checks that setting the Tax property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTax()
    {
        this._testClass.CheckProperty(x => x.Tax, 534815284.40999997, 528724141.11);
    }

    /// <summary>
    /// Checks that setting the TaxValue property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTaxValue()
    {
        this._testClass.CheckProperty(x => x.TaxValue, 1713871166.04, 322443890.51);
    }

    /// <summary>
    /// Checks that setting the NetValue property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNetValue()
    {
        this._testClass.CheckProperty(x => x.NetValue, 522072905.31, 1049193603.81);
    }

    /// <summary>
    /// Checks that setting the Fraction property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetFraction()
    {
        this._testClass.CheckProperty(x => x.Fraction, 494858674.53, 719281503.27);
    }

    /// <summary>
    /// Checks that setting the Factor property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetFactor()
    {
        this._testClass.CheckProperty(x => x.Factor, 1468874738.43, 357612193.62);
    }

    /// <summary>
    /// Checks that setting the Bias property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBias()
    {
        this._testClass.CheckProperty(x => x.Bias, 2020889004.75, 1617253653.51);
    }

    /// <summary>
    /// Checks that setting the GrossAmount property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetGrossAmount()
    {
        this._testClass.CheckProperty(x => x.GrossAmount, 122310879.57, 287280652.23);
    }

    /// <summary>
    /// Checks that setting the TaxAmount property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTaxAmount()
    {
        this._testClass.CheckProperty(x => x.TaxAmount, 1639361812.77, 1264301988.84);
    }

    /// <summary>
    /// Checks that setting the NetAmount property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNetAmount()
    {
        this._testClass.CheckProperty(x => x.NetAmount, 513843240.24, 405196804.98);
    }

    /// <summary>
    /// Checks that setting the Share property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetShare()
    {
        this._testClass.CheckProperty(x => x.Share, 1381879764.54, 991466253.36);
    }

    /// <summary>
    /// Checks that setting the TaxShare property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetTaxShare()
    {
        this._testClass.CheckProperty(x => x.TaxShare, 2111316706.62, 1098896688.45);
    }

    /// <summary>
    /// Checks that setting the NetShare property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetNetShare()
    {
        this._testClass.CheckProperty(x => x.NetShare, 401423765.49, 518364459.36);
    }
}