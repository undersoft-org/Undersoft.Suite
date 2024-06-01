namespace Undersoft.SDK.Updating
{
    using Invoking;

    public static class UpdaterExtensions
    {
        public static E PatchTo<T, E>(this T item, E target, IInvoker traceChanges = null)
            where T : class
            where E : class
        {
            return new Updater<T>(item, traceChanges).Patch(target);
        }

        public static object PatchTo(this object item, object target, IInvoker traceChanges = null)
        {
            return new Updater(item, traceChanges).Patch(target);
        }

        public static T PatchFrom<T, E>(this T item, E source, IInvoker traceChanges = null)
           where T : class
           where E : class
        {
            return new Updater<E>(source, traceChanges).Patch(item);
        }

        public static object PatchFrom(this object item, object source, IInvoker traceChanges = null)
        {
            return new Updater(source, traceChanges).Patch(item);
        }

        public static E PatchTo<T, E>(this T item, IInvoker traceChanges = null)
            where T : class
            where E : class
        {
            return new Updater<T>(item, traceChanges).Patch<E>();
        }

        public static E PatchTo<E>(this object item, IInvoker traceChanges = null)
          where E : class
        {
            return new Updater(item, traceChanges).Patch<E>();
        }

        public static E PutTo<T, E>(this T item, E target, IInvoker traceChanges = null)
            where T : class
            where E : class
        {
            return new Updater<T>(item, traceChanges).Put(target);
        }

        public static object PutTo(this object item, object target, IInvoker traceChanges = null)
        {
            return new Updater(item, traceChanges).Put(target);
        }

        public static E PutTo<T, E>(this T item, IInvoker traceChanges = null)
            where T : class
            where E : class
        {
            return new Updater<T>(item, traceChanges).Put<E>();
        }

        public static E PutTo<E>(this object item, IInvoker traceChanges = null)
           where E : class
        {
            return new Updater(item, traceChanges).Put<E>();
        }

        public static T PutFrom<T, E>(this T item, E source, IInvoker traceChanges = null)
         where T : class
         where E : class
        {
            return new Updater<E>(source, traceChanges).Put(item);
        }

        public static object PutFrom(this object item, object source, IInvoker traceChanges = null)
        {
            return new Updater(source, traceChanges).Put(item);
        }

        public static object ShallowPatchTo(this object item, object target, IInvoker traceChanges = null)
        {
            return new Updater(item, traceChanges).ShallowPatch(target);
        }

        public static object ShallowPatchFrom(this object item, object source, IInvoker traceChanges = null)
        {
            return new Updater(source, traceChanges).ShallowPatch(item);
        }

        public static object ShallowPutTo(this object item, object target, IInvoker traceChanges = null)
        {
            return new Updater(item, traceChanges).ShallowPut(target);
        }

        public static object ShallowPutFrom(this object item, object source, IInvoker traceChanges = null)
        {
            return new Updater(source, traceChanges).ShallowPut(item);
        }
    }
}
