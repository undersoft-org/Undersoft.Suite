using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Client;
using TStore = Undersoft.SDK.Service.Data.Store.IDataStore;

namespace Undersoft.SDK.Service.Tests.Data.Client;

/// <summary>
/// Unit tests for the type <see cref="ApiDataClient"/>.
/// </summary>
[TestClass]
public partial class ApiDataClient_1Tests
{
    private ApiDataClient<TStore> _testClass;
    private Uri _serviceUri;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ApiDataClient"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._serviceUri = new Uri("https://test.domain526037711.com");
        this._testClass = new ApiDataClient<TStore>(this._serviceUri);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ApiDataClient<TStore>(this._serviceUri);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the serviceUri parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServiceUri()
    {
        Should.Throw<ArgumentNullException>(() => new ApiDataClient<TStore>(default(Uri)));
    }
}