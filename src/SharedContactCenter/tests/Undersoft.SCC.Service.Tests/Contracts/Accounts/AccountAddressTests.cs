using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Contracts.Accounts;

namespace Undersoft.SCC.Service.Tests.Contracts.Accounts;

/// <summary>
/// Unit tests for the type <see cref="AccountAddress"/>.
/// </summary>
[TestClass]
public class AccountAddressTests
{
    private AccountAddress _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="AccountAddress"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new AccountAddress();
    }

    /// <summary>
    /// Checks that the Country property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCountry()
    {
        // Arrange
        var testValue = "TestValue1126611231";

        // Act
        this._testClass.Country = testValue;

        // Assert
        this._testClass.Country.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the State property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetState()
    {
        // Arrange
        var testValue = "TestValue238745265";

        // Act
        this._testClass.State = testValue;

        // Assert
        this._testClass.State.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the City property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetCity()
    {
        // Arrange
        var testValue = "TestValue614986912";

        // Act
        this._testClass.City = testValue;

        // Assert
        this._testClass.City.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Postcode property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPostcode()
    {
        // Arrange
        var testValue = "TestValue1478484059";

        // Act
        this._testClass.Postcode = testValue;

        // Assert
        this._testClass.Postcode.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Street property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetStreet()
    {
        // Arrange
        var testValue = "TestValue1622369720";

        // Act
        this._testClass.Street = testValue;

        // Assert
        this._testClass.Street.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Building property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBuilding()
    {
        // Arrange
        var testValue = "TestValue1159463953";

        // Act
        this._testClass.Building = testValue;

        // Assert
        this._testClass.Building.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Apartment property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetApartment()
    {
        // Arrange
        var testValue = "TestValue448974858";

        // Act
        this._testClass.Apartment = testValue;

        // Assert
        this._testClass.Apartment.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the AccountId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetAccountId()
    {
        // Arrange
        var testValue = 547847818L;

        // Act
        this._testClass.AccountId = testValue;

        // Assert
        this._testClass.AccountId.ShouldBe(testValue);
    }
}