using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Clients;

namespace Undersoft.SCC.Service.Tests.Clients;

/// <summary>
/// Unit tests for the type <see cref="AccessClient"/>.
/// </summary>
[TestClass]
public class AccessClientTests
{
    private AccessClient _testClass;
    private Uri _serviceUri;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccessClient"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._serviceUri = new Uri("https://test.domain1310628277.com");
        this._testClass = new AccessClient(this._serviceUri);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccessClient(this._serviceUri);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the serviceUri parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServiceUri()
    {
        Should.Throw<ArgumentNullException>(() => new AccessClient(default(Uri)));
    }
}