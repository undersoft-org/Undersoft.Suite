using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SCC.Service.Application.ViewModels.Contacts;

namespace Undersoft.SCC.Service.Application.Tests.ViewModels.Contacts;

/// <summary>
/// Unit tests for the type <see cref="ContactPersonal"/>.
/// </summary>
[TestClass]
public class ContactPersonalTests
{
    private ContactPersonal _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="ContactPersonal"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new ContactPersonal();
    }

    /// <summary>
    /// Checks that the FirstName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetFirstName()
    {
        // Arrange
        var testValue = "TestValue851594848";

        // Act
        this._testClass.FirstName = testValue;

        // Assert
        this._testClass.FirstName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the LastName property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetLastName()
    {
        // Arrange
        var testValue = "TestValue1616907273";

        // Act
        this._testClass.LastName = testValue;

        // Assert
        this._testClass.LastName.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Email property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetEmail()
    {
        // Arrange
        var testValue = "TestValue883429534";

        // Act
        this._testClass.Email = testValue;

        // Assert
        this._testClass.Email.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PhoneNumber property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPhoneNumber()
    {
        // Arrange
        var testValue = "TestValue697390346";

        // Act
        this._testClass.PhoneNumber = testValue;

        // Assert
        this._testClass.PhoneNumber.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the Birthdate property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetBirthdate()
    {
        // Arrange
        var testValue = DateTime.UtcNow;

        // Act
        this._testClass.Birthdate = testValue;

        // Assert
        this._testClass.Birthdate.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PersonalImage property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalImage()
    {
        // Arrange
        var testValue = "TestValue1546563308";

        // Act
        this._testClass.PersonalImage = testValue;

        // Assert
        this._testClass.PersonalImage.ShouldBe(testValue);
    }

    /// <summary>
    /// Checks that the PersonalImageData property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetPersonalImageData()
    {
        // Arrange
        var testValue = new byte[] { 165, 149, 46, 29 };

        // Act
        this._testClass.PersonalImageData = testValue;

        // Assert
        this._testClass.PersonalImageData.ShouldBeSameAs(testValue);
    }

    /// <summary>
    /// Checks that the ContactId property can be read from and written to.
    /// </summary>
    [TestMethod]
    public void CanSetAndGetContactId()
    {
        // Arrange
        var testValue = 501176066L;

        // Act
        this._testClass.ContactId = testValue;

        // Assert
        this._testClass.ContactId.ShouldBe(testValue);
    }
}