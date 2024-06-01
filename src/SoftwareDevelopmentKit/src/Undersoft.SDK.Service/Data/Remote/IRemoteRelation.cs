using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Remote;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics;

public interface IRemoteRelation<TOrigin, TTarget> : IRemoteRelation where TOrigin : class, IOrigin, IInnerProxy where TTarget : class, IOrigin, IInnerProxy
{
    Expression<Func<TOrigin, object>> SourceKey { get; set; }

    Expression<Func<TTarget, object>> TargetKey { get; set; }

    Expression<Func<IRemoteLink<TOrigin, TTarget>, object>> JoinKey { get; set; }

    Expression<Func<TOrigin, IEnumerable<IRemoteLink<TOrigin, TTarget>>>> MiddleSet { get; set; }

    Func<TOrigin, Expression<Func<TTarget, bool>>> Predicate { get; set; }

    Expression<Func<TTarget, bool>> CreatePredicate(object entity);
}

public interface IRemoteRelation : IIdentifiable
{
    string Name { get; }

    Towards Towards { get; set; }

    MemberRubric RemoteRubric { get; }
}
