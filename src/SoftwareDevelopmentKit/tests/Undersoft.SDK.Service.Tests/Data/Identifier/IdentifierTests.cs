using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Stocks;
using TObject = Undersoft.SDK.Stocks.StockContext;

namespace Undersoft.SDK.Service.Tests.Data.Identifier;

/// <summary>
/// Unit tests for the type <see cref="Identifier"/>.
/// </summary>
[TestClass]
public class Identifier_1Tests
{
    private Identifier<TObject> _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Identifier"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Identifier<TObject>();
    }

    /// <summary>
    /// Checks that setting the Object property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetObject()
    {
        this._testClass.CheckProperty(x => x.Object, new StockContext(), new StockContext());
    }
}

/// <summary>
/// Unit tests for the type <see cref="Identifier"/>.
/// </summary>
[TestClass]
public class IdentifierTests
{
    private Undersoft.SDK.Service.Data.Identifier.Identifier _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Identifier"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Undersoft.SDK.Service.Data.Identifier.Identifier();
    }

    /// <summary>
    /// Checks that setting the ObjectId property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetObjectId()
    {
        this._testClass.CheckProperty(x => x.ObjectId);
    }

    /// <summary>
    /// Checks that setting the Kind property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetKind()
    {
        this._testClass.CheckProperty(x => x.Kind, IdKind.Key, IdKind.Label);
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
    /// Checks that setting the Value property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetValue()
    {
        this._testClass.CheckProperty(x => x.Value);
    }

    /// <summary>
    /// Checks that setting the Key property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetKey()
    {
        this._testClass.CheckProperty(x => x.Key);
    }
}