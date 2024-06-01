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

    [TestClass]
    public class RegistryTest : RegistryTestHelper
    {
        public static object holder = new object();
        public static int threadCount = 0;
        public Task[] s1 = new Task[10];

        public RegistryTest() : base()
        {
            registry = new Registry64<string>();
            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Registry__{DateTime.Now.ToFileTime().ToString()}_Test.log";
        }

        [TestMethod]
        public async Task Registry_Identifiable_Async_Thread_Safe_Integrated_Test()
        {
            await Registry_Async_Thread_Safe_Integrated_Test_Startup(identifierKeyTestCollection).ConfigureAwait(true);
        }

        [TestMethod]
        public async Task Registry_Integer_Keys_Async_Thread_Safe_Integrated_Test()
        {
            await Registry_Async_Thread_Safe_Integrated_Test_Startup(intKeyTestCollection).ConfigureAwait(true);
        }

        [TestMethod]
        public async Task Registry_Long_Keys_Async_Thread_Safe_Integrated_Test()
        {
            await Registry_Async_Thread_Safe_Integrated_Test_Startup(longKeyTestCollection).ConfigureAwait(true);
        }

        [TestMethod]
        public async Task Registry_String_Keys_Async_Thread_Safe_Integrated_Test()
        {
            await Registry_Async_Thread_Safe_Integrated_Test_Startup(stringKeyTestCollection).ConfigureAwait(true);
        }

        [TestMethod]
        public void Registry_Identifiable_Sync_Integrated_Test()
        {
            Registry_Sync_Integrated_Test_Helper(identifierKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Registry_Integer_Keys_Sync_Integrated_Test()
        {
            Registry_Sync_Integrated_Test_Helper(intKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Registry_Long_Keys_Sync_Integrated_Test()
        {
            Registry_Sync_Integrated_Test_Helper(longKeyTestCollection.Take(100000).ToArray());
        }

        [TestMethod]
        public void Registry_String_Keys_Sync_Integrated_Test()
        {
            Registry_Sync_Integrated_Test_Helper(stringKeyTestCollection.Take(100000).ToArray());
        }

        private void Registry_Async_Thread_Safe_Integrated_Test_Callback(Task[] t)
        {
            Debug.WriteLine($"Test Finished");
        }

        private Task Registry_Async_Thread_Safe_Integrated_Test_Startup(IList<KeyValuePair<object, string>> collection)
        {
            Action publicTest = () =>
            {
                int c = 0;
                lock (holder)
                    c = threadCount++;

                Registry_Async_Thread_Safe_Integrated_Test_Helper(collection.Skip(c * 10000).Take(10000).ToArray());
            };

            for (int i = 0; i < 10; i++)
            {
                s1[i] = Task.Factory.StartNew(publicTest);
            }

            return Task.Factory.ContinueWhenAll(
                s1,
                new Action<Task[]>(a =>
                {
                    Registry_Async_Thread_Safe_Integrated_Test_Callback(a);
                })
            );
        }
    }
}
