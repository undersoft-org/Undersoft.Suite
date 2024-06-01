using System.Series.Tests;

namespace Undersoft.SDK.Benchmarks.Series
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Undersoft.SDK.Series;

    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 5)]
    public class TryAddBenchmark
    {
        public static object holder = new object();
        public static int threadCount = 0;
        public Task[] tasks = new Task[10];
        public BenchmarkCollectionHelper dhelper = new BenchmarkCollectionHelper();
        public BenchmarkSeriesHelper chelper = new BenchmarkSeriesHelper();
        public IList<KeyValuePair<object, string>> collection;

        public TryAddBenchmark()
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

        [Benchmark]
        public void Chain_TryAdd_Test()
        {
            var registry = new Chain<string>();
            chelper.TryAdd_Test(collection, registry);
        }

        [Benchmark]
        public void Catalog_TryAdd_Test()
        {
            var registry = new Catalog<string>();
            chelper.TryAdd_Test(collection, registry);
        }

        [Benchmark]
        public void Listing_TryAdd_Test()
        {
            var registry = new Listing<string>();
            chelper.TryAdd_Test(collection, registry);
        }

        [Benchmark]
        public void Registry_TryAdd_Test()
        {
            var registry = new Registry<string>();
            chelper.TryAdd_Test(collection, registry);
        }

        [Benchmark]
        public void Dictionary_TryAdd_Test()
        {
            var registry = new Dictionary<string, string>();
            dhelper.TryAdd_Test(collection, (IDictionary<string, string>)registry);
        }

        [Benchmark]
        public void ConcurrentDictionary_TryAdd_Test()
        {
            var registry = new ConcurrentDictionary<string, string>();
            dhelper.TryAdd_Test(collection, (IDictionary<string, string>)registry);
        }


    }
}
