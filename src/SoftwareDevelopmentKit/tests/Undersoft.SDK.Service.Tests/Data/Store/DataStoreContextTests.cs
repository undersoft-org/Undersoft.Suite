using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Service.Data.Store;
using TContext = Microsoft.EntityFrameworkCore.DbContext;
using TEntity = Undersoft.SDK.Service.Data.Entity.IEntity;
using TStore = Undersoft.SDK.Service.Data.Store.IDataStore;

namespace Undersoft.SDK.Service.Tests.Data.Store;

/// <summary>
/// Unit tests for the type <see cref="DataStoreContext"/>.
/// </summary>
[TestClass]
public class DataStoreContext_1Tests
{
    private DataStoreContext<TStore> _testClass;
    private DbContextOptions _options;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="DataStoreContext"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = new DbContextOptions<TContext>();
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new DataStoreContext<TStore>(this._options, this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new DataStoreContext<TStore>(this._options, this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new DataStoreContext<TStore>(default(DbContextOptions), this._servicer));
    }
}

/// <summary>
/// Unit tests for the type <see cref="DataStoreContext"/>.
/// </summary>
[TestClass]
public class DataStoreContextTests
{
    private DataStoreContext _testClass;
    private DbContextOptions _options;
    private IServicer _servicer;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="DataStoreContext"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = new DbContextOptions<TContext>();
        this._servicer = Substitute.For<IServicer>();
        this._testClass = new DataStoreContext(this._options, this._servicer);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new DataStoreContext(this._options, this._servicer);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new DataStoreContext(default(DbContextOptions), this._servicer));
    }

    /// <summary>
    /// Checks that the Query method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallQuery()
    {
        // Act
        var result = this._testClass.Query<TEntity>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the EntitySet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallEntitySetWithTEntity()
    {
        // Act
        var result = this._testClass.EntitySet<TEntity>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the EntitySet method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallEntitySetWithType()
    {
        // Arrange
        var @type = typeof(string);

        // Act
        var result = this._testClass.EntitySet(type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the EntitySet method throws when the type parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallEntitySetWithTypeWithNullType()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.EntitySet(default(Type)));
    }

    /// <summary>
    /// Checks that the Add method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddWithTEntityAndTEntityWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Add<TEntity>(default(TEntity)));
    }

    /// <summary>
    /// Checks that the AttachProperty method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAttachProperty()
    {
        // Arrange
        var item = new object();
        var propertyName = "TestValue1235277681";
        var @type = typeof(string);

        // Act
        var result = this._testClass.AttachProperty(item, propertyName, type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AttachProperty method throws when the item parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAttachPropertyWithNullItem()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AttachProperty(default(object), "TestValue1954973659", typeof(string)));
    }

    /// <summary>
    /// Checks that the AttachProperty method throws when the propertyName parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallAttachPropertyWithInvalidPropertyName(string value)
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AttachProperty(new object(), value, typeof(string)));
    }

    /// <summary>
    /// Checks that the Add method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddWithObject()
    {
        // Arrange
        var entity = new object();

        // Act
        var result = this._testClass.Add(entity);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Add method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAddWithObjectWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Add(default(object)));
    }

    /// <summary>
    /// Checks that the Update method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallUpdateWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Update<TEntity>(default(TEntity)));
    }

    /// <summary>
    /// Checks that the Remove method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallRemoveWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Remove<TEntity>(default(TEntity)));
    }

    /// <summary>
    /// Checks that the Attach method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAttachWithObject()
    {
        // Arrange
        var entity = new object();

        // Act
        var result = this._testClass.Attach(entity);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the Attach method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAttachWithObjectWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Attach(default(object)));
    }

    /// <summary>
    /// Checks that the Attach method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAttachWithTEntityAndTEntityWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.Attach<TEntity>(default(TEntity)));
    }

    /// <summary>
    /// Checks that the Save method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallSaveAsync()
    {
        // Arrange
        var asTransaction = true;
        var token = CancellationToken.None;

        // Act
        var result = await this._testClass.Save(asTransaction, token);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the servicer property is initialized correctly by the constructor.
    /// </summary>
    [TestMethod]
    public void servicerIsInitializedCorrectly()
    {
        this._testClass.servicer.ShouldBeSameAs(this._servicer);
    }

    /// <summary>
    /// Checks that the Model property can be read from.
    /// </summary>
    [TestMethod]
    public void CanGetModel()
    {
        // Assert
        this._testClass.Model.ShouldBeOfType<IModel>();

        Assert.Fail("Create or modify test");
    }
}