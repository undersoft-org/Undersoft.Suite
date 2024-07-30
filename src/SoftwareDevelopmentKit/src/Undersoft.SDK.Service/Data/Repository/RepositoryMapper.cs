using Microsoft.EntityFrameworkCore;

namespace Undersoft.SDK.Service.Data.Repository;

using Series;
using Undersoft.SDK.Proxies;

public partial class Repository<TEntity> : IRepositoryMapper<TEntity>
    where TEntity : class, IOrigin, IInnerProxy
{
    public virtual TEntity Map<TDto>(TDto model, TEntity entity)
    {
        return model.PutTo<TDto, TEntity>(entity);
    }

    public virtual TDto Map<TDto>(TEntity entity, TDto model)
    {
        return entity.PutTo<TEntity, TDto>(model);
    }

    public virtual IList<TEntity> Map<TDto>(IEnumerable<TDto> model, IEnumerable<TEntity> entity)
    {
        return MapById(model, entity);
    }

    public virtual IList<TDto> Map<TDto>(IEnumerable<TEntity> entity, IEnumerable<TDto> model)
    {
        return MapById(entity, model);
    }

    public virtual ISeries<TEntity> MapById<TDto>(
        IEnumerable<TDto> model,
        IEnumerable<TEntity> entity
    )
    {
        var _entity = entity.ToListing();
        model.ForEach(e =>
        {
            if (_entity.TryGet(e, out TEntity output))
                output.PutTo(e);
        });
        return _entity;
    }

    public virtual ISeries<TDto> MapById<TDto>(IEnumerable<TEntity> entity, IEnumerable<TDto> model)
    {
        var _model = model.ToListing();
        entity.ForEach(e =>
        {
            if (_model.TryGet(e, out TDto output))
                output.PutTo(e);
        });
        return _model;
    }

    public virtual TDto MapTo<TDto>(TEntity entity) where TDto : class
    {
        return entity.PutTo<TDto>();
    }

    public virtual TDto Generalize<TDto>(TDto model)
    {
        SerializeDocuments((IInnerProxy)model);
        return model;
    }

    public virtual TDto MapTo<TDto>(object entity)
    {
        return entity.PutTo<TDto>();
    }

    public virtual TEntity MapFrom<TDto>(TDto model)
    {
        return model.PutTo<TEntity>();
    }

    public virtual TDto MapFrom<TDto>(object model)
    {
        return model.PutTo<TDto>();
    }

    public virtual IList<TDto> MapTo<TDto>(IEnumerable<object> entity)
    {
        return entity.ForEach(e => e.PutTo<TDto>()).Commit();
    }

    public virtual IList<TDto> MapToList<TDto>(IEnumerable<TEntity> entity)
    {
        return entity.ForEach(e => e.PutTo<TDto>()).Commit();
    }

    public virtual async IAsyncEnumerable<TDto> MapToAsync<TDto>(IEnumerable<TEntity> entity)
        where TDto : class
    {
        foreach (var item in entity)
            yield return await Task.Run(() => item.PutTo<TDto>());
    }

    public virtual IList<TEntity> MapToList<TDto>(IEnumerable<TDto> model)
    {
        return model.ForEach(m => m.PutTo<TEntity>()).Commit();
    }

    public virtual IList<TEntity> GeneralizeToList<TDto>(IEnumerable<TDto> model)
    {
        return model.ForEach(e =>
        {
            SerializeDocuments((IInnerProxy)e);
            return e.PutTo<TEntity>();
        }).Commit();
    }

    public virtual IEnumerable<TDto> Generalize<TDto>(IEnumerable<TDto> model)
    {
        foreach (var item in model)
        {
            SerializeDocuments((IInnerProxy)item);
            yield return item;
        }
    }

    public virtual async IAsyncEnumerable<TEntity> MapToAsync<TDto>(IEnumerable<TDto> model)
    {
        foreach (var item in model)
            yield return await Task.Run(() => item.PutTo<TEntity>());
    }

    public virtual Task<ISeries<TDto>> KeyedMapAsync<TDto>(IEnumerable<object> entity)
    {
        return Task.Run(
            () => (ISeries<TDto>)entity.ForEach(m => m.PutTo<TDto>()).ToListing(),
            Cancellation
        );
    }

    public virtual IEnumerable<TDto> MapTo<TDto>(IEnumerable<TEntity> entities)
    {
        return entities.ForEach(e => e.PutTo<TDto>());
    }

    public virtual Task<ISeries<TDto>> KeyedMapAsync<TDto>(IEnumerable<TEntity> entity)
    {
        return Task.Run(
            () => (ISeries<TDto>)(entity.ForEach(m => m.PutTo<TEntity>())).ToListing(),
            Cancellation
        );
    }

    public virtual Task<ISeries<TEntity>> KeyedMapAsync<TDto>(IEnumerable<TDto> model)
    {
        return Task.Run(
            () => (ISeries<TEntity>)(model.ForEach(m => m.PutTo<TEntity>())).ToListing(),
            Cancellation
        );
    }

    public virtual async Task<IQueryable<TDto>> MapQueryAsync<TDto>(IQueryable<TEntity> entity)
        where TDto : class
    {
        return await Task.FromResult(
            entity.AsEnumerable().ForEach(e => e.PutTo<TDto>()).AsQueryable()
        );
    }

    public virtual async Task<IQueryable<TDto>> DetalizeQueryAsync<TDto>(IQueryable<TEntity> entity)
        where TDto : class
    {
        return await Task.FromResult(
            entity
                .AsEnumerable()
                .ForEach(e =>
                {
                    var contract = e.PutTo<TDto>();
                    DeserializeDocuments((IInnerProxy)contract);
                    return contract;
                })
                .AsQueryable()
        );
    }

    public virtual IQueryable<TDto> MapQuery<TDto>(IQueryable<TEntity> entity) where TDto : class
    {
        return entity.ForEach(e => e.PutTo<TDto>());
    }

    public virtual IQueryable<TEntity> MapQuery<TDto>(IQueryable<TDto> model)
    {
        return model.ForEach(m => m.PutTo<TEntity>());
    }

    public virtual async Task<IQueryable<TEntity>> MapQueryAsync<TDto>(IQueryable<TDto> model)
    {
        return await model.ForEachAsync(m => m.PutTo<TEntity>());
    }
}
