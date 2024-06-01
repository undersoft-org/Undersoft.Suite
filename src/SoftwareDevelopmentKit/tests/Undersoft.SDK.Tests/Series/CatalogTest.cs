using System.Series.Tests;

namespace Undersoft.SDK.Tests.Series
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Undersoft.SDK.Series;

    [TestClass]
    public class CatalogTest : CatalogTestHelper
    {
        public static int threadCount = 0;
        public object holder = new object();
        public Task[] s1 = new Task[6];

        public CatalogTest() : base()
        {
            registry = new Catalog64<string>();
            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Catalog_{DateTime.Now.ToFileTime().ToString()}_Test.log";
        }

        [TestMethod]
        public async Task Catalog_Identifiable_Async_Thread_Safe_Integrated_Test()
        {
            Task t = Catalog_Async_Thread_Safe_Integrated_Test_Startup(identifierKeyTestCollection);
            await t.ConfigureAwait(true);
        }

        [TestMethod]
        public async Task Catalog_Integer_Keys_Async_Thread_Safe_Integrated_Test()
        {
            Task t = Catalog_Async_Thread_Safe_Integrated_Test_Startup(intKeyTestCollection);
            await t.ConfigureAwait(true);
        }

        [TestMethod]
        public async Task Catalog_Long_Keys_Async_Thread_Safe_Integrated_Test()
        {
            Task t = Catalog_Async_Thread_Safe_Integrated_Test_Startup(longKeyTestCollection);
            await t.ConfigureAwait(true);
        }

        [TestMethod]
        public async Task Catalog_String_Keys_Async_Thread_Safe_Integrated_Test()
        {
            Task t = Catalog_Async_Thread_Safe_Integrated_Test_Startup(stringKeyTestCollection);
            await t.ConfigureAwait(true);
        }

        [TestMethod]
        public void Catalog_Identifiable_Sync_Integrated_Test()
        {
            Catalog_Sync_Integrated_Test_Helper(identifierKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Catalog_Integer_Keys_Sync_Integrated_Test()
        {
            Catalog_Sync_Integrated_Test_Helper(intKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Catalog_Long_Keys_Sync_Integrated_Test()
        {
            Catalog_Sync_Integrated_Test_Helper(longKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Catalog_String_Keys_Sync_Integrated_Test()
        {
            Catalog_Sync_Integrated_Test_Helper(stringKeyTestCollection.Take(100000).ToArray());
        }

        private Task Catalog_Async_Thread_Safe_Integrated_Test_Startup(IList<KeyValuePair<object, string>> collection)
        {
            registry = new Catalog64<string>();
            Action publicTest = () =>
            {
                int c = 0;
                lock (holder)
                    c = threadCount++;

                Catalog_Async_Thread_Safe_Integrated_Test_Helper(collection.Skip(c * 10000).Take(10000).ToArray());
            };

            for (int i = 0; i < 6; i++)
            {
                s1[i] = Task.Factory.StartNew(publicTest);
            }

            return Task.Factory.ContinueWhenAll(
                s1,
                new Action<Task[]>(a =>
                {
                    Catalog_Async_Thread_Safe_Integrated_Test_Callback(a);
                })
            );
        }

        private void Catalog_Async_Thread_Safe_Integrated_Test_Callback(Task[] t)
        {
            Debug.WriteLine($"Test Finished");
        }
    }
}
