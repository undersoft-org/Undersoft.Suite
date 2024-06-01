namespace System.Series.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Tests.Instant;

    public class RegistryTestHelper
    {
        public RegistryTestHelper()
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

        public ITypedSeries<Agreement> typedRegistry { get; set; }

        public IList<KeyValuePair<object, string>> stringKeyTestCollection { get; set; }

        public void Registry_Sync_Integrated_Test_Helper(IList<KeyValuePair<object, string>> testCollection)
        {
            Registry_Sync_Add_Test(testCollection);
            Registry_Sync_Count_Test(100000);
            Registry_Sync_SetByIndexer_Test(testCollection);
            Registry_Sync_First_Test(testCollection[0].Value);
            Registry_Sync_Last_Test(testCollection[99999].Value);
            Registry_Sync_Get_Test(testCollection);
            Registry_Sync_GetCard_Test(testCollection);
            Registry_Sync_Remove_Test(testCollection);
            Registry_Sync_Count_Test(70000);
            Registry_Sync_Enqueue_Test(testCollection);
            Registry_Sync_Count_Test(70005);
            Registry_Sync_Dequeue_Test(testCollection);
            Registry_Sync_Contains_Test(testCollection);
            Registry_Sync_ContainsKey_Test(testCollection);
            Registry_Sync_Put_Test(testCollection);
            Registry_Sync_Count_Test(100000);
            Registry_Sync_Clear_Test();
            Registry_Sync_Add_Value_Test(testCollection);
            Registry_Sync_Count_Test(100000);
            Registry_Sync_Remove_Value_Test(testCollection);
            Registry_Sync_Count_Test(70000);
            Registry_Sync_Put_Value_Test(testCollection);
            Registry_Sync_IndexOf_Test(testCollection);
            Registry_Sync_GetByIndexer_Test(testCollection);
            Registry_Sync_Count_Test(100000);
        }

        public void Registry_Async_Thread_Safe_Integrated_Test_Helper(
            IList<KeyValuePair<object, string>> testCollection
        )
        {
            Registry_Async_Add_Test(testCollection);
            Registry_Async_Get_Test(testCollection);
            Registry_Async_GetCard_Test(testCollection);
            Registry_Async_Remove_Test(testCollection);
            Registry_Async_Enqueue_Test(testCollection);
            Registry_Async_Dequeue_Test(testCollection);
            Registry_Async_Contains_Test(testCollection);
            Registry_Async_ContainsKey_Test(testCollection);
            Registry_Async_Put_Test(testCollection);
            Registry_Async_GetByIndexer_Test(testCollection);

            Debug.WriteLine($"Thread no {testCollection[0].Key.ToString()}_{registry.Count} ends");
        }

        private void Registry_Sync_Add_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            foreach (var item in testCollection)
            {
                registry.Add(item.Key, item.Value);
            }
        }

        private void Registry_Sync_Add_Value_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            foreach (var item in testCollection)
            {
                registry.Add(item.Value);
            }
        }

        private void Registry_Sync_SetByIndexer_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            foreach (var item in testCollection)
            {
                registry[item.Key] = item.Value;
            }
        }

        private void Registry_Sync_Clear_Test()
        {
            registry.Clear();
            Assert.IsFalse(registry.Any());
        }

        private void Registry_Sync_Contains_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                if (registry.Contains(registry.NewItem(item.Key, item.Value)))
                    items.Add(true);
            }
            Assert.AreEqual(70000, items.Count);
        }

        private void Registry_Sync_ContainsKey_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                if (registry.ContainsKey(item.Key))
                    items.Add(true);
            }
            Assert.AreEqual(70000, items.Count);
        }

        private void Registry_Sync_CopyTo_Test() { }

        private void Registry_Sync_Count_Test(int count)
        {
            Assert.AreEqual(count, registry.Count);
        }

        private void Registry_Sync_Dequeue_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string output = null;
                if (registry.TryDequeue(out output))
                    items.Add(output);
            }
            Assert.AreEqual(5, items.Count);
        }

        private void Registry_Sync_Enqueue_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection.Skip(70000).Take(5))
            {
                if (registry.Enqueue(item.Key, item.Value))
                    items.Add(true);
            }
            Assert.AreEqual(5, items.Count);
        }

        private void Registry_Sync_First_Test(string firstValue)
        {
            Assert.AreEqual(registry.Next(registry.First).Value, firstValue);
        }

        private void Registry_Sync_Get_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            foreach (var item in testCollection)
            {
                string r = registry.Get(item.Key);
                if (r != null)
                    items.Add(r);
            }
            Assert.AreEqual(100000, items.Count);
        }

        private void Registry_Sync_GetByIndexer_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            int i = 0;
            foreach (var item in testCollection)
            {
                string a = registry[i++];
                string b = item.Value;
            }
        }

        private void Registry_Sync_GetCard_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<ISeriesItem<string>> items = new List<ISeriesItem<string>>();
            foreach (var item in testCollection)
            {
                var r = registry.GetItem(item.Key);
                if (r != null)
                    items.Add(r);
            }
            Assert.AreEqual(100000, items.Count);
        }

        private void Registry_Sync_IndexOf_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<int> items = new List<int>();
            foreach (var item in testCollection.Skip(5000).Take(100))
            {
                int r = registry.IndexOf(item.Value);
                if (r > -1)
                    items.Add(r);
            }
        }

        private void Registry_Sync_Last_Test(string lastValue)
        {
            Assert.AreEqual(registry.Last.Value, lastValue);
        }

        private void Registry_Sync_Put_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            foreach (var item in testCollection)
            {
                registry.Put(item.Key, item.Value);
            }
        }

        private void Registry_Sync_Put_Value_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            foreach (var item in testCollection)
            {
                registry.Put(item.Value);
            }
        }

        private void Registry_Sync_Remove_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            foreach (var item in testCollection.Skip(70000))
            {
                var r = registry.Remove(item.Key);
                if (r != null)
                    items.Add(r);
            }
            Assert.AreEqual(30000, items.Count);
        }

        private void Registry_Sync_Remove_Value_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            foreach (var item in testCollection.Skip(70000))
            {
                string r = registry.Remove(item.Value);
                items.Add(r);
            }
            Assert.AreEqual(30000, items.Count);
        }

        private void Registry_Async_Add_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                items.Add(registry.Add(item.Key, item.Value));
            }
            Debug.WriteLine($"Add Thread no {testCollection[0].Key.ToString()}_{items.Count} ends");
        }

        private void Registry_Async_Add_V_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            foreach (var item in testCollection)
            {
                registry.Add(item.Value);
            }
        }

        private void Registry_Async_Contains_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                if (registry.Contains(registry.NewItem(item.Key, item.Value)))
                    items.Add(true);
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Key.ToString()}_{items.Count} ends"
            );
        }

        private void Registry_Async_ContainsKey_Test(
            IList<KeyValuePair<object, string>> testCollection
        )
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                if (registry.ContainsKey(item.Key))
                    items.Add(true);
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Key.ToString()}_{items.Count} ends"
            );
        }

        private void Registry_Async_Dequeue_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string output = null;
                if (registry.TryDequeue(out output))
                    items.Add(output);
            }
            Assert.AreEqual(5, items.Count);
        }

        private void Registry_Async_Enqueue_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection.Skip(5000).Take(5))
            {
                if (registry.Enqueue(item.Key, item.Value))
                    items.Add(true);
            }
            Assert.AreEqual(5, items.Count);
        }

        private void Registry_Async_Get_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            foreach (var item in testCollection)
            {
                string r = registry.Get(item.Key);
                if (r != null)
                    items.Add(r);
            }
            Debug.WriteLine($"Get Thread no {testCollection[0].Key.ToString()}_{items.Count} ends");
        }

        private void Registry_Async_GetByIndexer_Test(
            IList<KeyValuePair<object, string>> testCollection
        )
        {
            List<string> items = new List<string>();
            int i = 0;
            foreach (var item in testCollection)
            {
                items.Add(registry[i]);
            }
            Debug.WriteLine(
                $"Get By Indexer Thread no {testCollection[0].Key.ToString()}_{items.Count} ends"
            );
        }

        private void Registry_Async_GetCard_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<ISeriesItem<string>> items = new List<ISeriesItem<string>>();
            foreach (var item in testCollection)
            {
                var r = registry.GetItem(item.Key);
                if (r != null)
                    items.Add(r);
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Key.ToString()}_{items.Count} ends"
            );
        }

        private void Registry_Async_Put_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            foreach (var item in testCollection)
            {
                items.Add(registry.Put(item.Key, item.Value).Value);
            }
            Debug.WriteLine($"Put Thread no {testCollection[0].Key.ToString()}_{items.Count} ends");
        }

        private void Registry_Async_Put_V_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            foreach (var item in testCollection)
            {
                registry.Put(item.Value);
            }
        }

        private void Registry_Async_Remove_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            foreach (var item in testCollection.Skip(5000))
            {
                var r = registry.Remove(item.Key);
                if (r != null)
                    items.Add(r);
            }
            Debug.WriteLine(
                $"Removed Thread no {testCollection[0].Key.ToString()}_{items.Count} ends"
            );
        }

        private void Registry_Async_Remove_V_Test(IList<KeyValuePair<object, string>> testCollection)
        {
            List<string> items = new List<string>();
            foreach (var item in testCollection.Skip(5000))
            {
                string r = registry.Remove(item.Value);
                items.Add(r);
            }
            Debug.WriteLine(
                $"Removed V Thread no {testCollection[0].Key.ToString()}_{items.Count} ends"
            );
        }
    }
}
