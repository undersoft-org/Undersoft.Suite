namespace Undersoft.SDK.Instant
{
    using Rubrics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Logging;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Uniques;

    public class InstantCreator<T> : InstantCreator
    {
        public InstantCreator(InstantType modeType = InstantType.Reference)
            : base(typeof(T), modeType) { }

        public InstantCreator(string createdTypeName, InstantType modeType = InstantType.Reference)
            : base(typeof(T), createdTypeName, modeType) { }
    }

    public class InstantCreator : IInstantCreator
    {
        protected MemberBuilderCreator memberBuilderCreator = new MemberBuilderCreator();
        protected ISeries<MemberBuilder> memberBuilders = new Registry<MemberBuilder>();
        private Type compiledType;

        public InstantCreator(
            IList<MemberInfo> instantMembers,
            InstantType modeType = InstantType.Reference
        ) : this(instantMembers.ToArray(), null, modeType) { }

        public InstantCreator(
            IList<MemberInfo> instantMembers,
            string createdTypeName,
            InstantType modeType = InstantType.Reference
        )
        {
            Name =
                (createdTypeName != null && createdTypeName != "")
                    ? createdTypeName
                    : DateTime.Now.ToBinary().ToString();

            Name += "Instant";

            mode = modeType;

            memberBuilderCreator = new MemberBuilderCreator();

            memberBuilders = memberBuilderCreator.Create(memberBuilderCreator.PrepareMembers(instantMembers));

            Rubrics = new MemberRubrics(memberBuilders.Select(m => m.Member).ToArray());
            Rubrics.KeyRubrics = new MemberRubrics();
        }

        public InstantCreator(
            MemberRubrics instantRubrics,
            string createdTypeName,
            InstantType modeType = InstantType.Reference
        ) : this(instantRubrics.ToArray(), createdTypeName, modeType) { }

        public InstantCreator(Type baseOnType, InstantType modeType = InstantType.Reference)
            : this(baseOnType, null, modeType) { }

        public InstantCreator(
            Type baseOnType,
            string createdTypeName,
            InstantType modeType = InstantType.Reference
        )
        {
            BaseType = baseOnType;

            if (modeType == InstantType.Derived)
                IsDerived = true;

            Name = createdTypeName == null ? baseOnType.Name : createdTypeName;
            Name += "Instant";
            mode = modeType;

            memberBuilderCreator = new MemberBuilderCreator();
            memberBuilders = memberBuilderCreator.Create(baseOnType);

            Rubrics = new MemberRubrics(memberBuilders.Select(m => m.Member).ToArray());
            Rubrics.KeyRubrics = new MemberRubrics();
        }

        public Type BaseType { get; set; }

        public bool IsDerived { get; set; }

        public string Name { get; set; }

        public IRubrics Rubrics { get; set; }

        public int Size { get; set; }

        public Type Type { get; set; }

        private InstantType mode { get; set; }

        private long? _seed = null;
        private long seed => _seed ??= Type.UniqueKey64();

        public IInstant Create()
        {
            if (this.Type == null)
            {
                try
                {
                    switch (mode)
                    {
                        case InstantType.Reference:
                            compileInstantType(
                                new InstantCompilerReferenceTypes(this, memberBuilders)
                            );
                            break;
                        case InstantType.ValueType:
                            compileInstantType(new InstantCompilerValueTypes(this, memberBuilders));
                            break;
                        case InstantType.Derived:
                            compileDerivedType(
                                new InstantCompilerDerivedTypes(this, memberBuilders)
                            );
                            break;
                        default:
                            break;
                    }

                    // Rubrics.Update();
                }
                catch (Exception ex)
                {
                    throw new InstantTypeCompilerException(
                        "Instant compilation at runtime failed see inner exception",
                        ex
                    );
                }
            }
            return create();
        }

        public object New()
        {
            if (this.Type == null)
                return Create();
            return this.Type.New();
        }

        private IInstant create()
        {
            if (this.Type == null)
                return Create();

            var figure = (IInstant)this.Type.New();
            figure.Id = Unique.NewId;
            figure.TypeId = seed;
            return figure;
        }

        private void compileDerivedType(InstantCompiler compiler)
        {
            var fcdt = compiler;
            compiledType = fcdt.CompileInstantType(Name);
            Rubrics.KeyRubrics.Add(fcdt.Identities.Values);
            Type = compiledType.New().GetType();
            if (!(Rubrics.AsValues().Any(m => m.Name == "code")))
            {
                var f = this.Type.GetField("code", BindingFlags.NonPublic | BindingFlags.Instance);

                if (!Rubrics.TryGet("code", out MemberRubric mr))
                {
                    mr = new MemberRubric(f);
                    mr.InstantField = f;
                    Rubrics.Insert(0, mr);
                }
                mr.RubricName = "code";
            }
            Rubrics.Update();
            try
            {
                Size = Marshal.SizeOf(Type);
            }
            catch (Exception ex)
            {
                this.Warning<Instantlog>("Marshal cannot establish size of type", null, ex);
                Size = Rubrics.BinarySize;
            }
        }

        private void compileInstantType(InstantCompiler compiler)
        {
            var fcvt = compiler;
            compiledType = fcvt.CompileInstantType(Name);
            Rubrics.KeyRubrics.Add(fcvt.Identities.Values);
            Type = compiledType.New().GetType();
            Rubrics.Update();
            try
            {
                Size = Marshal.SizeOf(Type);
            }
            catch (Exception ex)
            {
                this.Warning<Instantlog>("Marshal cannot establish size of type", null, ex);
                Size = Rubrics.BinarySize;
            }
        }

        private MemberRubric[] createMemberRurics(IList<MemberInfo> membersInfo)
        {
            return membersInfo
                .Select(
                    m =>
                        !(m is MemberRubric rubric)
                            ? m.MemberType == MemberTypes.Field
                                ? new MemberRubric((FieldInfo)m)
                                : m.MemberType == MemberTypes.Property
                                    ? new MemberRubric((PropertyInfo)m)
                                    : null
                            : rubric
                )
                .Where(p => p != null)
                .ToArray();
        }
    }

    public class InstantTypeCompilerException : Exception
    {
        public InstantTypeCompilerException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
