using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Undersoft.SDK.Workflows
{
    using Uniques;
    using Series;

    public class WorkPointer : Origin
    {
        public IntPtr Pointer {  get; set; }
    }
     
    public static class WorkPointers
    {
        private static TypedRegistry<WorkPointer> pointers;

        static WorkPointers()
        {
            pointers = new TypedRegistry<WorkPointer>(true, UniquePrimes.Get(7));
        }

        public static IEnumerable<T> GetRange<T>(long id)
        {
            if (pointers.TryGet(id, out ISeriesItem<WorkPointer> ptrs))
                return ptrs.ForEach(p => Target<T>(p));
            return default;
        }

        public static IEnumerable<T> GetRange<T>()
        {
            if (pointers.TryGet(ThreadId, out ISeriesItem<WorkPointer> ptrs))
                return ptrs.ForEach(p => Target<T>(p));
            return default;
        }

        public static IEnumerable<object> GetRange()
        {
            if (pointers.TryGet(ThreadId, out ISeriesItem<WorkPointer> ptrs))
                return ptrs.ForEach(p => Target(p));
            return default;
        }

        public static IEnumerable<T> GetTypedRange<T>(long id)
        {
            if (pointers.TryGet(typeof(T).FullName.UniqueKey64((uint)id), out ISeriesItem<WorkPointer> ptrs))
                return ptrs.ForEach(p => Target<T>(p));
            return default;
        }

        public static IEnumerable<T> GetTypedRange<T>()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            var key = typeof(T).FullName.UniqueKey64((uint)id);
            if (pointers.TryGet(key, out ISeriesItem<WorkPointer> ptrs))
                return ptrs.ForEach(p => Target<T>(p));
            return default;
        }

        public static IEnumerable<object> GetTypedRange(Type type)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            var key = type.FullName.UniqueKey64((uint)id);
            if (pointers.TryGet(key, out ISeriesItem<WorkPointer> ptrs))
                return ptrs.ForEach(p => Target(p));
            return default;
        }

        public static T Get<T>(long id)
        {
            if (pointers.TryGet(id, out WorkPointer ptr))
                return Target<T>(ptr);
            return default;
        }

        public static T Get<T>()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            if (pointers.TryGet(id, out WorkPointer ptr))
                return Target<T>(ptr);
            return default;
        }

        public static object Get()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            if (pointers.TryGet(id, out WorkPointer ptr))
                return Target(ptr);
            return default;
        }

        public static T GetTyped<T>()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            var key = typeof(T).FullName.UniqueKey64((uint)id);
            if (pointers.TryGet(key, out WorkPointer ptr))
                return Target<T>(ptr);
            return default;
        }

        public static object GetTyped(Type type)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            var key = type.FullName.UniqueKey64((uint)id);
            if (pointers.TryGet(key, out WorkPointer ptr))
                return Target(ptr);
            return default;
        }

        public static T GetTyped<T>(long id)
        {
            if (pointers.TryGet(typeof(T).FullName.UniqueKey64((uint)id), out WorkPointer ptr))
                return Target<T>(ptr);
            return default;
        }

        public static T RemoveTyped<T>()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            var key = typeof(T).FullName.UniqueKey64((uint)id);
            return Target<T>(pointers.Remove(key));
        }

        public static object RemoveTyped(Type type)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            var key = type.FullName.UniqueKey64((uint)id);
            return Target(pointers.Remove(key));
        }

        public static T Remove<T>()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            var key = typeof(T).FullName.UniqueKey64((uint)id);
            return Target<T>(pointers.Remove(key));
        }

        public static T Remove<T>(long key)
        {
            return Target<T>(pointers.Remove(key));
        }

        public static void RemoveRange<T>(IEnumerable<long> keys)
        {
            keys.ForEach(
                (k) =>
                {
                    pointers.Remove(k);
                }
            );
        }

        public static long SetTyped<T>(T item)
        {
            var key = typeof(T).FullName.UniqueKey64();
            pointers.Put(ThreadId, key, Address(item));
            return key;
        }

        public static long Set<T>(T item)
        {
            var id = ThreadId;
            pointers.Put(id, Address(item));
            return id;
        }

        private static void ChangeKey<T>(long key, long newkey)
        {
            pointers.Put(newkey, pointers.Remove(key));
        }

        public static long Add<T>(T item)
        {
            var id = ThreadId;
            pointers.Add(id, Address(item));
            return id;
        }

        public static long Add(object item)
        {
            var id = ThreadId;
            pointers.Add(id, Address(item));
            return id;
        }

        public static long Add<T>(T item, long key)
        {
            if (key == 0)
                key = item.UniqueKey();

            pointers.Add(key, Address(item));

            return key;
        }

        public static IEnumerable<long> AddRange<T>(IEnumerable<T> items)
        {
            return items.ForEach(p => Add(p));
        }

        public static IEnumerable<long> AddRange(params object[] items)
        {
            return items.ForEach(p => Add(p));
        }

        public static long AddTyped<T>(object item)
        {
            var key = typeof(T).FullName.UniqueKey64((uint)ThreadId);
            pointers.Put(key, Address(item));
            return key;
        }

        public static long AddTyped(Type type, object item)
        {
            var key = type.FullName.UniqueKey64((uint)ThreadId);
            pointers.Put(key, Address(item));
            return key;
        }

        public static Task<long> AddTypedAsync<T>(T item)
        {
            return Task.Run(() => SetTyped(item));
        }

        public static Task<long> AddAsync<T>(T item)
        {
            return Task.Run(() => Add(item));
        }

        public static Task<long> AddAsync<T>(T item, long id)
        {
            return Task.Run(() => Add(item, id));
        }

        private static WorkPointer Address<T>(T item)
        {
            return new WorkPointer() { Pointer = GCHandle.ToIntPtr(GCHandle.Alloc(item, GCHandleType.Normal)) };
        }

        private static T Target<T>(WorkPointer work)
        {
            return (T)GCHandle.FromIntPtr(work.Pointer).Target;
        }

        private static object Target(WorkPointer work)
        {
            return GCHandle.FromIntPtr(work.Pointer).Target;
        }

        private static int ThreadId => Thread.CurrentThread.ManagedThreadId;
    }
}
