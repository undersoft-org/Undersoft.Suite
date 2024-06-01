using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Tests.Data.Query;

/// <summary>
/// Unit tests for the type <see cref="FilterOperand"/>.
/// </summary>
[TestClass]
public class FilterOperandTests
{
    /// <summary>
    /// Checks that the ParseMathOperand method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallParseMathOperand()
    {
        // Arrange
        var operandString = "TestValue1135444286";

        // Act
        var result = FilterOperand.ParseMathOperand(operandString);

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the ParseMathOperand method throws when the operandString parameter is null, empty or white space.
    /// </summary>
    /// <param name="value">The parameter that receives the test case values.</param>
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void CannotCallParseMathOperandWithInvalidOperandString(string value)
    {
        Should.Throw<ArgumentNullException>(() => FilterOperand.ParseMathOperand(value));
    }

    /// <summary>
    /// Checks that the ConvertMathOperand method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallConvertMathOperand()
    {
        // Arrange
        var operand = MathOperand.NotLike;

        // Act
        var result = FilterOperand.ConvertMathOperand(operand);

        // Assert
        Assert.Fail("Create or modify test");
    }
}