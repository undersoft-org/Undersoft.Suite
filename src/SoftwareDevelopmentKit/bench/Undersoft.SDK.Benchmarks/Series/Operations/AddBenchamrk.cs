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
    public class AddBenchmark
    {
        public static object holder = new object();
        public static int threadCount = 0;
        public Task[] tasks = new Task[10];
        public BenchmarkCollectionHelper dhelper = new BenchmarkCollectionHelper();
        public BenchmarkSeriesHelper chelper = new BenchmarkSeriesHelper();
        public IList<KeyValuePair<object, string>> collection;

        public AddBenchmark()
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
        public void List_Add_Test()
        {
            var registry = new List<string>();
            dhelper.Add_Test(collection, registry);
        }

        [Benchmark]
        public void Dictionary_Add_Test()
        {
            var registry = new Dictionary<string, string>();
            dhelper.Add_Test(collection, (IDictionary<string, string>)registry);
        }

        [Benchmark]
        public void OrderedDictionary_Add_Test()
        {
            var registry = new OrderedDictionary();
            dhelper.Add_Test(collection, registry);
        }

        [Benchmark]
        public void ConcurrentDictionary_Add_Test()
        {
            var registry = new ConcurrentDictionary<string, string>();
            dhelper.Add_Test(collection, (IDictionary<string, string>)registry);
        }


        [Benchmark]
        public void Chain_Add_Test()
        {
            var registry = new Chain<string>();
            chelper.Add_Test(collection, registry);
        }

        [Benchmark]
        public void Catalog_Add_Test()
        {
            var registry = new Catalog<string>();
            chelper.Add_Test(collection, registry);
        }

        [Benchmark]
        public void Listing_Add_Test()
        {
            var registry = new Listing<string>();
            chelper.Add_Test(collection, registry);
        }

        [Benchmark]
        public void Registry_Add_Test()
        {
            var registry = new Registry<string>();
            chelper.Add_Test(collection, registry);
        }
    }
}
