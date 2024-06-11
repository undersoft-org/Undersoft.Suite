namespace Undersoft.SDK.Proxies;

using Rubrics;
using System;
using System.Linq;
using Undersoft.SDK.Instant;
using Undersoft.SDK.Series;

public class ProxyCreator<T> : ProxyCreator
{
    public ProxyCreator() : base(typeof(T)) { }

    public ProxyCreator(string sleeveName) : base(typeof(T), sleeveName) { }
}

public class ProxyCreator : IInstantCreator
{
    private ISeries<MemberBuilder> rubricModels;
    private MemberBuilderCreator rubricBuilder;
    private Type compiledType;

    public ProxyCreator(Type figureModelType) : this(figureModelType, null) { }

    public ProxyCreator(Type figureModelType, string figureTypeName)
    {
        Traceable = figureModelType.IsAssignableTo(typeof(ITracedSeries));

        BaseType = figureModelType;

        Name = figureTypeName == null
            ? figureModelType.Name
            : figureTypeName;

        rubricBuilder = new MemberBuilderCreator();
        rubricModels = rubricBuilder.Create(figureModelType);

        Rubrics = new MemberRubrics(rubricModels.Select(m => m.Member).ToArray());
        Rubrics.KeyRubrics = new MemberRubrics();
    }

    public Type BaseType { get; set; }

    public string Name { get; set; }

    public IRubrics Rubrics { get; set; }

    public int Size { get; set; }

    public Type Type { get; set; }

    public bool Traceable { get; set; }

    public object New()
    {
        return Create();
    }

    public IProxy Create(object obj = null)
    {
        var s = create();
        if (obj == null)
            obj = BaseType.New();
        s.Target = obj;
        return s;
    }

    public IProxy create()
    {
        if (Type == null)
        {
            try
            {
                IProxy s = compile(new ProxyCompiler(this, rubricModels));
                Rubrics.Update();
                s.Rubrics = Rubrics;
                return s;
            }
            catch (Exception ex)
            {
                throw new SleeveCompilerException("ProxyCreator compilation at runtime failed see inner exception", ex);
            }
        }

        return activate();
    }

    private IProxy compile(ProxyCompiler compiler)
    {
        var fcdt = compiler;

        compiledType = fcdt.CompileProxyType(Name);

        Rubrics.KeyRubrics.Add(fcdt.Identities.Values.Select(v => Rubrics[v.Name]).ToArray());

        var obj = compiledType.New();

        Type = obj.GetType();

        Rubrics.ForEach(
            (f, y) => new object[]
            {
                f.FieldId = y,
                f.RubricId = y
            }).ToArray();

        return (IProxy)obj;
    }

    private IProxy activate()
    {
        var s = (IProxy)Type.New();
        s.Rubrics = Rubrics;
        return s;
    }

}

public class SleeveCompilerException : Exception
{
    public SleeveCompilerException(string message, Exception innerException) : base(message, innerException) { }
}