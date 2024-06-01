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
    using System.Threading.Tasks;
    using Undersoft.SDK.Series;

    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 5)]
    public class DequeueBenchmark
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

        public Queue<string> queue = new Queue<string>();
        public ConcurrentQueue<string> concurrentqueue = new ConcurrentQueue<string>();

        public DequeueBenchmark()
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
            chain = new Chain<string>();
            listing = new Listing<string>();
            catalog = new Catalog<string>();
            registry = new Registry<string>();
            queue = new Queue<string>();
            concurrentqueue = new ConcurrentQueue<string>();
            foreach (var item in collection)
            {
                chain.TryAdd(item.Key, item.Value);
                catalog.TryAdd(item.Key, item.Value);
                listing.TryAdd(item.Key, item.Value);
                registry.TryAdd(item.Key, item.Value);

                queue.Enqueue(item.Value);
                concurrentqueue.Enqueue(item.Value);
            }
        }

        [Benchmark]
        public void Chain_Dequeue_Test()
        {
            chelper.Dequeue_Test(collection, chain);
        }

        [Benchmark]
        public void Catalog_Dequeue_Test()
        {
            chelper.Dequeue_Test(collection, catalog);
        }

        [Benchmark]
        public void Listing_Dequeue_Test()
        {
            chelper.Dequeue_Test(collection, listing);
        }

        [Benchmark]
        public void Registry_Dequeue_Test()
        {
            chelper.Dequeue_Test(collection, registry);
        }

        //[Benchmark]
        //public void List_Dequeue_Test()
        //{
        //    dhelper.Dequeue_Test(collection, list);
        //}

        [Benchmark]
        public void Queue_Dequeue_Test()
        {
            dhelper.Dequeue_Test(collection, queue);
        }

        //[Benchmark]
        //public void OrderedDictionary_Dequeue_Test()
        //{
        //    dhelper.Dequeue_Test(collection, ordereddictionary);
        //}

        [Benchmark]
        public void ConcurrentQueue_Dequeue_Test()
        {
            dhelper.Dequeue_Test(collection, concurrentqueue);
        }


    }
}
