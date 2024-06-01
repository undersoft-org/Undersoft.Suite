﻿namespace System.Collections.Generic
{
    using System.Collections.Concurrent;

    public static class ConcurrentDictionaryExtension
    {
        public static ConcurrentDictionary<T, S> Add<T, S>(
            this ConcurrentDictionary<T, S> source,
            IDictionary<T, S> collection
        )
        {
            if (collection == null)
                throw new ArgumentNullException("Collection is null");
            ConcurrentDictionary<T, S> result = new ConcurrentDictionary<T, S>();
            foreach (var item in collection)
            {
                if (!source.ContainsKey(item.Key))
                    source.AddOrUpdate(item.Key, item.Value, (k, v) => item.Value);
                else
                    result.AddOrUpdate(item.Key, item.Value, (k, v) => item.Value);
            }
            return result;
        }

        public static void Put<T, S>(
            this ConcurrentDictionary<T, Dictionary<T, S>> source,
            T key,
            T key2,
            S item
        )
        {
            if (key == null || item == null)
                throw new ArgumentNullException("Collection is null");

            if (!source.TryAdd(key, new Dictionary<T, S>() { { key2, item } }))
            {
                if (!source[key].ContainsKey(key2))
                    source[key].Add(key2, item);
                else
                    source[key][key2] = item;
            }
        }

        public static void Put<T, S>(
            this ConcurrentDictionary<T, S> source,
            IDictionary<T, S> collection
        )
        {
            if (collection == null)
                throw new ArgumentNullException("Collection is null");

            foreach (var item in collection)
            {
                source.AddOrUpdate(item.Key, item.Value, (k, v) => v = item.Value);
            }
        }
    }
}
