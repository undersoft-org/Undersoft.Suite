using System.Series.Tests;

namespace Undersoft.SDK.Benchmarks.Series
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Undersoft.SDK.Series;

    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 5)]
    public class ConcurrentTryGetByKeyBenchmark
    {
        public static object holder = new object();
        public static int threadCount = 0;
        public Task[] tasks = new Task[10];
        public BenchmarkCollectionHelper dhelper = new BenchmarkCollectionHelper();
        public BenchmarkSeriesHelper chelper = new BenchmarkSeriesHelper();
        public IList<KeyValuePair<object, string>> collection;

        public Chain<string> chain = new Chain<string>();
        public Catalog<string> catalog = new Catalog<string>();
        public Listing<string> listing = new Listing<string>();
        public Registry<string> registry = new Registry<string>();

        public List<string> list = new List<string>();
        public Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public OrderedDictionary ordereddictionary = new OrderedDictionary();
        public ConcurrentDictionary<string, string> concurrentdictionary = new ConcurrentDictionary<string, string>();

        public ConcurrentTryGetByKeyBenchmark()
        {
            Setup();
        }

        [GlobalSetup]
        public void Setup()
        {
            dhelper = new BenchmarkCollectionHelper();
            chelper = new BenchmarkSeriesHelper(); ;

            collection = dhelper.identifierKeyTestCollection;

            foreach (var item in collection)
            {
                catalog.TryAdd(item.Key, item.Value);
                registry.TryAdd(item.Key, item.Value);
                concurrentdictionary.TryAdd(item.Key.ToString(), item.Value);
            }

            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Catalog64_{DateTime.Now.ToFileTime().ToString()}_Test.log";
        }

        [IterationSetup]
        public void Prepare()
        {
            tasks = new Task[10];
            threadCount = 0;
        }

        private void Callback(Task[] t)
        {
            Debug.WriteLine($"Test Finished");
        }

        public int count => collection.Count;

        [Benchmark]
        public Task Catalog_TryGetByKey_Test()
        {
            int limit = count / 10;
            return Task.Factory.ContinueWhenAll(
                tasks
                    .ForEach(
                        (t, x) =>
                            tasks[x] = Task.Factory.StartNew(
                                () =>
                                    chelper.TryGetByKey_Test(
                                        collection.Skip(x * limit).Take(limit).ToArray(),
                                       catalog
                                    )
                            )
                    )
                    .Commit(),
                new Action<Task[]>(a =>
                {
                    Callback(a);
                })
            );
        }

        [Benchmark]
        public Task Registry_TryGetByKey_Test()
        {
            int limit = count / 10;
            return Task.Factory.ContinueWhenAll(
                tasks
                    .ForEach(
                        (t, x) =>
                            tasks[x] = Task.Factory.StartNew(
                                () =>
                                    chelper.TryGetByKey_Test(
                                        collection.Skip(x * limit).Take(limit).ToArray(),
                                        registry
                                    )
                            )
                    )
                    .Commit(),
                new Action<Task[]>(a =>
                {
                    Callback(a);
                })
            );
        }

        [Benchmark]
        public Task ConcurrentDictionary_TryGetByKey_Test()
        {
            int limit = count / 10;
            return Task.Factory.ContinueWhenAll(
                tasks
                    .ForEach(
                        (t, x) =>
                            tasks[x] = Task.Factory.StartNew(
                                () =>
                                    dhelper.TryGetByKey_Test(
                                        collection.Skip(x * limit).Take(limit).ToArray(),
                                        concurrentdictionary
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
