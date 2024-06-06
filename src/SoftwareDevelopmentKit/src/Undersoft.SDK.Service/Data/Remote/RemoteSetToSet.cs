using System.Collections;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Remote;

public class RemoteSetToSet<TOrigin, TTarget> : RemoteRelation<TOrigin, TTarget>
    where TOrigin : class, IOrigin, IInnerProxy
    where TTarget : class, IOrigin, IInnerProxy
{
    private IRubric _nodeRubric;
    private Expression<Func<TTarget, object>> _targetKey;
    private Func<IRemoteLink<TOrigin, TTarget>, object> _joinKey;

    public RemoteSetToSet() : base() { }

    public RemoteSetToSet(
        Expression<Func<IRemoteLink<TOrigin, TTarget>, object>> joinKey,
        Expression<Func<TTarget, object>> targetKey
    ) : base()
    {
        Towards = Towards.SetToSet;
        JoinKey = joinKey;
        TargetKey = targetKey;

        _joinKey = joinKey.Compile();
        _targetKey = targetKey;

        Predicate = (o) => CreatePredicate(o);
    }

    public override Expression<Func<TTarget, bool>> CreatePredicate(object entity)
    {
        var proxy = ((IInnerProxy)entity).Proxy;
        var nodeRubric = _nodeRubric ??= GetNodeRubric(proxy.Rubrics);

        if (nodeRubric == null)
            return null;

        return LinqExtension.GetWhereInExpression(
            TargetKey,
            ((IEnumerable<IRemoteLink<TOrigin, TTarget>>)proxy[nodeRubric.RubricId])?.Select(
                _joinKey
            )
        );
    }

    private IRubric GetNodeRubric(IRubrics rubrics)
    {
        return rubrics.Where(r =>
        {
            if (r.RubricType.IsAssignableTo(typeof(IEnumerable)))
            {
                var elemType = r.RubricType.GetEnumerableElementType();
                if (elemType.IsAssignableTo(typeof(IRemoteLink)))
                {
                    var targetType = elemType.GetGenericArguments().LastOrDefault();
                    var originType = elemType.GetGenericArguments().FirstOrDefault();

                    if (
                        (
                            targetType.Name.Equals(typeof(TTarget).Name)
                            && originType.Name.Equals(typeof(TOrigin).Name)
                        )
                        || (
                            originType.Name.Equals(typeof(TTarget).Name)
                            && targetType.Name.Equals(typeof(TOrigin).Name)
                        )
                    )
                        return true;
                }
            }
            return false;
        }).FirstOrDefault();
    }
}
