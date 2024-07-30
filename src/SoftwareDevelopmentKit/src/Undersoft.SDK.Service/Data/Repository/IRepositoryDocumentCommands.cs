using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository;

public interface IRepositoryDocumentCommands<TEntity>
{
    TEntity AddDocument<TDto>(TDto model) where TDto : class, IOrigin;

    IEnumerable<TEntity> AddDocument<TDto>(IEnumerable<TDto> model) where TDto : class, IOrigin;

    IEnumerable<TEntity> AddDocument<TDto>(
        IEnumerable<TDto> models,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicate
    ) where TDto : class, IOrigin;

    Task<TEntity> PatchDocument<TDto>(TDto model) where TDto : class, IOrigin;

    Task<TEntity> PatchDocument<TDto>(TDto model, params object[] keys) where TDto : class, IOrigin;

    Task<TEntity> PatchDocument<TDto>(TDto model, Func<TDto, Expression<Func<TEntity, bool>>> predicate) where TDto : class, IOrigin;

    IEnumerable<TEntity> PatchDocument<TDto>(
        IEnumerable<TDto> models,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    ) where TDto : class, IOrigin;

    IEnumerable<TEntity> PatchDocument<TDto>(IEnumerable<TDto> models) where TDto : class, IOrigin;
}
