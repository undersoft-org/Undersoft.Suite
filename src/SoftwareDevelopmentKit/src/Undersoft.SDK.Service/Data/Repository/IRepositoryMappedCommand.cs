using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryMappedCommand<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        IEnumerable<TEntity> AddBy<TDto>(IEnumerable<TDto> model);
        IEnumerable<TEntity> AddBy<TDto>(IEnumerable<TDto> models, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        TEntity AddBy<TDto>(TDto model);
        TEntity AddBy<TDto>(TDto model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        Task<TEntity> AddByAsync<TDto>(TDto model);
        Task<TEntity> AddByAsync<TDto>(TDto model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        IAsyncEnumerable<TEntity> AddByAsync<TDto>(IEnumerable<TDto> model);
        IAsyncEnumerable<TEntity> AddByAsync<TDto>(IEnumerable<TDto> models, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);

        IEnumerable<TEntity> DeleteBy<TDto>(IEnumerable<TDto> model);
        IEnumerable<TEntity> DeleteBy<TDto>(IEnumerable<TDto> model, Func<TDto, Expression<Func<TEntity, bool>>> predicate);
        Task<TEntity> DeleteBy<TDto>(TDto model);
        Task<TEntity> DeleteBy<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate);
        IAsyncEnumerable<TEntity> DeleteByAsync<TDto>(IEnumerable<TDto> model);
        IAsyncEnumerable<TEntity> DeleteByAsync<TDto>(IEnumerable<TDto> model, Func<TDto, Expression<Func<TEntity, bool>>> predicate);

        IEnumerable<TEntity> PatchBy<TDto>(IEnumerable<TDto> entity) where TDto : class, IOrigin;
        IEnumerable<TEntity> PatchBy<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin;
        Task<TEntity> PatchBy<TDto>(TDto model) where TDto : class, IOrigin;
        Task<TEntity> PatchBy<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin;
        Task<TEntity> PatchBy<TDto>(TDto model, params object[] keys) where TDto : class, IOrigin;
        IAsyncEnumerable<TEntity> PatchByAsync<TDto>(IEnumerable<TDto> entity) where TDto : class, IOrigin;
        IAsyncEnumerable<TEntity> PatchByAsync<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin;

        IEnumerable<TEntity> PutBy<TDto>(IEnumerable<TDto> model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions);
        Task<TEntity> PutBy<TDto>(TDto model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions);
        IAsyncEnumerable<TEntity> PutByAsync<TDto>(IEnumerable<TDto> model, Func<TEntity, Expression<Func<TEntity, bool>>> predicate, params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions);

        IEnumerable<TEntity> SetBy<TDto>(IEnumerable<TDto> entity) where TDto : class, IOrigin;
        IEnumerable<TEntity> SetBy<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate, params Func<TDto, Expression<Func<TEntity, bool>>>[] conditions) where TDto : class, IOrigin;
        Task<TEntity> SetBy<TDto>(TDto model) where TDto : class, IOrigin;
        Task<TEntity> SetBy<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate, params Func<TDto, Expression<Func<TEntity, bool>>>[] conditions) where TDto : class, IOrigin;
        Task<TEntity> SetBy<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin;
        Task<TEntity> SetBy<TDto>(TDto model, params object[] keys) where TDto : class, IOrigin;
        IAsyncEnumerable<TEntity> SetByAsync<TDto>(IEnumerable<TDto> entity) where TDto : class, IOrigin;
        IAsyncEnumerable<TEntity> SetByAsync<TDto>(IEnumerable<TDto> models, Func<TDto, Expression<Func<TEntity, bool>>> predicate, params Func<TDto, Expression<Func<TEntity, bool>>>[] conditions) where TDto : class, IOrigin;
    }
}