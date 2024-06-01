using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;

public partial class Repository<TEntity> : IRepositoryMappedCommand<TEntity> where TEntity : class, IOrigin, IInnerProxy
{
    public virtual TEntity AddBy<TDto>(TDto model)
    {
        return this.Add(MapFrom(model));
    }
    public virtual TEntity AddBy<TDto>(TDto model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate)
    {
        return Add(MapFrom(model), predicate);
    }
    public virtual async Task<TEntity> AddByAsync<TDto>(TDto model)
    {
        return await Task.Run(() => AddBy<TDto>(model));
    }
    public virtual async Task<TEntity> AddByAsync<TDto>(TDto model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate)
    {
        return await Task.Run(() => AddBy<TDto>(model, predicate));
    }
    public virtual IEnumerable<TEntity> AddBy<TDto>(IEnumerable<TDto> model)
    {
        return Add(MapFrom(model));
    }
    public virtual IEnumerable<TEntity> AddBy<TDto>(IEnumerable<TDto> models, Func<TEntity, Expression<Func<TEntity, bool>>> predicate)
    {
        return Add(MapFrom(models), predicate).Commit();
    }

    public virtual IAsyncEnumerable<TEntity> AddByAsync<TDto>(IEnumerable<TDto> model)
    {
        return AddAsync(MapFromAsync(model));
    }
    public virtual IAsyncEnumerable<TEntity> AddByAsync<TDto>(IEnumerable<TDto> models, Func<TEntity, Expression<Func<TEntity, bool>>> predicate)
    {
        return AddAsync(MapFrom(models));
    }

    public virtual async Task<TEntity> DeleteBy<TDto>(TDto model)
    {
        return await this.Delete(((IIdentifiable)model).Id);
    }
    public virtual async Task<TEntity> DeleteBy<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate)
    {
        if (predicate != null)
            return Delete(predicate.Invoke(model));
        return await DeleteBy(model);
    }
    public virtual IEnumerable<TEntity> DeleteBy<TDto>(IEnumerable<TDto> model)
    {
        return Delete(MapFrom(model));
    }
    public virtual IEnumerable<TEntity> DeleteBy<TDto>(IEnumerable<TDto> model, Func<TDto, Expression<Func<TEntity, bool>>> predicate)
    {
        foreach (var dto in model)
        {
            yield return Delete(predicate.Invoke(dto));
        }
    }

    public virtual IAsyncEnumerable<TEntity> DeleteByAsync<TDto>(IEnumerable<TDto> model)
    {
        return DeleteAsync(MapFrom(model));
    }
    public virtual async IAsyncEnumerable<TEntity> DeleteByAsync<TDto>(IEnumerable<TDto> model, Func<TDto, Expression<Func<TEntity, bool>>> predicate)
    {
        foreach (var dto in model)
        {
            yield return await Task.Run(() => Delete(predicate.Invoke(dto)));
        }
    }

    public virtual async Task<TEntity> SetBy<TDto>(TDto model) where TDto : class, IOrigin
    {
        return await Set(model);
    }
    public virtual async Task<TEntity> SetBy<TDto>(TDto model, params object[] keys) where TDto : class, IOrigin
    {
        return await Set(model, keys);
    }
    public virtual async Task<TEntity> SetBy<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate, params Func<TDto, Expression<Func<TEntity, bool>>>[] conditions) where TDto : class, IOrigin
    {
        return await Set(model, predicate(model), conditions.ForEach(c => c(model)));
    }
    public virtual IEnumerable<TEntity> SetBy<TDto>(IEnumerable<TDto> entity) where TDto : class, IOrigin
    {
        return Set(entity).Commit();
    }
    public virtual async Task<TEntity> SetBy<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin
    {
        return await Set(model, predicate);
    }
    public virtual IEnumerable<TEntity> SetBy<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate, params Func<TDto, Expression<Func<TEntity, bool>>>[] conditions) where TDto : class, IOrigin
    {
        return Set(models, predicate, conditions).Commit();
    }

    public virtual IAsyncEnumerable<TEntity> SetByAsync<TDto>(IEnumerable<TDto> entity) where TDto : class, IOrigin
    {
        return SetAsync(entity);
    }
    public virtual IAsyncEnumerable<TEntity> SetByAsync<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate, params Func<TDto, Expression<Func<TEntity, bool>>>[] conditions) where TDto : class, IOrigin
    {
        return SetAsync(models, predicate, conditions);
    }

    public virtual async Task<TEntity> PutBy<TDto>(TDto model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions)
    {
        return await Put(MapFrom(model), predicate, conditions);
    }
    public virtual IEnumerable<TEntity> PutBy<TDto>(IEnumerable<TDto> model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions)
    {
        return Put(MapFrom(model), predicate, conditions).Commit();
    }

    public virtual IAsyncEnumerable<TEntity> PutByAsync<TDto>(IEnumerable<TDto> model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions)
    {
        return PutAsync(MapFrom(model), predicate, conditions);
    }

    public virtual async Task<TEntity> PatchBy<TDto>(TDto model) where TDto : class, IOrigin
    {
        return await Patch(model);
    }
    public virtual IEnumerable<TEntity> PatchBy<TDto>(IEnumerable<TDto> entity) where TDto : class, IOrigin
    {
        return Patch(entity).Commit();
    }
    public virtual async Task<TEntity> PatchBy<TDto>(TDto model, params object[] keys) where TDto : class, IOrigin
    {
        return await Patch(model, keys);
    }
    public virtual async Task<TEntity> PatchBy<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin
    {
        return await Patch(model, predicate);
    }
    public virtual IEnumerable<TEntity> PatchBy<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin
    {
        return Patch(models, predicate).Commit();
    }

    public virtual IAsyncEnumerable<TEntity> PatchByAsync<TDto>(IEnumerable<TDto> entity) where TDto : class, IOrigin
    {
        return PatchAsync(entity);
    }
    public virtual IAsyncEnumerable<TEntity> PatchByAsync<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin
    {
        return PatchAsync(models, predicate);
    }
}
