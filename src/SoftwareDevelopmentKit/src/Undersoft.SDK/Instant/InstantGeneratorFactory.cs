namespace Undersoft.SDK.Instant
{
    using Undersoft.SDK.Series;
    using Uniques;

    public static class InstantGeneratorFactory
    {
        public static ISeries<InstantGenerator> Cache = new Registry<InstantGenerator>();

        private static InstantGenerator GetOrCreateGenerator<T>(InstantType mode = InstantType.Derived)
        {
            return GetOrCreateGenerator(typeof(T), mode);
        }

        private static InstantGenerator GetOrCreateGenerator(Type type, InstantType mode = InstantType.Derived)
        {
            return GetOrCreateGenerator(type, type.UniqueKey32(), mode);
        }

        private static InstantGenerator GetOrCreateGenerator(Type type, int key, InstantType mode = InstantType.Derived)
        {
            if (!Cache.TryGet(key, out InstantGenerator figure))
            {
                Cache.Add(key, figure = new InstantGenerator(type, mode));
            }
            return figure;
        }

        public static InstantGenerator CreateGenerator<T>(InstantType mode = InstantType.Derived)
        {
            var figure = GetOrCreateGenerator<T>(mode);
            figure.Generate();
            return figure;
        }

        public static InstantGenerator CreateGenerator(Type type, InstantType mode = InstantType.Derived)
        {
            var figure = GetOrCreateGenerator(type);
            figure.Generate();
            return figure;
        }

        public static InstantGenerator CreateGenerator(object item, InstantType mode = InstantType.Derived)
        {
            var figure = item.GetInstantGenerator(mode);
            figure.Generate();
            return figure;
        }

        public static InstantGenerator CreateGenerator<T>(T item, InstantType mode = InstantType.Derived)
        {
            var figure = item.GetInstantGenerator(mode);
            figure.Generate();
            return figure;
        }
    }
}
