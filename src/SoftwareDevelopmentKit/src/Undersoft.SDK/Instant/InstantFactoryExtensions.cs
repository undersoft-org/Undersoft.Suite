namespace Undersoft.SDK.Instant
{
    using Undersoft.SDK.Utilities;
    using Uniques;

    public static class InstantFactoryExtensions
    {
        public static IInstant ToInstant<T>(this T item, InstantType mode = InstantType.Derived)
        {
            Type t = typeof(T);
            if (t.IsInterface)
                return InstantFactory.CreateInstant((object)item, mode);

            return InstantFactory.CreateInstant(item, mode);
        }

        public static IInstant ToInstant(this Type type, InstantType mode = InstantType.Derived)
        {
            return InstantFactory.CreateInstant(type.New(), mode);
        }

        public static InstantGenerator GetInstantGenerator(this object item, InstantType mode = InstantType.Derived)
        {
            var t = item.GetType();
            var key = t.UniqueKey32();
            if (!InstantGeneratorFactory.Cache.TryGet(key, out InstantGenerator figure))
            {
                InstantGeneratorFactory.Cache.Add(key, figure = new InstantGenerator(t, mode));
            }
            figure.Generate();
            return figure;
        }

        public static InstantGenerator GetInstantGenerator<T>(this T item, InstantType mode = InstantType.Derived)
        {
            var t = typeof(T);
            var key = t.UniqueKey32();
            if (!InstantGeneratorFactory.Cache.TryGet(key, out InstantGenerator figure))
            {
                InstantGeneratorFactory.Cache.Add(key, figure = new InstantGenerator(t, mode));
            }
            figure.Generate();
            return figure;
        }

        public static IInstant ToInstant(this object item, InstantType mode = InstantType.Derived)
        {
            return ToInstant(item, mode);
        }
    }
}
