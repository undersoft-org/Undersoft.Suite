namespace System.Series.Tests
{
    using NetTopologySuite.Utilities;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Undersoft.SDK.Series;

    public class BenchmarkSeriesHelper
    {
        public BenchmarkSeriesHelper()
        {
            stringKeyTestCollection = PrepareTestListings.prepareStringKeyTestCollection();
            intKeyTestCollection = PrepareTestListings.prepareIntKeyTestCollection();
            longKeyTestCollection = PrepareTestListings.prepareLongKeyTestCollection();
            identifierKeyTestCollection = PrepareTestListings.prepareIdentifierKeyTestCollection();
        }

        public IList<KeyValuePair<object, string>> identifierKeyTestCollection { get; set; }

        public IList<KeyValuePair<object, string>> intKeyTestCollection { get; set; }

        public IList<KeyValuePair<object, string>> longKeyTestCollection { get; set; }

        public ISeries<string> registry { get; set; }

        public IDictionary<string, string> dictionary { get; set; }

        public IList<KeyValuePair<object, string>> stringKeyTestCollection { get; set; }

        public void LogIntegrated_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            Add_Test(testCollection, registry);
            Count_Test(100000);
            First_Test(testCollection[0].Value, registry);
            Last_Test(testCollection[99999].Value, registry);
            Get_Test(testCollection, registry);
            GetItem_Test(testCollection, registry);
            Remove_Test(testCollection, registry);
            Count_Test(70000);
            Enqueue_Test(testCollection, registry);
            Count_Test(70005);
            Dequeue_Test(testCollection, registry);
            Contains_Test(testCollection, registry);
            ContainsKey_Test(testCollection, registry);
            Put_Test(testCollection, registry);
            Count_Test(100000);
            Clear_Test(registry);
            Add_V_Test(testCollection, registry);
            Count_Test(100000);
            Remove_V_Test(testCollection, registry);
            Count_Test(70000);
            Put_V_Test(testCollection, registry);
            IndexOf_Test(testCollection, registry);
            GetByIndexer_Test(testCollection, registry);
            Count_Test(100000);
        }

        public void LogThreadIntegrated_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            LogAdd_Test(testCollection, registry);
            LogGet_Test(testCollection, registry);
            LogGetItem_Test(testCollection, registry);
            LogRemove_Test(testCollection, registry);
            LogEnqueue_Test(testCollection, registry);
            LogDequeue_Test(testCollection, registry);
            LogContains_Test(testCollection, registry);
            LogContainsKey_Test(testCollection, registry);
            LogPut_Test(testCollection, registry);
            LogGetByIndexer_Test(testCollection, registry);

            Debug.WriteLine($"Thread no {testCollection[0].Key.ToString()}_{registry.Count} ends");
        }

        public void Add_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Add(item.Key, item.Value);
            }
        }

        public void TryAdd_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.TryAdd(item.Key, item.Value);
            }
        }

        public void Insert_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                registry.Insert(i++, item.Value);
            }
        }

        public void Add_V_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Add(item.Value);
            }
        }

        public void Clear_Test(ISeries<string> registry)
        {
            foreach (var item in stringKeyTestCollection)
            {
                registry.Add(item.Key, item.Value);
            }
            registry.Clear();
        }

        public void Contains_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Contains(registry.NewItem(item.Key, item.Value));
            }
        }

        public void ContainsKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.ContainsKey(item.Key);
            }
        }

        public void CopyTo_Test() { }

        public void Count_Test(int count)
        {
            Assert.Equals(count, registry.Count);
        }

        public void Dequeue_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                string output = registry.Dequeue();
            }
        }

        public void Enqueue_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Enqueue(item.Key, item.Value);
            }
        }

        public void First_Test(string firstValue, ISeries<string> registry)
        {
            registry = new Registry<string>();
            foreach (var item in identifierKeyTestCollection)
            {
                registry.Add(item.Key, item.Value);
            }
            Assert.Equals(registry.Next(registry.First).Value, firstValue);
        }

        public void Get_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                string r = registry.Get(item.Key);
            }
        }

        public void GetByIndex_From_Indexer_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                string a = registry[i++];
            }
        }

        public void GetByKey_From_Indexer_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                string a = registry[item.Key];
            }
        }

        public void TryGetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                var a = registry.TryGet(item.Key, out string output);
            }
        }

        public void GetByIndexer_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                string a = registry[i++];
            }
        }

        public void GetItem_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.GetItem(item.Key);
            }
        }

        public void GetOrAdd_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.EnsureGet(item.Key, item.Value);
            }
        }

        public void IndexOf_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.IndexOf(item.Value);
            }
        }

        public void Iteration_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (string item in registry)
            {
                string r = item;
            }
        }

        public void IterationAsBuckets_Test(IEnumerable<KeyValuePair<object, string>> testCollection, Chain<string> registry)
        {
            foreach (ISeriesItem<string> item in registry)
            {
                string r = item.Value;
            }
        }


        public void IterationAsBuckets_Test(IEnumerable<KeyValuePair<object, string>> testCollection, Catalog<string> registry)
        {
            foreach (ISeriesItem<string> item in registry)
            {
                string r = item.Value;
            }
        }

        public void Last_Test(string lastValue, ISeries<string> registry)
        {
            Assert.Equals(registry.Last.Value, lastValue);
        }

        public void LogAdd_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection)
            {
                registry.Add(item.Key, item.Value);
                counter++;
            }
            Debug.WriteLine($"Add Thread no {testCollection[0].Key.ToString()}_{counter} ends");
        }

        public void LogAdd_V_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection)
            {
                registry.Add(item.Value);
                counter++;
            }
            Debug.WriteLine($"Add Thread no {testCollection[0].Key.ToString()}_{counter} ends");
        }

        public void LogContains_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection)
            {
                if (registry.Contains(registry.NewItem(item.Key, item.Value)))
                    counter++;
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Key.ToString()}_{counter} ends"
            );
        }

        public void LogContainsKey_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection)
            {
                if (registry.ContainsKey(item.Key))
                    counter++;
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Key.ToString()}_{counter} ends"
            );
        }

        public void LogDequeue_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            for (int i = 0; i < 5; i++)
            {
                string output = null;
                if (registry.TryDequeue(out output))
                    counter++;
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Key.ToString()}_{counter} ends"
            );
        }

        public void LogEnqueue_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection.Skip(5000).Take(5))
            {
                if (registry.Enqueue(item.Key, item.Value))
                    counter++;
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Key.ToString()}_{counter} ends"
            );
        }

        public void LogGet_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection)
            {
                string r = registry.Get(item.Key);
                if (r != null)
                    counter++;
            }
            Debug.WriteLine($"Get Thread no {testCollection[0].Key.ToString()}_{counter} ends");
        }

        public void LogGetByIndexer_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            int i = 0;
            foreach (var item in testCollection)
            {
                var r = registry[i];
                if (r != null)
                    counter++;
            }
            Debug.WriteLine(
                $"Get By Indexer Thread no {testCollection[0].Key.ToString()}_{counter} ends"
            );
        }

        public void LogGetItem_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection)
            {
                var r = registry.GetItem(item.Key);
                if (r != null)
                    counter++;
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Key.ToString()}_{counter} ends"
            );
        }

        public void LogPut_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection)
            {
                registry.Put(item.Key, item.Value);
                counter++;
            }
            Debug.WriteLine($"Put Thread no {testCollection[0].Key.ToString()}_{counter} ends");
        }

        public void LogPut_V_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection)
            {
                var r = registry.Put(item.Value);
                if (r != null)
                    counter++;
            }
            Debug.WriteLine($"Removed Thread no {testCollection[0].Key.ToString()}_{counter} ends");
        }

        public void LogRemove_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection.Skip(5000))
            {
                var r = registry.Remove(item.Key);
                if (r != null)
                    counter++;
            }
            Debug.WriteLine($"Removed Thread no {testCollection[0].Key.ToString()}_{counter} ends");
        }

        public void LogRemove_V_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int counter = 0;
            foreach (var item in testCollection.Skip(5000))
            {
                string r = registry.Remove(item.Value);
                if (r != null)
                    counter++;
            }
        }

        public void Put_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Put(item.Key, item.Value);
            }
        }

        public void SetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry[item.Key] = item.Value;
            }
        }

        public void SetByIndex_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                registry[i++] = item.Value;
            }
        }

        public void Put_V_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Put(item.Value);
            }
        }

        public void Remove_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection.Skip(100000))
            {
                registry.Remove(item.Key);
            }
        }

        public void Remove_V_Test(IList<KeyValuePair<object, string>> testCollection, ISeries<string> registry)
        {
            foreach (var item in testCollection.Skip(70000))
            {
                registry.Remove(item.Value);
            }
        }
    }
}
