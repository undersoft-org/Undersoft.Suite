using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Invoking;
using Undersoft.SDK.Service.Data.Repository.Client;
using Undersoft.SDK.Service.Data.Repository.Source;
using R = System.String;
using T = System.String;
using TResponse = System.String;

namespace Undersoft.SDK.Service.Tests;

/// <summary>
/// Unit tests for the type <see cref="Servicer"/>.
/// </summary>
[TestClass]
public class ServicerTests
{
    private Servicer _testClass;
    private IServiceManager _serviceManager;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Servicer"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._serviceManager = Substitute.For<IServiceManager>();
        this._testClass = new Servicer(this._serviceManager);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Servicer();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new Servicer(this._serviceManager);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the serviceManager parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullServiceManager()
    {
        Should.Throw<ArgumentNullException>(() => new Servicer(default(IServiceManager)));
    }

    /// <summary>
    /// Checks that the CreateStream method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreateStreamWithIStreamRequestOfTResponseAndCancellationToken()
    {
        // Arrange
        var request = Substitute.For<IStreamRequest<TResponse>>();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = this._testClass.CreateStream<TResponse>(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CreateStream method throws when the request parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCreateStreamWithIStreamRequestOfTResponseAndCancellationTokenWithNullRequest()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.CreateStream<TResponse>(default(IStreamRequest<TResponse>), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the CreateStream method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallCreateStreamWithObjectAndCancellationToken()
    {
        // Arrange
        var request = new object();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = this._testClass.CreateStream(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the CreateStream method throws when the request parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallCreateStreamWithObjectAndCancellationTokenWithNullRequest()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.CreateStream(default(object), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the LazyServe method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallLazyServe()
    {
        // Arrange
        Func<T, R> function = x => "TestValue466948028";

        // Act
        var result = this._testClass.LazyServe<T, R>(function);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the LazyServe method throws when the function parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallLazyServeWithNullFunction()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.LazyServe<T, R>(default(Func<T, R>)));
    }

    /// <summary>
    /// Checks that the Run method throws when the function parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallRunWithTAndRAndFuncOfTAndTaskOfRWithNullFunctionAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Run<T, R>(default(Func<T, Task<R>>)));
    }

    /// <summary>
    /// Checks that the Run method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallRunWithTAndFuncOfTAndTaskAsync()
    {
        // Arrange
        Func<T, Task> function = x => Task.CompletedTask;

        // Act
        await this._testClass.Run<T>(function);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Run method throws when the function parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallRunWithTAndFuncOfTAndTaskWithNullFunctionAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Run<T>(default(Func<T, Task>)));
    }

    /// <summary>
    /// Checks that the Run method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallRunWithTAndStringAndArrayOfObjectAsync()
    {
        // Arrange
        var methodname = "TestValue1229894453";
        var parameters = new[] { new object(), new object(), new object() };

        // Act
        var result = await this._testClass.Run<T>(methodname, parameters);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Run method throws when the parameters parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallRunWithTAndStringAndArrayOfObjectWithNullParametersAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Run<T>("TestValue1479920727", default(object[])));
    }

    /// <summary>
    /// Checks that the Run method throws when the methodname parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallRunWithTAndStringAndArrayOfObjectWithInvalidMethodnameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Run<T>(value, new[] { new object(), new object(), new object() }));
    }

    /// <summary>
    /// Checks that the Run method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallRunWithTAndRAndStringAndArrayOfObjectAsync()
    {
        // Arrange
        var methodname = "TestValue121685591";
        var parameters = new[] { new object(), new object(), new object() };

        // Act
        var result = await this._testClass.Run<T, R>(methodname, parameters);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Run method throws when the parameters parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallRunWithTAndRAndStringAndArrayOfObjectWithNullParametersAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Run<T, R>("TestValue2094269141", default(object[])));
    }

