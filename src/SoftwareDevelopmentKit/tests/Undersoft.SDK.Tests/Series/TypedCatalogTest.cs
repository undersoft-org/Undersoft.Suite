using System.Series.Tests;

namespace Undersoft.SDK.Tests.Series
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Tests.Instant;

    [TestClass]
    public class TypedCatalogTest : TypedCatalogTestHelper
    {
        public static object holder = new object();
        public static int threadCount = 0;
        public Task[] s1 = new Task[10];

        public TypedCatalogTest() : base()
        {
            typedRegistry = new TypedCatalog<Agreement>();
            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Registry__{DateTime.Now.ToFileTime().ToString()}_Test.log";
        }

        [TestMethod]
        public async Task Typed_Catalog_Async_Thread_Safe_Integrated_Test()
        {
            await Typed_Catalog_Async_Thread_Safe_Integrated_Test_Startup(identifiableObjectTestCollection).ConfigureAwait(true);
        }

        private void Typed_Catalog_Async_Thread_Safe_Integrated_Test_Callback(Task[] t)
        {
            Debug.WriteLine($"Test Finished");
        }

        private Task Typed_Catalog_Async_Thread_Safe_Integrated_Test_Startup(IList<Agreement> collection)
        {
            Action publicTest = () =>
            {
                int c = 0;
                lock (holder)
                    c = threadCount++;

                Typed_Catalog_Async_Thread_Safe_Integrated_Test_Helper(collection.Skip(c * 10000).Take(10000).ToArray());
            };

            for (int i = 0; i < 10; i++)
            {
                s1[i] = Task.Factory.StartNew(publicTest);
            }

            return Task.Factory.ContinueWhenAll(
                s1,
                new Action<Task[]>(a =>
                {
                    Typed_Catalog_Async_Thread_Safe_Integrated_Test_Callback(a);
                })
            );
        }
    }
}
