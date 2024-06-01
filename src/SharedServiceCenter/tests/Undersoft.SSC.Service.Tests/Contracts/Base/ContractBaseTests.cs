using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Domain.Entities;
using Undersoft.SSC.Service.Contracts.Base;
using TContract = Undersoft.SDK.Service.Data.Remote.RemoteLink;
using TGroup = System.AttributeTargets;


namespace Undersoft.SSC.Service.Tests.Contracts.Base;

/// <summary>
/// Unit tests for the type <see cref="ContractBase"/>.
/// </summary>
[TestClass]
public class ContractBase_4Tests
{
    private ContractBase<TContract, Detail, Setting, TGroup> _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContractBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ContractBase<TContract, Detail, Setting, TGroup>();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ContractBase<TContract, Detail, Setting, TGroup>();

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that setting the DefaultId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDefaultId()
    {
        this._testClass.CheckProperty(x => x.DefaultId, 377686613L, 1978706745L);
    }

    /// <summary>
    /// Checks that setting the LocationId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLocationId()
    {
        this._testClass.CheckProperty(x => x.LocationId, 1034648915L, 1978639380L);
    }
}