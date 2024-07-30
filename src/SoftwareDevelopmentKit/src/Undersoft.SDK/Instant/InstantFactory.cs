namespace Undersoft.SDK.Instant
{
    using Uniques;

    public static class InstantFactory
    {
        public static IInstant CreateInstant(object item, InstantType mode = InstantType.Derived)
        {
            var t = item.GetType();
            if (t.IsAssignableTo(typeof(IInstant)))
                return (IInstant)item;

            var key = t.UniqueKey32();
            if (!InstantGeneratorFactory.Cache.TryGet(key, out InstantGenerator figure))
                InstantGeneratorFactory.Cache.Add(key, figure = new InstantGenerator(t, mode));

            return figure.Generate();
        }

        public static IInstant CreateInstant<T>(T item, InstantType mode = InstantType.Derived)
        {
            var t = typeof(T);
            if (t.IsAssignableTo(typeof(IInstant)))
                return (IInstant)item;

            var key = t.UniqueKey32();
            if (!InstantGeneratorFactory.Cache.TryGet(key, out InstantGenerator figure))
                InstantGeneratorFactory.Cache.Add(key, figure = new InstantGenerator(t, mode));

            return figure.Generate();
        }
    }
}
