using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository;

public partial class Repository<TEntity>
{
    public virtual TEntity AddDocument<TDto>(TDto model) where TDto : class, IOrigin
    {
        return AddBy(Generalize(model));
    }
    public virtual IEnumerable<TEntity> AddDocument<TDto>(IEnumerable<TDto> model) where TDto : class, IOrigin
    {
        return AddBy(Generalize(model));
    }
    public virtual IEnumerable<TEntity> AddDocument<TDto>(IEnumerable<TDto> models, Func<TEntity, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin
    {
        return AddBy(Generalize(models), predicate).Commit();
    }
    public virtual Task<TEntity> PatchDocument<TDto>(TDto model) where TDto : class, IOrigin
    {
        return PatchBy(Generalize(model));
    }
    public virtual Task<TEntity> PatchDocument<TDto>(TDto model, params object[] keys) where TDto : class, IOrigin
    {
        return PatchBy(Generalize(model), keys);
    }
    public virtual Task<TEntity> PatchDocument<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin
    {
        return PatchBy(Generalize(model), predicate);
    }
    public virtual IEnumerable<TEntity> PatchDocument<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin
    {
        return PatchBy(Generalize(models), predicate).Commit();
    }
    public virtual IEnumerable<TEntity> PatchDocument<TDto>(IEnumerable<TDto> models) where TDto : class, IOrigin
    {
        return PatchBy(Generalize(models));
    }

}
