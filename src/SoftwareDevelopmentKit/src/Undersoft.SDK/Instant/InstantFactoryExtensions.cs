namespace Undersoft.SDK.Instant
{
    using Uniques;

    public static class InstantFactoryExtensions
    {
        public static IInstant ToInstant<T>(this T item, InstantType mode = InstantType.Derived)
        {
            Type t = typeof(T);
            if (t.IsInterface)
                return InstantFactory.Create((object)item, mode);

            return InstantFactory.Create(item, mode);
        }

        public static IInstant ToInstant(this Type type, InstantType mode = InstantType.Derived)
        {
            return InstantFactory.Create(type.New(), mode);
        }

        public static InstantCreator GetInstantCreator(this object item, InstantType mode = InstantType.Derived)
        {
            var t = item.GetType();
            var key = t.UniqueKey32();
            if (!InstantFactory.Cache.TryGet(key, out InstantCreator figure))
            {
                InstantFactory.Cache.Add(key, figure = new InstantCreator(t, mode));
            }
            figure.Create();
            return figure;
        }

        public static InstantCreator GetInstantCreator<T>(this T item, InstantType mode = InstantType.Derived)
        {
            var t = typeof(T);
            var key = t.UniqueKey32();
            if (!InstantFactory.Cache.TryGet(key, out InstantCreator figure))
            {
                InstantFactory.Cache.Add(key, figure = new InstantCreator(t, mode));
            }
            figure.Create();
            return figure;
        }

        public static IInstant ToInstant(this object item, InstantType mode = InstantType.Derived)
        {
            return ToInstant(item, mode);
        }
    }
}
