namespace System.Series.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Tests.Instant;

    public class TypedCatalogTestHelper
    {
        public TypedCatalogTestHelper()
        {
            typedRegistry = new TypedCatalog<Agreement>();
            identifiableObjectTestCollection = PrepareTestListings.prepareIdentifiableObjectTestCollection();
        }

        public IList<Agreement> identifiableObjectTestCollection { get; set; }

        public ITypedSeries<Agreement> typedRegistry { get; set; }

        public void Typed_Catalog_Sync_Integrated_Test_Helper(IList<Agreement> testCollection)
        {
            Typed_Catalog_Sync_Add_Test(testCollection);
            Typed_Catalog_Sync_Count_Test(100000);
            Typed_Catalog_Sync_First_Test(testCollection[0]);
            Typed_Catalog_Sync_Last_Test(testCollection[99999]);
            Typed_Catalog_Sync_Get_Test(testCollection);
            Typed_Catalog_Sync_GetCard_Test(testCollection);
            Typed_Catalog_Sync_Remove_Test(testCollection);
            Typed_Catalog_Sync_Count_Test(70000);
            Typed_Catalog_Sync_Enqueue_Test(testCollection);
            Typed_Catalog_Sync_Count_Test(70005);
            Typed_Catalog_Sync_Dequeue_Test(testCollection);
            Typed_Catalog_Sync_Contains_Test(testCollection);
            Typed_Catalog_Sync_ContainsKey_Test(testCollection);
            Typed_Catalog_Sync_Put_Test(testCollection);
            Typed_Catalog_Sync_Count_Test(100000);
            Typed_Catalog_Sync_Clear_Test();
            Typed_Catalog_Sync_Add_V_Test(testCollection);
            Typed_Catalog_Sync_Count_Test(100000);
            Typed_Catalog_Sync_Remove_Value_Test(testCollection);
            Typed_Catalog_Sync_Count_Test(70000);
            Typed_Catalog_Sync_Put_Value_Test(testCollection);
            Typed_Catalog_Sync_IndexOf_Test(testCollection);
            Typed_Catalog_Sync_GetByIndexer_Test(testCollection);
            Typed_Catalog_Sync_Count_Test(100000);
        }

        public void Typed_Catalog_Async_Thread_Safe_Integrated_Test_Helper(
            IList<Agreement> testCollection
        )
        {
            Typed_Catalog_Async_Add_Test(testCollection);
            Typed_Catalog_Async_Get_Test(testCollection);
            Typed_Catalog_Async_GetCard_Test(testCollection);
            Typed_Catalog_Async_Remove_Test(testCollection);
            Typed_Catalog_Async_Enqueue_Test(testCollection);
            Typed_Catalog_Async_Dequeue_Test(testCollection);
            Typed_Catalog_Async_Contains_Test(testCollection);
            Typed_Catalog_Async_ContainsKey_Test(testCollection);
            Typed_Catalog_Async_Put_Test(testCollection);
            Typed_Catalog_Async_GetByIndexer_Test(testCollection);

            Debug.WriteLine($"Thread no {testCollection[0].Id.ToString()}_{typedRegistry.Count} ends");
        }

        private void Typed_Catalog_Sync_Add_Test(IList<Agreement> testCollection)
        {
            foreach (var item in testCollection)
            {
                typedRegistry.Add(item);
            }
        }

        private void Typed_Catalog_Sync_Add_V_Test(IList<Agreement> testCollection)
        {
            foreach (var item in testCollection)
            {
                typedRegistry.Add(item);
            }
        }

        private void Typed_Catalog_Sync_Clear_Test()
        {
            typedRegistry.Clear();
            Assert.IsFalse(typedRegistry.Any());
        }

        private void Catalog_Sync_SetByIndexer_Test(IList<Agreement> testCollection)
        {
            foreach (var item in testCollection)
            {
                typedRegistry[item.Id] = item;
            }
        }

        private void Typed_Catalog_Sync_Contains_Test(IList<Agreement> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                if (typedRegistry.Contains(typedRegistry.NewItem(item)))
                    items.Add(true);
            }
            Assert.AreEqual(70000, items.Count);
        }

        private void Typed_Catalog_Sync_ContainsKey_Test(IList<Agreement> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                if (typedRegistry.ContainsKey(item))
                    items.Add(true);
            }
            Assert.AreEqual(70000, items.Count);
        }

        private void Typed_Catalog_Sync_CopyTo_Test() { }

        private void Typed_Catalog_Sync_Count_Test(int count)
        {
            Assert.AreEqual(count, typedRegistry.Count);
        }

        private void Typed_Catalog_Sync_Dequeue_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            for (int i = 0; i < 5; i++)
            {
                if (typedRegistry.TryDequeue(out Agreement output))
                    items.Add(output);
            }
            Assert.AreEqual(5, items.Count);
        }

        private void Typed_Catalog_Sync_Enqueue_Test(IList<Agreement> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection.Skip(70000).Take(5))
            {
                if (typedRegistry.Enqueue(item))
                    items.Add(true);
            }
            Assert.AreEqual(5, items.Count);
        }

        private void Typed_Catalog_Sync_First_Test(Agreement firstValue)
        {
            Assert.AreEqual(typedRegistry.Next(typedRegistry.First).Value, firstValue);
        }

        private void Typed_Catalog_Sync_Get_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            foreach (var item in testCollection)
            {
                Agreement r = typedRegistry.Get(item);
                if (r != null)
                    items.Add(r);
                else
                    Thread.Sleep(1000);
            }
            Assert.AreEqual(100000, items.Count);
        }

        private void Typed_Catalog_Sync_GetByIndexer_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            int i = 0;
            foreach (var item in testCollection)
            {
                Agreement a = typedRegistry[i++];
                Agreement b = item;
            }
        }

        private void Typed_Catalog_Sync_GetCard_Test(IList<Agreement> testCollection)
        {
            List<ISeriesItem<Agreement>> items = new List<ISeriesItem<Agreement>>();
            foreach (var item in testCollection)
            {
                var r = typedRegistry.GetItem(item);
                if (r != null)
                    items.Add(r);
            }
            Assert.AreEqual(100000, items.Count);
        }

        private void Typed_Catalog_Sync_IndexOf_Test(IList<Agreement> testCollection)
        {
            List<int> items = new List<int>();
            foreach (var item in testCollection.Skip(5000).Take(100))
            {
                int r = typedRegistry.IndexOf(item);
                items.Add(r);
            }
        }

        private void Typed_Catalog_Sync_Last_Test(Agreement lastValue)
        {
            Assert.AreEqual(typedRegistry.Last.Value, lastValue);
        }

        private void Typed_Catalog_Sync_Put_Test(IList<Agreement> testCollection)
        {
            foreach (var item in testCollection)
            {
                typedRegistry.Put(item);
            }
        }

        private void Typed_Catalog_Sync_Put_Value_Test(IList<Agreement> testCollection)
        {
            foreach (var item in testCollection)
            {
                typedRegistry.Put(item);
            }
        }

        private void Typed_Catalog_Sync_Remove_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            foreach (var item in testCollection.Skip(70000))
            {
                var r = typedRegistry.Remove(item);
                if (r != null)
                    items.Add(r);
            }
            Assert.AreEqual(30000, items.Count);
        }

        private void Typed_Catalog_Sync_Remove_Value_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            foreach (var item in testCollection.Skip(70000))
            {
                Agreement r = typedRegistry.Remove(item);
                items.Add(r);
            }
            Assert.AreEqual(30000, items.Count);
        }

        private void Typed_Catalog_Async_Add_Test(IList<Agreement> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                items.Add(typedRegistry.TryAdd(item));
            }
            Debug.WriteLine($"Add Thread no {testCollection[0].Id.ToString()}_{items.Count} ends");
        }

        private void Typed_Catalog_Async_Add_Value_Test(IList<Agreement> testCollection)
        {
            foreach (var item in testCollection)
            {
                typedRegistry.Add(item);
            }
        }

        private void Typed_Catalog_Async_Contains_Test(IList<Agreement> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                if (typedRegistry.Contains(typedRegistry.NewItem(item)))
                    items.Add(true);
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Id.ToString()}_{items.Count} ends"
            );
        }

        private void Typed_Catalog_Async_ContainsKey_Test(
            IList<Agreement> testCollection
        )
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection)
            {
                if (typedRegistry.ContainsKey(item))
                    items.Add(true);
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Id.ToString()}_{items.Count} ends"
            );
        }

        private void Typed_Catalog_Async_Dequeue_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            for (int i = 0; i < 5; i++)
            {
                Agreement output = null;
                if (typedRegistry.TryDequeue(out output))
                    items.Add(output);
            }
            Assert.AreEqual(5, items.Count);
        }

        private void Typed_Catalog_Async_Enqueue_Test(IList<Agreement> testCollection)
        {
            List<bool> items = new List<bool>();
            foreach (var item in testCollection.Skip(5000).Take(5))
            {
                if (typedRegistry.Enqueue(item))
                    items.Add(true);
            }
            Assert.AreEqual(5, items.Count);
        }

        private void Typed_Catalog_Async_Get_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            foreach (var item in testCollection)
            {
                Agreement r = typedRegistry.Get(item);
                if (r != null)
                    items.Add(r);
            }
            Debug.WriteLine($"Get Thread no {testCollection[0].Id.ToString()}_{items.Count} ends");
        }

        private void Typed_Catalog_Async_GetByIndexer_Test(
            IList<Agreement> testCollection
        )
        {
            List<Agreement> items = new List<Agreement>();
            int i = 0;
            foreach (var item in testCollection)
            {
                items.Add(typedRegistry[i]);
            }
            Debug.WriteLine(
                $"Get By Indexer Thread no {testCollection[0].Id.ToString()}_{items.Count} ends"
            );
        }

        private void Typed_Catalog_Async_GetCard_Test(IList<Agreement> testCollection)
        {
            List<ISeriesItem<Agreement>> items = new List<ISeriesItem<Agreement>>();
            foreach (var item in testCollection)
            {
                var r = typedRegistry.GetItem(item);
                if (r != null)
                    items.Add(r);
            }
            Debug.WriteLine(
                $"Get Card Thread no {testCollection[0].Id.ToString()}_{items.Count} ends"
            );
        }

        private void Typed_Catalog_Async_Put_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            foreach (var item in testCollection)
            {
                items.Add(typedRegistry.Put(item).Value);
            }
            Debug.WriteLine($"Put Thread no {testCollection[0].Id.ToString()}_{items.Count} ends");
        }

        private void Typed_Catalog_Async_Put_V_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            foreach (var item in testCollection)
            {
                typedRegistry.Put(item);
            }
        }

        private void Typed_Catalog_Async_Remove_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            foreach (var item in testCollection.Skip(5000))
            {
                var r = typedRegistry.Remove(item);
                if (r != null)
                    items.Add(r);
            }
            Debug.WriteLine(
                $"Removed Thread no {testCollection[0].Id.ToString()}_{items.Count} ends"
            );
        }

        private void Typed_Catalog_Async_Remove_Value_Test(IList<Agreement> testCollection)
        {
            List<Agreement> items = new List<Agreement>();
            foreach (var item in testCollection.Skip(5000))
            {
                Agreement r = typedRegistry.Remove(item);
                items.Add(r);
            }
            Debug.WriteLine(
                $"Removed V Thread no {testCollection[0].Id.ToString()}_{items.Count} ends"
            );
        }
    }
}
