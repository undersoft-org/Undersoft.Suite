using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Tests.Data.Store;

/// <summary>
/// Unit tests for the type <see cref="DataStoreSchema"/>.
/// </summary>
[TestClass]
public class DataStoreSchemaTests
{
    /// <summary>
    /// Checks that the DomainSchema property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetDomainSchema()
    {
        // Assert
        DataStoreSchema.DomainSchema.ShouldBeOfType<string>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the RemoteSchema property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetRemoteSchema()
    {
        // Assert
        DataStoreSchema.RemoteSchema.ShouldBeOfType<string>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the IdentifierSchema property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetIdentifierSchema()
    {
        // Assert
        DataStoreSchema.IdentifierSchema.ShouldBeOfType<string>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the RelationSchema property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetRelationSchema()
    {
        // Assert
        DataStoreSchema.RelationSchema.ShouldBeOfType<string>();

        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the PropertySchema property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetPropertySchema()
    {
        // Assert
        DataStoreSchema.PropertySchema.ShouldBeOfType<string>();

        Assert.Fail("Create or modify test");
    }
}