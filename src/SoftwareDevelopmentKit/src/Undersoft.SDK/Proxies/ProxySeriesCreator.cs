namespace Undersoft.SDK.Proxies
{
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Rubrics;
    using Undersoft.SDK.Uniques;

    public class ProxySeriesCreator<T> : ProxySeriesCreator
    {
        public ProxySeriesCreator() : base(typeof(T)) { }

        public ProxySeriesCreator(bool threadSafe = true) : base(typeof(T), threadSafe) { }

        public ProxySeriesCreator(string seriesName) : base(typeof(T), seriesName) { }

        public ProxySeriesCreator(string seriesName, bool threadSafe = true)
            : base(typeof(T), seriesName, threadSafe) { }
    }

    public class ProxySeriesCreator : InstantSeriesCreator, IProxyCreator
    {
        private ProxyCreator proxy;

        public ProxySeriesCreator(
            ProxyCreator proxyGenerator,
            string seriesTypeName = null,
            bool safeThread = true
        ) : base()
        {
            mode = InstantSeriesMode.proxy;
            if (proxyGenerator.Type == null)
                proxyGenerator.Create();
            threadSafe = safeThread;
            proxy = proxyGenerator;
            Name =
                seriesTypeName != null && seriesTypeName != ""
                    ? seriesTypeName
                    : proxy.Name + "_Proxy";
        }

        public ProxySeriesCreator(IInstant instant, bool threadSafe = true)
            : this(
                new ProxyCreator(instant.GetType(), instant.GetType().Name),
                null,
                threadSafe
            )
        { }

        public ProxySeriesCreator(Type proxyModelType, bool threadSafe = true)
            : this(new ProxyCreator(proxyModelType), null, threadSafe) { }

        public ProxySeriesCreator(Type proxyModelType, string seriesName, bool threadSafe = true)
            : this(new ProxyCreator(proxyModelType), seriesName, threadSafe) { }

        public virtual Type TargetType => proxy.BaseType;

        public override IRubrics Rubrics
        {
            get => proxy.Rubrics;
        }

        public override int Size
        {
            get => proxy.Size;
        }

        public override IInstantSeries Create()
        {
            if (Type == null)
            {
                var ifc = new InstantSeriesCompiler(this, threadSafe);
                compiledType = ifc.CompileInstantType(Name);
                Type = compiledType.New().GetType();
                key = Unique.NewId;
            }
            return newProxies();
        }

        public virtual ProxyCreator GetProxyCreator()
        {
            return proxy;
        }

        public virtual IProxy CreateProxy(object target = null)
        {
            return proxy.Create(target);
        }

        public override object New()
        {
            return newProxies();
        }

        private IInstantSeries newProxies()
        {
            IInstantSeries newproxies = newProxies((IInstantSeries)Type.New());
            newproxies.Rubrics = CloneRubrics();
            newproxies.KeyRubrics = newproxies.Rubrics.KeyRubrics;
            newproxies.View = newproxies.AsQueryable();
            return newproxies;
        }

        private IInstantSeries newProxies(IInstantSeries newproxies)
        {
            newproxies.InstantType = proxy.Type;
            newproxies.InstantSize = proxy.Size;
            newproxies.Type = Type;
            newproxies.Instant = this;
            newproxies.Prime = true;
            newproxies.Id = key;
            newproxies.TypeId = Name.UniqueKey64();

            return newproxies;
        }
    }
}
