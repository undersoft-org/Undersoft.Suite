using System.Series.Tests;

namespace Undersoft.SDK.Tests.Series
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics;
    using System.Linq;
    using Undersoft.SDK.Series;

    [TestClass]
    public class ChainTest : CatalogTestHelper
    {
        public ChainTest()
        {
            registry = new Chain<string>();
            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Chain_{DateTime.Now.ToFileTime().ToString()}_Test.log";
        }

        [TestMethod]
        public void Chain_Identifiable_Sync_Integrated_Test()
        {
            Catalog_Sync_Integrated_Test_Helper(identifierKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Chain_Integer_Keys_Sync_Integrated_Test()
        {
            Catalog_Sync_Integrated_Test_Helper(intKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Chain_Long_Keys_Sync_Integrated_Test()
        {
            Catalog_Sync_Integrated_Test_Helper(longKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Chain_String_Keys_Sync_Integrated_Test()
        {
            Catalog_Sync_Integrated_Test_Helper(stringKeyTestCollection.Take(100000).ToArray());
        }
    }
}
