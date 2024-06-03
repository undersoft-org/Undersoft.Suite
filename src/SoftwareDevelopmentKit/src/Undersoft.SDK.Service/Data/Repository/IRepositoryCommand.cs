using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryCommand<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entity);
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        TEntity Add(TEntity entity);
        TEntity Add(TEntity entity, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        IAsyncEnumerable<TEntity> AddAsync(IAsyncEnumerable<TEntity> entity);
        IAsyncEnumerable<TEntity> AddAsync(IEnumerable<TEntity> entity);
        IAsyncEnumerable<TEntity> AddAsync(IEnumerable<TEntity> entities, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);

        TEntity Delete(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entity);
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        IEnumerable<TEntity> Delete(long[] ids);
        Task<TEntity> Delete(params object[] key);
        TEntity Delete(TEntity entity);
        TEntity Delete(TEntity entity, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        IAsyncEnumerable<TEntity> DeleteAsync(IEnumerable<TEntity> entity);
        IAsyncEnumerable<TEntity> DeleteAsync(IEnumerable<TEntity> entities, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);

        TEntity NewEntry(params object[] parameters);

        TEntity InnerPut<TModel>(TModel source, TEntity target) where TModel : class;

        TEntity InnerSet<TModel>(TModel source, TEntity target) where TModel : class;

        TEntity InnerPatch<TModel>(TModel source, TEntity target) where TModel : class;

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> Patch<TModel>(IEnumerable<TModel> entities, Func<TModel, Expression<Func<TEntity, bool>>> predicate, params Expression<Func<TEntity, object>>[] expanders) where TModel : class, IOrigin;
        IEnumerable<TEntity> Patch<TModel>(IEnumerable<TModel> entities, params Expression<Func<TEntity, object>>[] expanders) where TModel : class, IOrigin;

        TEntity Patch(TEntity entity);
        Task<TEntity> Patch<TModel>(TModel delta) where TModel : class, IOrigin;
        Task<TEntity> Patch<TModel>(TModel delta, Func<TModel, Expression<Func<TEntity, bool>>> predicate) where TModel : class, IOrigin;
        Task<TEntity> Patch<TModel>(TModel delta, params object[] keys) where TModel : class, IOrigin;
        IAsyncEnumerable<TEntity> PatchAsync<TModel>(IEnumerable<TModel> entities, Func<TModel, Expression<Func<TEntity, bool>>> predicate, params Expression<Func<TEntity, object>>[] expanders) where TModel : class, IOrigin;
        IAsyncEnumerable<TEntity> PatchAsync<TModel>(IEnumerable<TModel> entities, params Expression<Func<TEntity, object>>[] expanders) where TModel : class, IOrigin;

        IEnumerable<TEntity> Put(IEnumerable<TEntity> entities, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions);
        Task<TEntity> Put(TEntity entity, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions);
        IAsyncEnumerable<TEntity> PutAsync(IEnumerable<TEntity> entities, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions);

        IEnumerable<TEntity> Set<TModel>(IEnumerable<TModel> models) where TModel : class, IOrigin;
        IEnumerable<TEntity> Set<TModel>(IEnumerable<TModel> entities, Func<TModel, Expression<Func<TEntity, bool>>> predicate, params Func<TModel, Expression<Func<TEntity, bool>>>[] conditions) where TModel : class, IOrigin;
        Task<TEntity> Set<TModel>(TModel entity) where TModel : class, IOrigin;
        Task<TEntity> Set<TModel>(TModel entity, Func<TModel, Expression<Func<TEntity, bool>>> predicate, params Func<TModel, Expression<Func<TEntity, bool>>>[] conditions) where TModel : class, IOrigin;
        Task<TEntity> Set<TModel>(TModel entity, object key, Func<TEntity, Expression<Func<TEntity, bool>>> conditions) where TModel : class, IOrigin;
        Task<TEntity> Set<TModel>(TModel entity, Func<TModel, Expression<Func<TEntity, bool>>> predicate) where TModel : class, IOrigin;
        Task<TEntity> Set<TModel>(TModel entity, params object[] key) where TModel : class;
        IAsyncEnumerable<TEntity> SetAsync<TModel>(IEnumerable<TModel> models) where TModel : class, IOrigin;
        IAsyncEnumerable<TEntity> SetAsync<TModel>(IEnumerable<TModel> entities, Func<TModel, Expression<Func<TEntity, bool>>> predicate, params Func<TModel, Expression<Func<TEntity, bool>>>[] conditions) where TModel : class, IOrigin;
    }
}