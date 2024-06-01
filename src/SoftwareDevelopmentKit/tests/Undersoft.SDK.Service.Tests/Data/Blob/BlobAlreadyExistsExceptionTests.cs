using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobAlreadyExistsException"/>.
/// </summary>
[TestClass]
public class BlobAlreadyExistsExceptionTests
{
    private BlobAlreadyExistsException _testClass;
    private string _message;
    private Exception _innerException;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobAlreadyExistsException"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._message = "TestValue107828642";
        this._innerException = new Exception();
        this._testClass = new BlobAlreadyExistsException(this._message, this._innerException);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new BlobAlreadyExistsException();

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new BlobAlreadyExistsException(this._message);

        // Assert
        instance.ShouldNotBeNull();

        // Act
        instance = new BlobAlreadyExistsException(this._message, this._innerException);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the innerException parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullInnerException()
    {
        Should.Throw<ArgumentNullException>(() => new BlobAlreadyExistsException(this._message, default(Exception)));
    }

    /// <summary>
    /// Checks that the constructor throws when the message parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotConstructWithInvalidMessage(string value)
    {
        Should.Throw<ArgumentNullException>(() => new BlobAlreadyExistsException(value));
        Should.Throw<ArgumentNullException>(() => new BlobAlreadyExistsException(value, this._innerException));
    }
}