using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Tests.Data.Blob;

/// <summary>
/// Unit tests for the type <see cref="BlobProviderBase"/>.
/// </summary>
[TestClass]
public class BlobProviderBaseTests
{
    private class TestBlobProviderBase : BlobProviderBase
    {
        public override Task SaveAsync(BlobProviderSaveArgs args)
        {
            return default(Task);
        }

        public override Task<bool> DeleteAsync(BlobProviderArgs args)
        {
            return default(Task<bool>);
        }

        public override Task<bool> ExistsAsync(BlobProviderArgs args)
        {
            return default(Task<bool>);
        }

        public override Task<Stream> GetOrNullAsync(BlobProviderArgs args)
        {
            return default(Task<Stream>);
        }
    }

    private TestBlobProviderBase _testClass;

    /// <summary>
    /// Sets up the dependencies required for the tests for <see cref="BlobProviderBase"/>.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        this._testClass = new TestBlobProviderBase();
    }
}