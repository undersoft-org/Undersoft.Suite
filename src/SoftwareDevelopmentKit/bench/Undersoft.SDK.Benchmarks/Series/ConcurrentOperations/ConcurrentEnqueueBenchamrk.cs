using System.Series.Tests;

namespace Undersoft.SDK.Benchmarks.Series
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Undersoft.SDK.Series;

    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 5)]
    public class ConcurrentEnqueueBenchmark
    {
        public static object holder = new object();
        public static int threadCount = 0;
        public Task[] tasks = new Task[10];
        public BenchmarkCollectionHelper dhelper = new BenchmarkCollectionHelper();
        public BenchmarkSeriesHelper chelper = new BenchmarkSeriesHelper();
        public IList<KeyValuePair<object, string>> collection;

        public ConcurrentEnqueueBenchmark()
        {
            Setup();
        }

        [GlobalSetup]
        public void Setup()
        {
            dhelper = new BenchmarkCollectionHelper();
            chelper = new BenchmarkSeriesHelper(); ;

            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Catalog64_{DateTime.Now.ToFileTime().ToString()}_Test.log";

            collection = dhelper.identifierKeyTestCollection;
        }

        [IterationSetup]
        public void Prepare()
        {
            tasks = new Task[10];
            threadCount = 0;
        }

        public int count => collection.Count;

        private void Callback(Task[] t)
        {
            Debug.WriteLine($"Test Finished");
        }

        [Benchmark]
        public Task Catalog_Enqueue_Test()
        {
            var registry = new Catalog<string>();
            int limit = count / 10;
            return Task.Factory.ContinueWhenAll(
                tasks
                    .ForEach(
                        (t, x) =>
                            tasks[x] = Task.Factory.StartNew(
                                () =>
                                    chelper.Enqueue_Test(
                                        collection.Skip(x * limit).Take(limit).ToArray(),
                                        registry
                                    )
                            )
                    )
                    .ToArray(),
                new Action<Task[]>(a =>
                {
                    Callback(a);
                })
            );
        }

        [Benchmark]
        public Task Registry_Enqueue_Test()
        {
            var registry = new Registry<string>();
            int limit = count / 10;
            return Task.Factory.ContinueWhenAll(
                tasks
                    .ForEach(
                        (t, x) =>
                            tasks[x] = Task.Factory.StartNew(
                                () =>
                                    chelper.Enqueue_Test(
                                        collection.Skip(x * limit).Take(limit).ToArray(),
                                        registry
                                    )
                            )
                    )
                    .ToArray(),
                new Action<Task[]>(a =>
                {
                    Callback(a);
                })
            );
        }

        [Benchmark]
        public Task ConcurrentQueue_Enqueue_Test()
        {
            var registry = new ConcurrentQueue<string>();
            int limit = count / 10;
            return Task.Factory.ContinueWhenAll(
                tasks
                    .ForEach(
                        (t, x) =>
                            tasks[x] = Task.Factory.StartNew(
                                () =>
                                    dhelper.Enqueue_Test(
                                        collection.Skip(x * limit).Take(limit).ToArray(),
                                        registry
                                    )
                            )
                    )
                    .ToArray(),
                new Action<Task[]>(a =>
                {
                    Callback(a);
                })
            );
        }

    }
}
