using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Remote;

using Rubrics;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Store;

public abstract class RemoteRelation<TOrigin, TTarget> : RemoteRelation, IRemoteRelation<TOrigin, TTarget>
    where TOrigin : class, IOrigin, IInnerProxy where TTarget : class, IOrigin, IInnerProxy
{
    public RemoteRelation()
    {
        Name = typeof(TOrigin).FullName + "__&__" + typeof(TTarget).FullName;
        Id = Name.UniqueKey();

        OpenDataRegistry.Remotes.Add(Name, this);
        OpenDataRegistry.Remotes.Add(typeof(TOrigin), this);

    }

    public virtual Expression<Func<TOrigin, object>> SourceKey { get; set; }
    public virtual Expression<Func<IRemoteLink<TOrigin, TTarget>, object>> JoinKey { get; set; }
    public virtual Expression<Func<TTarget, object>> TargetKey { get; set; }

    public virtual Func<TOrigin, Expression<Func<TTarget, bool>>> Predicate { get; set; }

    public virtual Expression<Func<TOrigin, IEnumerable<IRemoteLink<TOrigin, TTarget>>>> MiddleSet { get; set; }

    public abstract Expression<Func<TTarget, bool>> CreatePredicate(object entity);

    public override MemberRubric RemoteRubric => DataStoreRegistry.GetRemoteMember<TOrigin, TTarget>();

}

public abstract class RemoteRelation : Origin, IRemoteRelation
{
    protected RemoteRelation() { }

    public virtual string Name { get; set; }

    public virtual Towards Towards { get; set; }

    public virtual IUnique Empty => Uscn.Empty;

    public virtual MemberRubric RemoteRubric { get; }
}

public enum Towards
{
    None,
    ToSet,
    ToSingle,
    SetToSet
}