    /// <summary>
    /// Checks that the Run method throws when the methodname parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallRunWithTAndRAndStringAndArrayOfObjectWithInvalidMethodnameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Run<T, R>(value, new[] { new object(), new object(), new object() }));
    }

    /// <summary>
    /// Checks that the Run method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallRunWithTAndArgumentsAsync()
    {
        // Arrange
        var arguments = new Arguments();

        // Act
        var result = await this._testClass.Run<T>(arguments);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Run method throws when the arguments parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallRunWithTAndArgumentsWithNullArgumentsAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Run<T>(default(Arguments)));
    }

    /// <summary>
    /// Checks that the Run method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallRunWithTAndRAndArgumentsAsync()
    {
        // Arrange
        var arguments = new Arguments();

        // Act
        var result = await this._testClass.Run<T, R>(arguments);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Run method throws when the arguments parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallRunWithTAndRAndArgumentsWithNullArgumentsAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Run<T, R>(default(Arguments)));
    }

    /// <summary>
    /// Checks that the Send method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSendWithTResponseAndIRequestOfTResponseAndCancellationTokenAsync()
    {
        // Arrange
        var request = Substitute.For<IRequest<TResponse>>();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.Send<TResponse>(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Send method throws when the request parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSendWithTResponseAndIRequestOfTResponseAndCancellationTokenWithNullRequestAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Send<TResponse>(default(IRequest<TResponse>), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Send method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSendWithObjectAndCancellationTokenAsync()
    {
        // Arrange
        var request = new object();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.Send(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Send method throws when the request parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSendWithObjectAndCancellationTokenWithNullRequestAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Send(default(object), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Publish method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallPublishWithObjectAndCancellationTokenAsync()
    {
        // Arrange
        var notification = new object();
        var cancellationToken = CancellationToken.None;

        // Act
        await this._testClass.Publish(notification, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Publish method throws when the notification parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallPublishWithObjectAndCancellationTokenWithNullNotificationAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Publish(default(object), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Report method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallReportWithTResponseAndIRequestOfTResponseAndCancellationTokenAsync()
    {
        // Arrange
        var request = Substitute.For<IRequest<TResponse>>();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.Report<TResponse>(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Report method throws when the request parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallReportWithTResponseAndIRequestOfTResponseAndCancellationTokenWithNullRequestAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Report<TResponse>(default(IRequest<TResponse>), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Report method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallReportWithObjectAndCancellationTokenAsync()
    {
        // Arrange
        var request = new object();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.Report(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Report method throws when the request parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallReportWithObjectAndCancellationTokenWithNullRequestAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Report(default(object), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Entry method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallEntryWithTResponseAndIRequestOfTResponseAndCancellationTokenAsync()
    {
        // Arrange
        var request = Substitute.For<IRequest<TResponse>>();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.Entry<TResponse>(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Entry method throws when the request parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallEntryWithTResponseAndIRequestOfTResponseAndCancellationTokenWithNullRequestAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Entry<TResponse>(default(IRequest<TResponse>), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Entry method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallEntryWithObjectAndCancellationTokenAsync()
    {
        // Arrange
        var request = new object();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.Entry(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Entry method throws when the request parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallEntryWithObjectAndCancellationTokenWithNullRequestAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Entry(default(object), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Perform method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallPerformWithTResponseAndIRequestOfTResponseAndCancellationTokenAsync()
    {
        // Arrange
        var request = Substitute.For<IRequest<TResponse>>();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.Perform<TResponse>(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Perform method throws when the request parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallPerformWithTResponseAndIRequestOfTResponseAndCancellationTokenWithNullRequestAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Perform<TResponse>(default(IRequest<TResponse>), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Perform method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallPerformWithObjectAndCancellationTokenAsync()
    {
        // Arrange
        var request = new object();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.Perform(request, cancellationToken);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Perform method throws when the request parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallPerformWithObjectAndCancellationTokenWithNullRequestAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Perform(default(object), CancellationToken.None));
    }

    /// <summary>
    /// Checks that the Serve method throws when the function parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallServeWithTAndRAndFuncOfTAndTaskOfRWithNullFunctionAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T, R>(default(Func<T, Task<R>>)));
    }

    /// <summary>
    /// Checks that the Serve method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallServeWithTAndFuncOfTAndTaskAsync()
    {
        // Arrange
        Func<T, Task> function = x => Task.CompletedTask;

        // Act
        await this._testClass.Serve<T>(function);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Serve method throws when the function parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallServeWithTAndFuncOfTAndTaskWithNullFunctionAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T>(default(Func<T, Task>)));
    }

    /// <summary>
    /// Checks that the Serve method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallServeWithTAndStringAndArrayOfObjectAsync()
    {
        // Arrange
        var methodname = "TestValue775800465";
        var parameters = new[] { new object(), new object(), new object() };

        // Act
        await this._testClass.Serve<T>(methodname, parameters);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Serve method throws when the parameters parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallServeWithTAndStringAndArrayOfObjectWithNullParametersAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T>("TestValue1466174480", default(object[])));
    }

    /// <summary>
    /// Checks that the Serve method throws when the methodname parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallServeWithTAndStringAndArrayOfObjectWithInvalidMethodnameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T>(value, new[] { new object(), new object(), new object() }));
    }

    /// <summary>
    /// Checks that the Serve method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallServeWithTAndRAndStringAndArrayOfObjectAsync()
    {
        // Arrange
        var methodname = "TestValue598284610";
        var parameters = new[] { new object(), new object(), new object() };

        // Act
        var result = await this._testClass.Serve<T, R>(methodname, parameters);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Serve method throws when the parameters parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallServeWithTAndRAndStringAndArrayOfObjectWithNullParametersAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T, R>("TestValue1438729150", default(object[])));
    }

    /// <summary>
    /// Checks that the Serve method throws when the methodname parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallServeWithTAndRAndStringAndArrayOfObjectWithInvalidMethodnameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T, R>(value, new[] { new object(), new object(), new object() }));
    }

    /// <summary>
    /// Checks that the Serve method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallServeWithTAndStringAndArgumentsAsync()
    {
        // Arrange
        var methodname = "TestValue284915489";
        var arguments = new Arguments();

        // Act
        await this._testClass.Serve<T>(methodname, arguments);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Serve method throws when the arguments parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallServeWithTAndStringAndArgumentsWithNullArgumentsAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T>("TestValue1961902777", default(Arguments)));
    }

    /// <summary>
    /// Checks that the Serve method throws when the methodname parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallServeWithTAndStringAndArgumentsWithInvalidMethodnameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T>(value, new Arguments()));
    }

    /// <summary>
    /// Checks that the Serve method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallServeWithTAndRAndStringAndArgumentsAsync()
    {
        // Arrange
        var methodname = "TestValue549718524";
        var arguments = new Arguments();

        // Act
        var result = await this._testClass.Serve<T, R>(methodname, arguments);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Serve method throws when the arguments parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallServeWithTAndRAndStringAndArgumentsWithNullArgumentsAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T, R>("TestValue888418596", default(Arguments)));
    }

    /// <summary>
    /// Checks that the Serve method throws when the methodname parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    /// <returns>A task that represents the running test.</returns>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task CannotCallServeWithTAndRAndStringAndArgumentsWithInvalidMethodnameAsync(string value)
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.Serve<T, R>(value, new Arguments()));
    }

    /// <summary>
    /// Checks that the Save method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveAsync()
    {
        // Arrange
        var asTransaction = false;

        // Act
        await this._testClass.Save(asTransaction);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SaveClient method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveClientAsync()
    {
        // Arrange
        var client = Substitute.For<IRepositoryClient>();
        var asTransaction = false;

        // Act
        var result = await this._testClass.SaveClient(client, asTransaction);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SaveClient method throws when the client parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSaveClientWithNullClientAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SaveClient(default(IRepositoryClient), false));
    }

    /// <summary>
    /// Checks that the SaveClients method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveClientsAsync()
    {
        // Arrange
        var asTransaction = true;

        // Act
        var result = await this._testClass.SaveClients(asTransaction);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SaveStore method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveStoreAsync()
    {
        // Arrange
        var source = Substitute.For<IRepositorySource>();
        var asTransaction = false;

        // Act
        var result = await this._testClass.SaveStore(source, asTransaction);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the SaveStore method throws when the source parameter is null.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CannotCallSaveStoreWithNullSourceAsync()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => this._testClass.SaveStore(default(IRepositorySource), false));
    }

    /// <summary>
    /// Checks that the SaveStores method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveStoresAsync()
    {
        // Arrange
        var asTransaction = true;

        // Act
        var result = await this._testClass.SaveStores(asTransaction);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the DisposeAsyncCore method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallDisposeAsyncCoreAsync()
    {
        // Act
        await this._testClass.DisposeAsyncCore();

        // Assert
        Assert.Fail("Create or modify test");
    }
}