namespace System.Series.Tests
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public class BenchmarkCollectionHelper
    {
        public BenchmarkCollectionHelper()
        {
            stringKeyTestCollection = PrepareTestListings.prepareStringKeyTestCollection();
            intKeyTestCollection = PrepareTestListings.prepareIntKeyTestCollection();
            longKeyTestCollection = PrepareTestListings.prepareLongKeyTestCollection();
            identifierKeyTestCollection = PrepareTestListings.prepareIdentifierKeyTestCollection();
        }

        public IList<KeyValuePair<object, string>> identifierKeyTestCollection { get; set; }

        public IList<KeyValuePair<object, string>> intKeyTestCollection { get; set; }

        public IList<KeyValuePair<object, string>> longKeyTestCollection { get; set; }

        public ConcurrentDictionary<string, string> registry { get; set; }

        public IDictionary orderedRegistry { get; set; }

        public IList<string> list { get; set; }

        public ConcurrentQueue<string> queue { get; set; }

        public IList<KeyValuePair<object, string>> stringKeyTestCollection { get; set; }

        public void TryAdd_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.TryAdd(item.Key.ToString(), item.Value);
            }
        }

        public void Add_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Add(item.Key.ToString(), item.Value);
            }
        }

        public void Insert_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IList<string> registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                registry.Insert(i++, item.Value);
            }
        }

        public void ContainsValue_Test(IEnumerable<KeyValuePair<object, string>> testCollection, Dictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.ContainsValue(
                    (item.Value)
                );
            }
        }

        public void Insert_Test(IEnumerable<KeyValuePair<object, string>> testCollection, OrderedDictionary registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                registry.Insert(i++, item.Key.ToString(), item.Value);
            }
        }

        public void ContainsKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.ContainsKey(item.Key.ToString());
            }
        }

        public void GetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                string r = registry[item.Key.ToString()];
            }
        }

        public void GetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, Dictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                string r = registry[item.Key.ToString()];
            }
        }

        public void TryGetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                var r = registry.TryGetValue(item.Key.ToString(), out var output);
            }
        }

        public void GetOrAdd_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                string r = registry.GetOrAdd(item.Key.ToString(), item.Value);
            }
        }

        public void GetByIndex_Test(IEnumerable<KeyValuePair<object, string>> testCollection, OrderedDictionary registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                object r = registry[i++];
            }
        }

        public void GetLast_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary<string, string> registry)
        {
            string r = registry.Last().Value;
        }

        public void SetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                registry[item.Key.ToString()] = item.Value;
            }
        }

        public void Remove_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary<string, string> registry)
        {
            foreach (var item in testCollection.Skip(100000))
            {
                registry.Remove(item.Key.ToString());
            }
        }

        public void TryRemove_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentDictionary<string, string> registry)
        {
            foreach (var item in testCollection.Skip(100000))
            {
                var result = registry.Remove(item.Key.ToString(), out var output);
            }
        }

        public void Iteration_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary<string, string> registry)
        {
            foreach (var item in registry)
            {
                var r = item.Value;
            }
        }

        public void Iteration_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentDictionary<string, string> registry)
        {
            foreach (var item in registry)
            {
                var r = item.Value;
            }
        }

        public void IterationAsBuckets_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary<string, string> registry)
        {
            foreach (var item in registry)
            {
                string r = item.Value;
            }
        }

        public void IterationAsBuckets_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentDictionary<string, string> registry)
        {
            foreach (var item in registry)
            {
                var r = item.Value;
            }
        }

        public void IndexOf_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IList<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.IndexOf(item.Value);
            }
        }

        public void Add_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary registry)
        {
            foreach (var item in testCollection)
            {
                registry.Add(item.Key, item.Value);
            }
        }

        public void Contains_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary registry)
        {
            foreach (var item in testCollection)
            {
                registry.Contains(item.Key
                );
            }
        }

        public void GetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary registry)
        {
            foreach (var item in testCollection)
            {
                string r = (string)registry[item.Key];
            }
        }

        public void SetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary registry)
        {
            foreach (var item in testCollection)
            {
                registry[item.Key] = item.Value;
            }
        }

        public void SetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                registry[item.Key.ToString()] = item.Value;
            }
        }

        public void SetByKey_Test(IEnumerable<KeyValuePair<object, string>> testCollection, Dictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                registry[item.Key.ToString()] = item.Value;
            }
        }

        public void AddOrUpdate_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentDictionary<string, string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.AddOrUpdate(item.Key.ToString(), item.Value, (k, v) => item.Value);
            }
        }

        public void SetByIndex_Test(IEnumerable<KeyValuePair<object, string>> testCollection, OrderedDictionary registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                registry[i++] = item.Value;
            }
        }

        public void Remove_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary registry)
        {
            foreach (var item in testCollection.Skip(100000))
            {
                registry.Remove(item.Key);
            }
        }

        public void Iteration_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary registry)
        {
            foreach (var item in registry)
            {
                object r = item;
            }
        }

        public void IterationAsBuckets_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IDictionary registry)
        {
            foreach (var item in registry)
            {
                object r = item;
            }
        }

        public void Dequeue_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentQueue<string> queue)
        {
            foreach (var item in testCollection)
            {
                string output = null;
                queue.TryDequeue(out output);
            }
        }

        public void Enqueue_Test(IEnumerable<KeyValuePair<object, string>> testCollection, ConcurrentQueue<string> queue)
        {
            foreach (var item in testCollection)
            {
                queue.Enqueue(item.Value);
            }
        }

        public void Dequeue_Test(IEnumerable<KeyValuePair<object, string>> testCollection, Queue<string> queue)
        {
            foreach (var item in testCollection)
            {
                var obj = queue.Dequeue();
            }
        }

        public void Enqueue_Test(IEnumerable<KeyValuePair<object, string>> testCollection, Queue<string> queue)
        {
            foreach (var item in testCollection)
            {
                queue.Enqueue(item.Value);
            }
        }

        public void Add_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IList<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Add(item.Value);
            }
        }

        public void Contains_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IList<string> registry)
        {
            foreach (var item in testCollection)
            {
                registry.Contains(item.Value);
            }
        }

        public void Iteration_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IList<string> registry)
        {
            foreach (var item in registry)
            {
                object r = item;
            }
        }

        public void GetByIndex_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IList<string> registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                string r = registry.ElementAt(i++);
            }
        }


        public void SetByIndex_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IList<string> registry)
        {
            int i = 0;
            foreach (var item in testCollection)
            {
                string r = registry[i++] = item.Value;
            }
        }

        public void Remove_Test(IEnumerable<KeyValuePair<object, string>> testCollection, IList<string> registry)
        {
            foreach (var item in testCollection.Skip(100000))
            {
                registry.Remove(item.Value);
            }
        }
    }
}
