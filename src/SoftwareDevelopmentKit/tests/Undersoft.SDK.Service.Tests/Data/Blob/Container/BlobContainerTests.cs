using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;
using TContainer = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Blob.Container;

/// <summary>
/// Unit tests for the type <see cref="BlobContainer"/>.
/// </summary>
[TestClass]
public class BlobContainer_1Tests
{
    private BlobContainer<TContainer> _testClass;
    private IBlobContainerFactory _blobContainerFactory;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobContainer"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._blobContainerFactory = Substitute.For<IBlobContainerFactory>();
        this._testClass = new BlobContainer<TContainer>(this._blobContainerFactory);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobContainer<TContainer>(this._blobContainerFactory);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the blobContainerFactory parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullBlobContainerFactory()
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainer<TContainer>(default(IBlobContainerFactory)));
    }

    /// <summary>
    /// Checks that the SaveAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveAsync()
    {
        // Arrange
        var name = "TestValue1109153841";
        var stream = new MemoryStream();
        var overrideExisting = false;
        var cancellationToken = CancellationToken.None;

        // Act
        await this._testClass.SaveAsync(name, stream, overrideExisting, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SaveAsync method throws when the stream parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSaveAsyncWithNullStreamAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SaveAsync("TestValue710402821", default(Stream), false, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the SaveAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSaveAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SaveAsync(value, new MemoryStream(), false, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the DeleteAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallDeleteAsync()
    {
        // Arrange
        var name = "TestValue1045637158";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.DeleteAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the DeleteAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallDeleteAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.DeleteAsync(value, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the ExistsAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallExistsAsync()
    {
        // Arrange
        var name = "TestValue934137246";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.ExistsAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ExistsAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallExistsAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.ExistsAsync(value, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the GetAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetAsync()
    {
        // Arrange
        var name = "TestValue1391797245";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.GetAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetAsync(value, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the GetOrNullAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetOrNullAsync()
    {
        // Arrange
        var name = "TestValue379006941";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.GetOrNullAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetOrNullAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetOrNullAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetOrNullAsync(value, CancellationToken.None));
    }
}

/// <summary>
/// Unit tests for the type <see cref="BlobContainer"/>.
/// </summary>
[TestClass]
public class BlobContainerTests
{
    private BlobContainer _testClass;
    private string _containerName;
    private BlobContainerConfiguration _configuration;
    private IBlobProvider _provider;
    private IBlobNormalizeNamingService _blobNormalizeNamingService;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobContainer"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._containerName = "TestValue2054960235";
        this._configuration = new BlobContainerConfiguration(default(BlobContainerConfiguration));
        this._provider = Substitute.For<IBlobProvider>();
        this._blobNormalizeNamingService = Substitute.For<IBlobNormalizeNamingService>();
        this._testClass = new BlobContainer(this._containerName, this._configuration, this._provider, this._blobNormalizeNamingService);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobContainer(this._containerName, this._configuration, this._provider, this._blobNormalizeNamingService);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the configuration parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullConfiguration()
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainer(this._containerName, default(BlobContainerConfiguration), this._provider, this._blobNormalizeNamingService));
    }

    /// <summary>
    /// Checks that instance construction throws when the provider parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullProvider()
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainer(this._containerName, this._configuration, default(IBlobProvider), this._blobNormalizeNamingService));
    }

    /// <summary>
    /// Checks that the constructor throws when the containerName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidContainerName(string value)
    {
        Should.Throw<ArgumentNullException>(() => new BlobContainer(value, this._configuration, this._provider, this._blobNormalizeNamingService));
    }

    /// <summary>
    /// Checks that the SaveAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveAsync()
    {
        // Arrange
        var name = "TestValue586794358";
        var stream = new MemoryStream();
        var overrideExisting = true;
        var cancellationToken = CancellationToken.None;

        // Act
        await this._testClass.SaveAsync(name, stream, overrideExisting, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SaveAsync method throws when the stream parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSaveAsyncWithNullStreamAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SaveAsync("TestValue1243623559", default(Stream), true, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the SaveAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallSaveAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SaveAsync(value, new MemoryStream(), false, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the DeleteAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallDeleteAsync()
    {
        // Arrange
        var name = "TestValue97260268";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.DeleteAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the DeleteAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallDeleteAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.DeleteAsync(value, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the ExistsAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallExistsAsync()
    {
        // Arrange
        var name = "TestValue1415128402";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.ExistsAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ExistsAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallExistsAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.ExistsAsync(value, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the GetAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetAsync()
    {
        // Arrange
        var name = "TestValue1129164909";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.GetAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetAsync(value, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the GetOrNullAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallGetOrNullAsync()
    {
        // Arrange
        var name = "TestValue1693451171";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.GetOrNullAsync(name, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetOrNullAsync method throws when the name parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallGetOrNullAsyncWithInvalidNameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.GetOrNullAsync(value, CancellationToken.None));
    }

    /// <summary>
    /// Checks that the ContainerName property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ContainerNameIsInitializedCorrectly()
    {
        this._testClass.ContainerName.ShouldBe(this._containerName);
    }

    /// <summary>
    /// Checks that the ContainerName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContainerName()
    {
        // Arrange
        var testValue = "TestValue330946543";

        // Act
        this._testClass.ContainerName = testValue;

        // Assert
        this._testClass.ContainerName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Configuration property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ConfigurationIsInitializedCorrectly()
    {
        this._testClass.Configuration.ShouldBeSameAs(this._configuration);
    }

    /// <summary>
    /// Checks that the Provider property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void ProviderIsInitializedCorrectly()
    {
        this._testClass.Provider.ShouldBeSameAs(this._provider);
    }

    /// <summary>
    /// Checks that the CancellationTokenProvider property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetCancellationTokenProvider()
    {
        // Assert
        this._testClass.CancellationTokenProvider.ShouldBeOfType<CancellationToken>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the BlobNormalizeNamingService property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void BlobNormalizeNamingServiceIsInitializedCorrectly()
    {
        this._testClass.BlobNormalizeNamingService.ShouldBeSameAs(this._blobNormalizeNamingService);
    }
}