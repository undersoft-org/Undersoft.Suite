using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Tests.Contracts.Details;

/// <summary>
/// Unit tests for the type <see cref="Response"/>.
/// </summary>
[TestClass]
public class ResponseTests
{
    private Response _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="Response"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new Response();
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new Response();

        // Assert
        instance.ShouldNotBeNull();
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
    /// Checks that setting the Description property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetDescription()
    {
        this._testClass.CheckProperty(x => x.Description);
    }

    /// <summary>
    /// Checks that setting the IssuerName property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetIssuerName()
    {
        this._testClass.CheckProperty(x => x.IssuerName);
    }

    /// <summary>
    /// Checks that setting the CreationDate property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCreationDate()
    {
        this._testClass.CheckProperty(x => x.CreationDate, DateTime.UtcNow, DateTime.UtcNow);
    }

    /// <summary>
    /// Checks that setting the ExpirationDate property correctly raises PropertyChanged events.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetExpirationDate()
    {
        this._testClass.CheckProperty(x => x.ExpirationDate, DateTime.UtcNow, DateTime.UtcNow);
    }
}