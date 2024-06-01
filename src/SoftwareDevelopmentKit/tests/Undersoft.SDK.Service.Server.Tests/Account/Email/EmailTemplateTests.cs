using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Server.Accounts.Email;

namespace Undersoft.SDK.Service.Server.Tests.Accounts.Email;

/// <summary>
/// Unit tests for the type <see cref="EmailTemplate"/>.
/// </summary>
[TestClass]
public class EmailTemplateTests
{
    /// <summary>
    /// Checks that the GetVerificationCodeMessage method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetVerificationCodeMessage()
    {
        // Arrange
        var token = "TestValue1133219331";

        // Act
        var result = EmailTemplate.GetVerificationCodeMessage(token);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetVerificationCodeMessage method throws when the token parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetVerificationCodeMessageWithInvalidToken(string value)
    {
        Should.Throw<ArgumentNullException>(() => EmailTemplate.GetVerificationCodeMessage(value));
    }

    /// <summary>
    /// Checks that the GetResetPasswordMessage method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallGetResetPasswordMessage()
    {
        // Arrange
        var password = "TestValue2048596238";

        // Act
        var result = EmailTemplate.GetResetPasswordMessage(password);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the GetResetPasswordMessage method throws when the password parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallGetResetPasswordMessageWithInvalidPassword(string value)
    {
        Should.Throw<ArgumentNullException>(() => EmailTemplate.GetResetPasswordMessage(value));
    }
}