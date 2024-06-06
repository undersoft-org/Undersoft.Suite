using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Remote;

using Undersoft.SDK.Proxies;

public class RemoteOneToOne<TOrigin, TTarget> : RemoteRelation<TOrigin, TTarget> where TOrigin : class, IOrigin, IInnerProxy where TTarget : class, IOrigin, IInnerProxy
{

    private Func<TTarget, object> targetKey;
    private Func<TOrigin, object> originKey;

    public RemoteOneToOne() : base()
    {
    }
    public RemoteOneToOne(Expression<Func<TOrigin, object>> originkey,
                               Expression<Func<TTarget, object>> targetkey)
                               : base()
    {
        Towards = Towards.ToSingle;
        SourceKey = originkey;
        TargetKey = targetkey;

        originKey = originkey.Compile();
        targetKey = targetkey.Compile();
    }

    public override Expression<Func<TTarget, bool>> CreatePredicate(object entity)
    {
        return LinqExtensions.GetEqualityExpression(TargetKey, originKey, (TOrigin)entity);
    }
}
