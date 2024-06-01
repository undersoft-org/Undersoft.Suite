using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Tests.Hosting;

/// <summary>
/// Unit tests for the type <see cref="ServiceHostBuilder"/>.
/// </summary>
[TestClass]
public class ServiceHostBuilderTests
{
    private ServiceHostBuilder _testClass;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ServiceHostBuilder"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new ServiceHostBuilder(this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new ServiceHostBuilder();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new ServiceHostBuilder(this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the servicer parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServicer()
    {
        Should.Throw<ArgumentNullException>(() => new ServiceHostBuilder(default(IServicer)));
    }

}