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
    public class IndexOfBenchmark
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

        public IndexOfBenchmark()
        {
            Setup();
        }

        [GlobalSetup]
        public void Setup()
        {
            dhelper = new BenchmarkCollectionHelper();
            chelper = new BenchmarkSeriesHelper(); ;

            collection = dhelper.identifierKeyTestCollection.Take(10000).ToArray();

            foreach (var item in collection)
            {
                listing.Add(item.Key, item.Value);
                registry.Add(item.Key, item.Value);

                list.Add(item.Value);
            }

            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Catalog64_{DateTime.Now.ToFileTime().ToString()}_Test.log";
        }

        //[Benchmark]
        //public void Chain_IndexOf_Test()
        //{
        //    chelper.IndexOf_From_Indexer_Test(collection, chain);
        //}

        //[Benchmark]
        //public void Catalog_IndexOf_Test()
        //{
        //    chelper.IndexOf_From_Indexer_Test(collection, catalog);
        //}

        [Benchmark]
        public void Listing_IndexOf_Test()
        {
            chelper.IndexOf_Test(collection, listing);
        }

        [Benchmark]
        public void Registry_IndexOf_Test()
        {
            chelper.IndexOf_Test(collection, registry);
        }

        [Benchmark]
        public void List_IndexOf_Test()
        {
            dhelper.IndexOf_Test(collection, list);
        }

        //[Benchmark]
        //public void Dictionary_IndexOf_Test()
        //{
        //    dhelper.IndexOf_Test(collection, (IDictionary<string, string>)dictionary);
        //}

        //[Benchmark]
        //public void OrderedDictionary_IndexOf_Test()
        //{
        //    dhelper.IndexOf_Test(collection, ordereddictionary);
        //}

        //[Benchmark]
        //public void ConcurrentDictionary_IndexOf_Test()
        //{
        //    dhelper.IndexOf_Test(collection, (IDictionary<string, string>)concurrentdictionary);
        //}


    }
}
