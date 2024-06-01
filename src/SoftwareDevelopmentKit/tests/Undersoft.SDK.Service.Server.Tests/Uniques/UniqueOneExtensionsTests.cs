using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Undersoft.SDK.Uniques;
using T = Undersoft.SDK.Service.Data.Entity.Entity;

namespace Undersoft.SDK.Service.Server.Tests;

/// <summary>
/// Unit tests for the type <see cref="UniqueOneExtensions"/>.
/// </summary>
[TestClass]
public class UniqueOneExtensionsTests
{
    /// <summary>
    /// Checks that the AsUniqueOne method functions correctly.
    /// </summary>
    [TestMethod]
    public void CanCallAsUniqueOneWithIQueryableOfT()
    {
        // Arrange
        var entity = Substitute.For<IQueryable<T>>();

        // Act
        var result = entity.AsUniqueOne<T>();

        // Assert
        Assert.Fail("Create or modify test");
    }

    /// <summary>
    /// Checks that the AsUniqueOne method throws when the entity parameter is null.
    /// </summary>
    [TestMethod]
    public void CannotCallAsUniqueOneWithIEnumerableOfTWithNullEntity()
    {
        Should.Throw<ArgumentNullException>(() => default(IEnumerable<T>).AsUniqueOne<T>());
    }
}