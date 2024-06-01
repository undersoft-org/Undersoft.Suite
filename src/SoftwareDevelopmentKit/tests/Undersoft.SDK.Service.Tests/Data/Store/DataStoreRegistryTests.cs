using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Store;
using TEntity = System.String;
using TOrigin = System.String;
using TTarget = System.String;

namespace Undersoft.SDK.Service.Tests.Data.Store;

/// <summary>
/// Unit tests for the type <see cref="DataStoreRegistry"/>.
/// </summary>
[TestClass]
public class DataStoreRegistryTests
{
    /// <summary>
    /// Checks that the GetEntityProperties method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntityPropertiesWithIDataStoreContext()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();

        // Act
        var result = context.GetEntityProperties();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntityProperties method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntityPropertiesWithIDataStoreContextWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetEntityProperties());
    }

    /// <summary>
    /// Checks that the GetEntityProperties method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntityPropertiesWithType()
    {
        // Arrange
        var contextType = typeof(string);

        // Act
        var result = DataStoreRegistry.GetEntityProperties(contextType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntityProperties method throws when the contextType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntityPropertiesWithTypeWithNullContextType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetEntityProperties(default(Type)));
    }

    /// <summary>
    /// Checks that the GetEntityTypes method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntityTypesWithIDataStoreContext()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();

        // Act
        var result = context.GetEntityTypes();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntityTypes method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntityTypesWithIDataStoreContextWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetEntityTypes());
    }

    /// <summary>
    /// Checks that the GetEntityTypes method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntityTypesWithType()
    {
        // Arrange
        var contextType = typeof(string);

        // Act
        var result = DataStoreRegistry.GetEntityTypes(contextType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntityTypes method throws when the contextType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntityTypesWithTypeWithNullContextType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetEntityTypes(default(Type)));
    }

    /// <summary>
    /// Checks that the GetRemoteType method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRemoteType()
    {
        // Arrange
        var remoteName = "TestValue904469798";

        // Act
        var result = DataStoreRegistry.GetRemoteType(remoteName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRemoteType method throws when the remoteName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetRemoteTypeWithInvalidRemoteName(string value)
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetRemoteType(value));
    }

    /// <summary>
    /// Checks that the GetRemoteMembers method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRemoteMembersWithIDataStoreContext()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();

        // Act
        var result = context.GetRemoteMembers();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRemoteMembers method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetRemoteMembersWithIDataStoreContextWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetRemoteMembers());
    }

    /// <summary>
    /// Checks that the GetStoreType method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetStoreTypeWithIDataStoreContext()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();

        // Act
        var result = context.GetStoreType();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetStoreType method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetStoreTypeWithIDataStoreContextWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetStoreType());
    }

    /// <summary>
    /// Checks that the GetStoreType method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetStoreTypeWithType()
    {
        // Arrange
        var contextType = typeof(string);

        // Act
        var result = DataStoreRegistry.GetStoreType(contextType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetStoreType method throws when the contextType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetStoreTypeWithTypeWithNullContextType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetStoreType(default(Type)));
    }

    /// <summary>
    /// Checks that the GetIndentityProperties method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetIndentityPropertiesWithIDataStoreContext()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();

        // Act
        var result = context
        .GetIndentityProperties();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetIndentityProperties method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetIndentityPropertiesWithIDataStoreContextWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetIndentityProperties());
    }

    /// <summary>
    /// Checks that the GetIdentityProperty method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetIdentityPropertyWithEntityTypeAndIdentityType()
    {
        // Arrange
        var entityType = typeof(string);
        var identityType = DbIdentityType.PrimaryKey;

        // Act
        var result = DataStoreRegistry.GetIdentityProperty(entityType, identityType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetIdentityProperty method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetIdentityPropertyWithEntityTypeAndIdentityTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetIdentityProperty(default(Type), DbIdentityType.PrimaryKey));
    }

    /// <summary>
    /// Checks that the GetIdentityProperty method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetIdentityPropertyWithEntityAndIdentityType()
    {
        // Arrange
        var entity = Substitute.For<IIdentifiable>();
        var identityType = DbIdentityType.Index;

        // Act
        var result = entity.GetIdentityProperty(identityType
        );

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetIdentityProperty method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetIdentityPropertyWithEntityAndIdentityTypeWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => default(IIdentifiable).GetIdentityProperty(DbIdentityType.ForeignKey));
    }

    /// <summary>
    /// Checks that the GetIndentityProperties method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetIndentityPropertiesWithIOrigin()
    {
        // Arrange
        var entity = Substitute.For<IOrigin>();

        // Act
        var result = entity.GetIndentityProperties();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetIndentityProperties method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetIndentityPropertiesWithIOriginWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => default(IOrigin).GetIndentityProperties());
    }

    /// <summary>
    /// Checks that the GetIndentityProperties method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetIndentityPropertiesWithIIdentifiable()
    {
        // Arrange
        var entity = Substitute.For<IIdentifiable>();

        // Act
        var result = entity.GetIndentityProperties();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetIndentityProperties method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetIndentityPropertiesWithIIdentifiableWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => default(IIdentifiable).GetIndentityProperties());
    }

    /// <summary>
    /// Checks that the GetIndentityProperties method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetIndentityPropertiesWithType()
    {
        // Arrange
        var entityType = typeof(string);

        // Act
        var result = DataStoreRegistry.GetIndentityProperties(entityType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetIndentityProperties method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetIndentityPropertiesWithTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetIndentityProperties(default(Type)));
    }

    /// <summary>
    /// Checks that the GetProperty method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetPropertyWithIDataStoreContextAndString()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();
        var entityTypeName = "TestValue1315929440";

        // Act
        var result = context.GetProperty(entityTypeName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetProperty method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetPropertyWithIDataStoreContextAndStringWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetProperty("TestValue781494953"));
    }

    /// <summary>
    /// Checks that the GetProperty method throws when the entityTypeName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetPropertyWithIDataStoreContextAndStringWithInvalidEntityTypeName(string value)
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IDataStoreContext>().GetProperty(value));
    }

    /// <summary>
    /// Checks that the GetProperty method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetPropertyWithIDataStoreContextAndType()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();
        var entityType = typeof(string);

        // Act
        var result = context.GetProperty(entityType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetProperty method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetPropertyWithIDataStoreContextAndTypeWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetProperty(typeof(string)));
    }

    /// <summary>
    /// Checks that the GetProperty method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetPropertyWithIDataStoreContextAndTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IDataStoreContext>().GetProperty(default(Type)));
    }

    /// <summary>
    /// Checks that the GetProperty maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void GetPropertyWithIDataStoreContextAndTypePerformsMapping()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();
        var entityType = typeof(string);

        // Act
        var result = context.GetProperty(entityType);

        // Assert
        result.IsSpecialName.ShouldBe(entityType.IsSpecialName);
        result.MemberType.ShouldBe(entityType.MemberType);
    }

    /// <summary>
    /// Checks that the GetProperty method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetPropertyWithTEntityAndIDataStoreContext()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();

        // Act
        var result = context.GetProperty<TEntity>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetProperty method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetPropertyWithTEntityAndIDataStoreContextWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetProperty<TEntity>());
    }

    /// <summary>
    /// Checks that the GetEntityType method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntityTypeWithIDataStoreContextAndString()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();
        var entityTypeName = "TestValue821915485";

        // Act
        var result = context.GetEntityType(entityTypeName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntityType method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntityTypeWithIDataStoreContextAndStringWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetEntityType("TestValue1512664184"));
    }

    /// <summary>
    /// Checks that the GetEntityType method throws when the entityTypeName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetEntityTypeWithIDataStoreContextAndStringWithInvalidEntityTypeName(string value)
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IDataStoreContext>().GetEntityType(value));
    }

    /// <summary>
    /// Checks that the GetEntityType method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntityTypeWithIDataStoreContextAndType()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();
        var entityType = typeof(string);

        // Act
        var result = context.GetEntityType(entityType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntityType method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntityTypeWithIDataStoreContextAndTypeWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetEntityType(typeof(string)));
    }

    /// <summary>
    /// Checks that the GetEntityType method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntityTypeWithIDataStoreContextAndTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IDataStoreContext>().GetEntityType(default(Type)));
    }

    /// <summary>
    /// Checks that the GetEntityType maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void GetEntityTypeWithIDataStoreContextAndTypePerformsMapping()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();
        var entityType = typeof(string);

        // Act
        var result = context.GetEntityType(entityType);

        // Assert
        result.BaseType.ShouldBeSameAs(entityType.BaseType);
    }

    /// <summary>
    /// Checks that the GetEntityType method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntityTypeWithTEntityAndIDataStoreContext()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();

        // Act
        var result = context.GetEntityType<TEntity>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntityType method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntityTypeWithTEntityAndIDataStoreContextWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetEntityType<TEntity>());
    }

    /// <summary>
    /// Checks that the GetRemoteMembers method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRemoteMembersWithType()
    {
        // Arrange
        var entityType = typeof(string);

        // Act
        var result = DataStoreRegistry.GetRemoteMembers(entityType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRemoteMembers method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetRemoteMembersWithTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetRemoteMembers(default(Type)));
    }

    /// <summary>
    /// Checks that the GetRemoteMembers method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRemoteMembersWithTOrigin()
    {
        // Act
        var result = DataStoreRegistry.GetRemoteMembers<TOrigin>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRemoteMember method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRemoteMemberWithEntityTypeAndTargetType()
    {
        // Arrange
        var entityType = typeof(string);
        var targetType = typeof(string);

        // Act
        var result = DataStoreRegistry.GetRemoteMember(entityType, targetType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetRemoteMember method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetRemoteMemberWithEntityTypeAndTargetTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetRemoteMember(default(Type), typeof(string)));
    }

    /// <summary>
    /// Checks that the GetRemoteMember method throws when the targetType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetRemoteMemberWithEntityTypeAndTargetTypeWithNullTargetType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetRemoteMember(typeof(string), default(Type)));
    }

    /// <summary>
    /// Checks that the GetRemoteMember maps values from the input to the returned instance.
    /// </summary>
    [TestMethod]
    public void GetRemoteMemberWithEntityTypeAndTargetTypePerformsMapping()
    {
        // Arrange
        var entityType = typeof(string);
        var targetType = typeof(string);

        // Act
        var result = DataStoreRegistry.GetRemoteMember(entityType, targetType);

        // Assert
        result.DeclaringType.ShouldBeSameAs(entityType.DeclaringType);
        result.MemberType.ShouldBe(entityType.MemberType);
        result.ReflectedType.ShouldBeSameAs(entityType.ReflectedType);
        result.DeclaringType.ShouldBeSameAs(targetType.DeclaringType);
        result.MemberType.ShouldBe(targetType.MemberType);
        result.ReflectedType.ShouldBeSameAs(targetType.ReflectedType);
    }

    /// <summary>
    /// Checks that the GetRemoteMember method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetRemoteMemberWithTOriginAndTTarget()
    {
        // Act
        var result = DataStoreRegistry.GetRemoteMember<TOrigin, TTarget>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntitySet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntitySetWithIDataStoreContextAndString()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();
        var entityTypeName = "TestValue1850529051";

        // Act
        var result = context.GetEntitySet(entityTypeName);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntitySet method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntitySetWithIDataStoreContextAndStringWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetEntitySet("TestValue352416714"));
    }

    /// <summary>
    /// Checks that the GetEntitySet method throws when the entityTypeName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetEntitySetWithIDataStoreContextAndStringWithInvalidEntityTypeName(string value)
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IDataStoreContext>().GetEntitySet(value));
    }

    /// <summary>
    /// Checks that the GetEntitySet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetEntitySetWithIDataStoreContextAndType()
    {
        // Arrange
        var context = Substitute.For<IDataStoreContext>();
        var entityType = typeof(string);

        // Act
        var result = context.GetEntitySet(entityType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetEntitySet method throws when the context parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntitySetWithIDataStoreContextAndTypeWithNullContext()
    {
        Should.Throw<ArgumentNullException>(() => default(IDataStoreContext).GetEntitySet(typeof(string)));
    }

    /// <summary>
    /// Checks that the GetEntitySet method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetEntitySetWithIDataStoreContextAndTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => Substitute.For<IDataStoreContext>().GetEntitySet(default(Type)));
    }

    /// <summary>
    /// Checks that the GetContext method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetContextWithStoreTypeAndEntityType()
    {
        // Arrange
        var storeType = typeof(string);
        var entityType = typeof(string);

        // Act
        var result = DataStoreRegistry.GetContext(storeType, entityType);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetContext method throws when the storeType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetContextWithStoreTypeAndEntityTypeWithNullStoreType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetContext(default(Type), typeof(string)));
    }

    /// <summary>
    /// Checks that the GetContext method throws when the entityType parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallGetContextWithStoreTypeAndEntityTypeWithNullEntityType()
    {
        Should.Throw<ArgumentNullException>(() => DataStoreRegistry.GetContext(typeof(string), default(Type)));
    }

}