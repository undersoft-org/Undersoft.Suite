using Microsoft.EntityFrameworkCore;

namespace Undersoft.SDK.Service.Data.Repository;

using Series;
using Undersoft.SDK.Proxies;

public partial class Repository<TEntity> : IRepositoryMapper<TEntity>
    where TEntity : class, IOrigin, IInnerProxy
{
    public virtual TEntity Map<TDto>(TDto model, TEntity entity)
    {
        return Mapper.Map(model, entity);
    }

    public virtual TDto Map<TDto>(TEntity entity, TDto model)
    {
        return Mapper.Map(entity, model);
    }

    public virtual IList<TEntity> Map<TDto>(IEnumerable<TDto> model, IEnumerable<TEntity> entity)
    {
        return (IList<TEntity>)Mapper.Map(model, entity).ToList();
    }

    public virtual IList<TDto> Map<TDto>(IEnumerable<TEntity> entity, IEnumerable<TDto> model)
    {
        return (IList<TDto>)(Mapper.Map(entity, model).ToList());
    }

    public virtual ISeries<TEntity> HashMap<TDto>(
        IEnumerable<TDto> model,
        IEnumerable<TEntity> entity
    )
    {
        return (ISeries<TEntity>)Mapper.Map(model, entity).ToListing();
    }

    public virtual ISeries<TDto> HashMap<TDto>(IEnumerable<TEntity> entity, IEnumerable<TDto> model)
    {
        return (ISeries<TDto>)(Mapper.Map(entity, model).ToListing());
    }

    public virtual TDto MapTo<TDto>(TEntity entity) where TDto : class
    {
        return entity.PutTo<TDto>();
    }

    public virtual TDto MapTo<TDto>(object entity)
    {
        return Mapper.Map<TDto>(entity);
    }

    public virtual TEntity MapFrom<TDto>(TDto model)
    {
        return Mapper.Map<TDto, TEntity>(model);
    }

    public virtual TDto MapFrom<TDto>(object model)
    {
        return Mapper.Map<TDto>(model);
    }

    public virtual IList<TDto> MapTo<TDto>(IEnumerable<object> entity)
    {
        return Mapper.Map<IList<TDto>>(entity.Commit());
    }

    public virtual IList<TDto> MapTo<TDto>(IEnumerable<TEntity> entity)
    {
        return Mapper.Map<IList<TDto>>(entity.Commit());
    }

    public virtual async IAsyncEnumerable<TDto> MapToAsync<TDto>(IEnumerable<TEntity> entity) where TDto : class
    {
        foreach (var item in entity)
            yield return await Task.Run(() => item.PutTo<TDto>());
    }

    public virtual IList<TEntity> MapFrom<TDto>(IEnumerable<TDto> model)
    {
        return Mapper.Map<TDto[], IList<TEntity>>(model.Commit());
    }

    public virtual async IAsyncEnumerable<TEntity> MapFromAsync<TDto>(IEnumerable<TDto> model)
    {
        foreach (var item in model)
            yield return await Task.Run(() => Mapper.Map<TDto, TEntity>(item));
    }

    public virtual Task<ISeries<TDto>> HashMapTo<TDto>(IEnumerable<object> entity)
    {
        return Task.Run(
            () => (ISeries<TDto>)(Mapper.Map<IEnumerable<TDto>>(entity.ToArray())).ToChain(),
            Cancellation
        );
    }

    public virtual IEnumerable<TDto> YieldMapTo<TDto>(IEnumerable<TEntity> entities)
    {
        return entities.ForEach(e => Mapper.Map<TDto>(e));
    }

    public virtual Task<ISeries<TDto>> HashMapTo<TDto>(IEnumerable<TEntity> entity)
    {
        return Task.Run(
            () => (ISeries<TDto>)(Mapper.Map<IEnumerable<TDto>>(entity.ToArray())).ToChain(),
            Cancellation
        );
    }

    public virtual Task<ISeries<TEntity>> HashMapFrom<TDto>(IEnumerable<TDto> model)
    {
        return Task.Run(
            () =>
                (ISeries<TEntity>)
                    (
                        Mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(model.ToArray())
                    ).ToChain(),
            Cancellation
        );
    }

    public virtual async Task<IQueryable<TDto>> QueryMapAsyncTo<TDto>(IQueryable<TEntity> entity)
        where TDto : class
    {
        return await Task.FromResult(entity.AsEnumerable().ForEach(e => e.PutTo<TDto>()).AsQueryable());
    }

    public virtual IQueryable<TDto> QueryMapTo<TDto>(IQueryable<TEntity> entity) where TDto : class
    {
        return entity.ForEach(e => e.PutTo<TDto>());
    }

    public virtual IQueryable<TEntity> QueryMapFrom<TDto>(IQueryable<TDto> model)
    {
        return model.ForEach(m => m.PutTo<TEntity>());
    }

    public virtual async Task<IQueryable<TEntity>> QueryMapAsyncFrom<TDto>(IQueryable<TDto> model)
    {
        return await model.ForEachAsync(m => m.PutTo<TEntity>());
    }
}
