namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryMapper<TEntity> where TEntity : class, IOrigin
    {
        ISeries<TEntity> MapById<TDto>(IEnumerable<TDto> model, IEnumerable<TEntity> entity);
        ISeries<TDto> MapById<TDto>(IEnumerable<TEntity> entity, IEnumerable<TDto> model);
        Task<ISeries<TEntity>> KeyedMapAsync<TDto>(IEnumerable<TDto> model);
        Task<ISeries<TDto>> KeyedMapAsync<TDto>(IEnumerable<object> entity);
        Task<ISeries<TDto>> KeyedMapAsync<TDto>(IEnumerable<TEntity> entity);
        IList<TEntity> Map<TDto>(IEnumerable<TDto> model, IEnumerable<TEntity> entity);
        IList<TDto> Map<TDto>(IEnumerable<TEntity> entity, IEnumerable<TDto> model);
        TEntity Map<TDto>(TDto model, TEntity entity);
        TDto Map<TDto>(TEntity entity, TDto model);
        IList<TEntity> MapToList<TDto>(IEnumerable<TDto> model);
        TDto MapFrom<TDto>(object model);
        TEntity MapFrom<TDto>(TDto model);
        IAsyncEnumerable<TEntity> MapToAsync<TDto>(IEnumerable<TDto> model);
        IList<TDto> MapTo<TDto>(IEnumerable<object> entity);
        IList<TDto> MapToList<TDto>(IEnumerable<TEntity> entity);
        TDto MapTo<TDto>(object entity);
        TDto MapTo<TDto>(TEntity entity) where TDto : class;
        IAsyncEnumerable<TDto> MapToAsync<TDto>(IEnumerable<TEntity> entity) where TDto : class;

        Task<IQueryable<TEntity>> MapQueryAsync<TDto>(IQueryable<TDto> model);
        Task<IQueryable<TDto>> MapQueryAsync<TDto>(IQueryable<TEntity> entity) where TDto : class;
        IQueryable<TEntity> MapQuery<TDto>(IQueryable<TDto> model);
        IQueryable<TDto> MapQuery<TDto>(IQueryable<TEntity> entity) where TDto : class;
    }
}