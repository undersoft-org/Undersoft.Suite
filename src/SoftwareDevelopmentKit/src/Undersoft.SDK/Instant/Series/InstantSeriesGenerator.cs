namespace Undersoft.SDK.Instant.Series
{
    using Rubrics;
    using SDK.Uniques;
    using System.Linq;
    using Undersoft.SDK.Utilities;

    public class InstantSeriesGenerator<T> : InstantSeriesGenerator
    {
        public InstantSeriesGenerator(InstantType mode = InstantType.Reference, bool threadSafe = true) : base(typeof(T), mode, threadSafe) { }

        public InstantSeriesGenerator(string seriesName, InstantType mode = InstantType.Reference, bool threadSafe = true)
            : base(typeof(T), seriesName, mode) { }
    }

    public class InstantSeriesGenerator : IInstantGenerator
    {
        protected Type compiledType;
        private InstantGenerator instant;

        protected long key;
        protected bool threadSafe;
        protected InstantSeriesMode mode;

        public InstantSeriesGenerator() { }

        public InstantSeriesGenerator(
            InstantGenerator instantGenerator,
            string seriesTypeName = null,
            bool threadSafe = true
        )
        {
            mode = InstantSeriesMode.instant;
            if (instantGenerator.Type == null)
                instantGenerator.Generate();
            this.threadSafe = threadSafe;
            this.instant = instantGenerator;
            Name =
                (!string.IsNullOrEmpty(seriesTypeName))
                    ? seriesTypeName
                    : instant.Name + "Series";
        }

        public InstantSeriesGenerator(IInstant instantObject, bool threadSafe = true)
            : this(
                new InstantGenerator(
                    instantObject.GetType(),
                    instantObject.GetType().Name,
                    InstantType.Reference
                ),
                null,
                threadSafe
            )
        { }

        public InstantSeriesGenerator(
            IInstant instantObject,
            string seriesTypeName,
            InstantType modeType = InstantType.Reference,
            bool threadSafe = true
        )
            : this(
                new InstantGenerator(instantObject.GetType(), instantObject.GetType().Name, modeType),
                seriesTypeName,
                threadSafe
            )
        { }

        public InstantSeriesGenerator(
            MemberRubrics instantRubrics,
            string seriesTypeName = null,
            string instantTypeName = null,
            InstantType modeType = InstantType.Reference,
            bool safeThread = true
        ) : this(new InstantGenerator(instantRubrics, instantTypeName, modeType), seriesTypeName, safeThread)
        { }

        public InstantSeriesGenerator(Type instantModelType, InstantType modeType, bool safeThread = true)
            : this(new InstantGenerator(instantModelType, null, modeType), null, safeThread) { }

        public InstantSeriesGenerator(
            Type instantModelType,
            string seriesTypeName,
            InstantType modeType,
            bool safeThread = true
        ) : this(new InstantGenerator(instantModelType, null, modeType), seriesTypeName, safeThread) { }

        public InstantSeriesGenerator(
            Type instantModelType,
            string seriesTypeName,
            string instantTypeName,
            InstantType modeType = InstantType.Reference,
            bool safeThread = true
        ) : this(new InstantGenerator(instantModelType, instantTypeName, modeType), seriesTypeName, safeThread)
        { }

        public Type BaseType { get; set; }

        public string Name { get; set; }

        public virtual IRubrics Rubrics
        {
            get => instant.Rubrics;
        }

        public virtual int Size
        {
            get => instant.Size;
        }

        public Type Type { get; set; }

        public virtual IInstantSeries Generate()
        {
            if (this.Type == null)
            {
                var ifc = new InstantSeriesCompiler(this, threadSafe);
                compiledType = ifc.CompileInstantType(Name);
                this.Type = compiledType.New().GetType();
                key = Unique.NewId;
            }
            return newinstants();
        }

        public virtual object New()
        {
            return newinstants();
        }

        public MemberRubrics CloneRubrics()
        {
            var rubrics = new MemberRubrics(Rubrics.ForEach(r => r.ShallowCopy(null)));
            rubrics.KeyRubrics = new MemberRubrics(
                Rubrics.KeyRubrics.ForEach(r => r.ShallowCopy(null))
            );
            rubrics.Update();
            return rubrics;
        }

        private IInstantSeries newinstants()
        {
            IInstantSeries newseries = newInstants((IInstantSeries)(Type.New()));
            newseries.Rubrics = CloneRubrics();
            newseries.KeyRubrics = newseries.Rubrics.KeyRubrics;
            newseries.View = newseries.AsQueryable();
            return newseries;
        }

        private IInstantSeries newInstants(IInstantSeries newseries)
        {
            newseries.InstantType = instant.Type;
            newseries.InstantSize = instant.Size;
            newseries.Type = this.Type;
            newseries.Instant = this;
            newseries.Prime = true;
            ((IIdentifiable)newseries).Id = key;
            ((IIdentifiable)newseries).TypeId = Name.UniqueKey64();

            return newseries;
        }
    }
}
