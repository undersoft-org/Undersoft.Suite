namespace Undersoft.SDK.Instant
{
    using Undersoft.SDK.Series;
    using Uniques;

    public static class InstantFactory
    {
        public static ISeries<InstantCreator> Cache = new Registry<InstantCreator>();

        private static InstantCreator GetCreator<T>(InstantType mode = InstantType.Derived)
        {
            return GetCreator(typeof(T), mode); 
        }

        private static InstantCreator GetCreator(Type type, InstantType mode = InstantType.Derived)
        {
            return GetCreator(type, type.UniqueKey32(), mode);
        }

        private static InstantCreator GetCreator(Type type, int key, InstantType mode = InstantType.Derived)
        {
            if (!Cache.TryGet(key, out InstantCreator figure))
            {
                Cache.Add(key, figure = new InstantCreator(type, mode));
            }
            return figure;
        }      

        public static InstantCreator GetCompiledCreator<T>(InstantType mode = InstantType.Derived)
        {
            var figure = GetCreator<T>(mode);
            figure.Create();
            return figure;
        }

        public static InstantCreator GetCompiledCreator(Type type, InstantType mode = InstantType.Derived)
        {
            var figure = GetCreator(type);
            figure.Create();
            return figure;
        }

        public static InstantCreator GetCompiledCreator(object item, InstantType mode = InstantType.Derived)
        {
            var figure = item.GetInstantCreator(mode);
            figure.Create();
            return figure;
        }

        public static InstantCreator GetCompiledCreator<T>(T item, InstantType mode = InstantType.Derived)
        {
            var figure = item.GetInstantCreator(mode);
            figure.Create();
            return figure;
        }       

        public static IInstant Create(object item, InstantType mode = InstantType.Derived)
        {
            var t = item.GetType();
            if (t.IsAssignableTo(typeof(IInstant)))
                return (IInstant)item;

            var key = t.UniqueKey32();
            if (!Cache.TryGet(key, out InstantCreator figure))
                Cache.Add(key, figure = new InstantCreator(t, mode));

            return figure.Create();
        }

        public static IInstant Create<T>(T item, InstantType mode = InstantType.Derived)
        {
            var t = typeof(T);
            if (t.IsAssignableTo(typeof(IInstant)))
                return (IInstant)item;

            var key = t.UniqueKey32();
            if (!Cache.TryGet(key, out InstantCreator figure))
                Cache.Add(key, figure = new InstantCreator(t, mode));

            return figure.Create();
        }
    }
}
