namespace Undersoft.SDK.Proxies
{
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Rubrics;
    using Undersoft.SDK.Uniques;
    using Undersoft.SDK.Utilities;

    public class ProxySeriesGenerator<T> : ProxySeriesGenerator
    {
        public ProxySeriesGenerator() : base(typeof(T)) { }

        public ProxySeriesGenerator(bool threadSafe = true) : base(typeof(T), threadSafe) { }

        public ProxySeriesGenerator(string seriesName) : base(typeof(T), seriesName) { }

        public ProxySeriesGenerator(string seriesName, bool threadSafe = true)
            : base(typeof(T), seriesName, threadSafe) { }
    }

    public class ProxySeriesGenerator : InstantSeriesGenerator, IProxyGenerator
    {
        private ProxyGenerator proxy;

        public ProxySeriesGenerator(
            ProxyGenerator proxyGenerator,
            string seriesTypeName = null,
            bool safeThread = true
        ) : base()
        {
            mode = InstantSeriesMode.proxy;
            if (proxyGenerator.Type == null)
                proxyGenerator.Generate();
            threadSafe = safeThread;
            proxy = proxyGenerator;
            Name =
                string.IsNullOrEmpty(seriesTypeName)
                    ? seriesTypeName
                    : proxy.Name + "Series";
        }

        public ProxySeriesGenerator(IInstant instant, bool threadSafe = true)
            : this(
                new ProxyGenerator(instant.GetType(), instant.GetType().Name),
                null,
                threadSafe
            )
        { }

        public ProxySeriesGenerator(Type proxyModelType, bool threadSafe = true)
            : this(new ProxyGenerator(proxyModelType), null, threadSafe) { }

        public ProxySeriesGenerator(Type proxyModelType, string seriesName, bool threadSafe = true)
            : this(new ProxyGenerator(proxyModelType), seriesName, threadSafe) { }

        public virtual Type TargetType => proxy.BaseType;

        public override IRubrics Rubrics
        {
            get => proxy.Rubrics;
        }

        public override int Size
        {
            get => proxy.Size;
        }

        public override IInstantSeries Generate()
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

        public virtual ProxyGenerator GetProxyGenerator()
        {
            return proxy;
        }

        public virtual IProxy GenerateProxy(object target = null)
        {
            return proxy.Generate(target);
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
