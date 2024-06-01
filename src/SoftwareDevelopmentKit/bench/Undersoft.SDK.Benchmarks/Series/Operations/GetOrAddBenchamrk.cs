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
    public class GetOrAddBenchmark
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

        public GetOrAddBenchmark()
        {
            Setup();
        }

        [GlobalSetup]
        public void Setup()
        {
            dhelper = new BenchmarkCollectionHelper();
            chelper = new BenchmarkSeriesHelper(); ;

            collection = dhelper.identifierKeyTestCollection;

            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Catalog64_{DateTime.Now.ToFileTime().ToString()}_Test.log";
        }

        [IterationSetup]
        public void Prepare()
        {
            chain = new Chain<string>();
            listing = new Listing<string>();
            catalog = new Catalog<string>();
            registry = new Registry<string>();
            concurrentdictionary = new ConcurrentDictionary<string, string>();

            foreach (var item in collection.Take(collection.Count / 2))
            {
                chain.Add(item.Key, item.Value);
                catalog.Add(item.Key, item.Value);
                listing.Add(item.Key, item.Value);
                registry.Add(item.Key, item.Value);
                concurrentdictionary.TryAdd(item.Key.ToString(), item.Value);
            }
        }

        [Benchmark]
        public void Chain_GetOrAdd_Test()
        {
            chelper.GetOrAdd_Test(collection, chain);
        }

        [Benchmark]
        public void Catalog_GetOrAdd_Test()
        {
            chelper.GetOrAdd_Test(collection, catalog);
        }

        [Benchmark]
        public void Listing_GetOrAdd_Test()
        {
            chelper.GetOrAdd_Test(collection, listing);
        }

        [Benchmark]
        public void Registry_GetOrAdd_Test()
        {
            chelper.GetOrAdd_Test(collection, registry);
        }

        [Benchmark]
        public void ConcurrentDictionary_GetOrAdd_Test()
        {
            dhelper.GetOrAdd_Test(collection, concurrentdictionary);
        }
    }
}
