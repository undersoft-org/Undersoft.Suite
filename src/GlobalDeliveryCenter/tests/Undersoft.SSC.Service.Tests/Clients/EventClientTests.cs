using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.GDC.Service.Clients;

namespace Undersoft.GDC.Service.Tests.Clients;

/// <summary>
/// Unit tests for the type <see cref="EventClient"/>.
/// </summary>
[TestClass]
public class EventClientTests
{
    private EventClient _testClass;
    private Uri _serviceUri;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="EventClient"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._serviceUri = new Uri("https://test.domain546619590.com");
        this._testClass = new EventClient(this._serviceUri);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new EventClient(this._serviceUri);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the serviceUri parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServiceUri()
    {
        Should.Throw<ArgumentNullException>(() => new EventClient(default(Uri)));
    }
}