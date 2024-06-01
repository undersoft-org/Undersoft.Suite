namespace Undersoft.SDK.Instant.Series
{
    using Rubrics;
    using SDK.Uniques;
    using System.Linq;

    public class InstantSeriesCreator<T> : InstantSeriesCreator
    {
        public InstantSeriesCreator(InstantType mode = InstantType.Reference, bool threadSafe = true) : base(typeof(T), mode, threadSafe) { }

        public InstantSeriesCreator(string seriesName, InstantType mode = InstantType.Reference, bool threadSafe = true)
            : base(typeof(T), seriesName, mode) { }
    }

    public class InstantSeriesCreator : IInstantCreator
    {
        protected Type compiledType;
        private InstantCreator instant;

        protected long key;
        protected bool threadSafe;
        protected InstantSeriesMode mode;

        public InstantSeriesCreator() { }

        public InstantSeriesCreator(
            InstantCreator instantGenerator,
            string seriesTypeName = null,
            bool threadSafe = true
        )
        {
            mode = InstantSeriesMode.instant;
            if (instantGenerator.Type == null)
                instantGenerator.Create();
            this.threadSafe = threadSafe;
            this.instant = instantGenerator;
            Name =
                (seriesTypeName != null && seriesTypeName != "")
                    ? seriesTypeName
                    : instant.Name + "_Instant";
        }

        public InstantSeriesCreator(IInstant instantObject, bool threadSafe = true)
            : this(
                new InstantCreator(
                    instantObject.GetType(),
                    instantObject.GetType().Name,
                    InstantType.Reference
                ),
                null,
                threadSafe
            )
        { }

        public InstantSeriesCreator(
            IInstant instantObject,
            string seriesTypeName,
            InstantType modeType = InstantType.Reference,
            bool threadSafe = true
        )
            : this(
                new InstantCreator(instantObject.GetType(), instantObject.GetType().Name, modeType),
                seriesTypeName,
                threadSafe
            )
        { }

        public InstantSeriesCreator(
            MemberRubrics instantRubrics,
            string seriesTypeName = null,
            string instantTypeName = null,
            InstantType modeType = InstantType.Reference,
            bool safeThread = true
        ) : this(new InstantCreator(instantRubrics, instantTypeName, modeType), seriesTypeName, safeThread)
        { }

        public InstantSeriesCreator(Type instantModelType, InstantType modeType, bool safeThread = true)
            : this(new InstantCreator(instantModelType, null, modeType), null, safeThread) { }

        public InstantSeriesCreator(
            Type instantModelType,
            string seriesTypeName,
            InstantType modeType,
            bool safeThread = true
        ) : this(new InstantCreator(instantModelType, null, modeType), seriesTypeName, safeThread) { }

        public InstantSeriesCreator(
            Type instantModelType,
            string seriesTypeName,
            string instantTypeName,
            InstantType modeType = InstantType.Reference,
            bool safeThread = true
        ) : this(new InstantCreator(instantModelType, instantTypeName, modeType), seriesTypeName, safeThread)
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

        public virtual IInstantSeries Create()
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
            var rubrics = new MemberRubrics(Rubrics.Select(r => r.ShallowCopy(null)));
            rubrics.KeyRubrics = new MemberRubrics(
                Rubrics.KeyRubrics.Select(r => r.ShallowCopy(null))
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
