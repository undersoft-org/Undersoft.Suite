using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts;
using TContext = Microsoft.EntityFrameworkCore.DbContext;
using TEntity = System.String;
using TStore = Undersoft.SDK.Service.Data.Store.IDataStore;

namespace Undersoft.SDK.Service.Server.Tests.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountStore"/>.
/// </summary>
[TestClass]
public partial class AccountStore_2Tests
{
    private AccountStore<TStore, TContext> _testClass;
    private DbContextOptions<TContext> _options;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountStore"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = new DbContextOptions<TContext>();
        this._testClass = new AccountStore<TStore, TContext>(this._options);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountStore<TStore, TContext>(this._options);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new AccountStore<TStore, TContext>(default(DbContextOptions<TContext>)));
    }
}

/// <summary>
/// Unit tests for the type <see cref="AccountStoreContext"/>.
/// </summary>
[TestClass]
public partial class AccountStoreContext_1Tests
{
    private AccountStoreContext<TStore> _testClass;
    private DbContextOptions _options;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountStoreContext"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._options = new DbContextOptions<TContext>();
        this._testClass = new AccountStoreContext<TStore>(this._options);
    }

    /// <summary>
    /// Checks that instance construction works.
    /// </summary>
    [TestMethod]
    public void CanConstruct()
    {
        // Act
        var instance = new AccountStoreContext<TStore>(this._options);

        // Assert
        instance.ShouldNotBeNull();
    }

    /// <summary>
    /// Checks that instance construction throws when the options parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotConstructWithNullOptions()
    {
        Should.Throw<ArgumentNullException>(() => new AccountStoreContext<TStore>(default(DbContextOptions)));
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
    /// Checks that the Add method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAddWithTEntityAndTEntity()
    {
        // Arrange
        var entity = "TestValue1943701900";

        // Act
        var result = this._testClass.Add<TEntity>(entity);

        // Assert
        Assert.Fail("Create or modify test");
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
    /// Checks that the AddAsync method functions correctly.
    /// </summary>
    /// <returns>A task that represents the running test.</returns>
    [TestMethod]
    public async Task CanCallAddAsync()
    {
        // Arrange
        var entity = "TestValue1755188410";
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await this._testClass.AddAsync<TEntity>(entity, cancellationToken
        );

        // Assert
        Assert.Fail("Create or modify test");
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
    /// Checks that the Update method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallUpdate()
    {
        // Arrange
        var entity = "TestValue2131645821";

        // Act
        var result = this._testClass.Update<TEntity>(entity);

        // Assert
        Assert.Fail("Create or modify test");
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
    /// Checks that the Remove method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallRemove()
    {
        // Arrange
        var entity = "TestValue810663699";

        // Act
        var result = this._testClass.Remove<TEntity>(entity);

        // Assert
        Assert.Fail("Create or modify test");
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
    /// Checks that the Attach method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAttachWithTEntityAndTEntity()
    {
        // Arrange
        var entity = "TestValue216353235";

        // Act
        var result = this._testClass.Attach<TEntity>(entity);

        // Assert
        Assert.Fail("Create or modify test");
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
    /// Checks that the AttachProperty method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAttachProperty()
    {
        // Arrange
        var entity = new object();
        var propertyName = "TestValue840384596";
        var @type = typeof(string);

        // Act
        var result = this._testClass.AttachProperty(entity, propertyName, type);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AttachProperty method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAttachPropertyWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => this._testClass.AttachProperty(default(object), "TestValue208412809", typeof(string)));
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

}