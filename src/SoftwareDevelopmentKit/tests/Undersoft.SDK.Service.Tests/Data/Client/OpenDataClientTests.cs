using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client;
using TStore = Undersoft.SDK.Service.Data.Store.IDataStore;

namespace Undersoft.SDK.Service.Tests.Data.Client;

/// <summary>
/// Unit tests for the type <see cref="OpenDataClient"/>.
/// </summary>
[TestClass]
public partial class OpenDataClient_1Tests
{
    private OpenDataClient<TStore> _testClass;
    private Uri _serviceUri;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="OpenDataClient"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._serviceUri = new Uri("https://test.domain1728264140.com");
        this._testClass = new OpenDataClient<TStore>(this._serviceUri);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new OpenDataClient<TStore>(this._serviceUri);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the serviceUri parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServiceUri()
    {
        Should.Throw<ArgumentNullException>(() => new OpenDataClient<TStore>(default(Uri)));
    }
}