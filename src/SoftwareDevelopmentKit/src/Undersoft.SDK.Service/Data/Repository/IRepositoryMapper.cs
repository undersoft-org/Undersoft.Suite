namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryMapper<TEntity> where TEntity : class, IOrigin
    {
        ISeries<TEntity> HashMap<TDto>(IEnumerable<TDto> model, IEnumerable<TEntity> entity);
        ISeries<TDto> HashMap<TDto>(IEnumerable<TEntity> entity, IEnumerable<TDto> model);
        Task<ISeries<TEntity>> HashMapFrom<TDto>(IEnumerable<TDto> model);
        Task<ISeries<TDto>> HashMapTo<TDto>(IEnumerable<object> entity);
        Task<ISeries<TDto>> HashMapTo<TDto>(IEnumerable<TEntity> entity);
        IList<TEntity> Map<TDto>(IEnumerable<TDto> model, IEnumerable<TEntity> entity);
        IList<TDto> Map<TDto>(IEnumerable<TEntity> entity, IEnumerable<TDto> model);
        TEntity Map<TDto>(TDto model, TEntity entity);
        TDto Map<TDto>(TEntity entity, TDto model);
        IList<TEntity> MapFrom<TDto>(IEnumerable<TDto> model);
        TDto MapFrom<TDto>(object model);
        TEntity MapFrom<TDto>(TDto model);
        IAsyncEnumerable<TEntity> MapFromAsync<TDto>(IEnumerable<TDto> model);
        IList<TDto> MapTo<TDto>(IEnumerable<object> entity);
        IList<TDto> MapTo<TDto>(IEnumerable<TEntity> entity);
        TDto MapTo<TDto>(object entity);
        TDto MapTo<TDto>(TEntity entity) where TDto : class;
        IAsyncEnumerable<TDto> MapToAsync<TDto>(IEnumerable<TEntity> entity) where TDto : class;

        Task<IQueryable<TEntity>> QueryMapAsyncFrom<TDto>(IQueryable<TDto> model);
        Task<IQueryable<TDto>> QueryMapAsyncTo<TDto>(IQueryable<TEntity> entity) where TDto : class;
        IQueryable<TEntity> QueryMapFrom<TDto>(IQueryable<TDto> model);
        IQueryable<TDto> QueryMapTo<TDto>(IQueryable<TEntity> entity) where TDto : class;
    }
}