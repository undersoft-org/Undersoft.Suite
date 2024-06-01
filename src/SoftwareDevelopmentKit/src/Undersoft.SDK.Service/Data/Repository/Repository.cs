using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Linq.Expressions;
using Undersoft.SDK.Service.Data.Cache;

namespace Undersoft.SDK.Service.Data.Repository;

using Data.Object;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Repository.Client;
using Undersoft.SDK.Service.Data.Repository.Pagination;
using Undersoft.SDK.Service.Data.Repository.Source;

public abstract partial class Repository<TEntity> : Repository, IPagedSet<TEntity>, IRepository<TEntity> where TEntity : class, IOrigin, IInnerProxy
{
    protected IDataCache cache;
    protected IQueryable<TEntity> query;

    public Repository()
    {
        ElementType = typeof(TEntity).GetDataType();
        Expression = Expression.Constant(this);
    }

    public Repository(IRepositoryClient repositorySource) : base(repositorySource)
    {
        ElementType = typeof(TEntity).GetDataType();
        Expression = Expression.Constant(this.AsEnumerable());
    }

    public Repository(IRepositorySource repositorySource) : base(repositorySource)
    {
        ElementType = typeof(TEntity).GetDataType();
        Expression = Expression.Constant(this.AsEnumerable());
    }

    public Repository(object context) : base(context)
    {
        ElementType = typeof(TEntity).GetDataType();
        Expression = Expression.Constant(this.AsEnumerable());
    }

    public Repository(IRepositoryContext context) : base(context)
    {
        ElementType = typeof(TEntity).GetDataType();
        Expression = Expression.Constant(this.AsEnumerable());
    }

    public Repository(IQueryProvider provider, Expression expression)
    {
        ElementType = typeof(TEntity).GetDataType();
        Provider = provider;
        Expression = expression;
    }

    public bool HasNextPage { get; set; }

    public bool HasPreviousPage { get; set; }

    public int IndexFrom { get; set; }

    public IList<TEntity> Items { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages { get; set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract IQueryable<TEntity> AsQueryable();

    public IEnumerator<TEntity> GetEnumerator()
    {
        return Provider.Execute<IQueryable<TEntity>>(Expression).GetEnumerator();
    }

    public override void LoadRemotesEvent(object sender, EntityEntryEventArgs e)
    {
        EntityEntry entry = e.Entry;
        object entity = entry.Entity;
        Type type = entity.GetDataType();

        if (type == ElementType)
        {
            RemoteProperties.DoEach(async (o) => await o.LoadAsync(entity));
        }
    }

    public TEntity Sign(TEntity entity)
    {
        entity.Sign(entity);
        cache?.Memorize(entity);
        return entity;
    }

    public TEntity Stamp(TEntity entity)
    {
        entity.Stamp(entity);
        cache?.Memorize(entity);
        return entity;
    }

    public abstract IQueryable<TEntity> Query { get; }
}

public enum RelatedType
{
    None = 0,
    Reference = 1,
    Collection = 2,
    Any = 3
}
