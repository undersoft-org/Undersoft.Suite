using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository;

public interface IRepositoryGeneralizedCommands<TEntity>
{
    TEntity GeneralizedAddBy<TDto>(TDto model) where TDto : class, IOrigin;

    IEnumerable<TEntity> GeneralizedAddBy<TDto>(IEnumerable<TDto> model) where TDto : class, IOrigin;

    IEnumerable<TEntity> GeneralizedAddBy<TDto>(
        IEnumerable<TDto> models,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicate
    ) where TDto : class, IOrigin;

    Task<TEntity> GeneralizedPatchBy<TDto>(TDto model) where TDto : class, IOrigin;

    IEnumerable<TEntity> GeneralizedPatchBy<TDto>(
        IEnumerable<TDto> models,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate
    ) where TDto : class, IOrigin;

    IEnumerable<TEntity> GeneralizedPatchBy<TDto>(IEnumerable<TDto> models) where TDto : class, IOrigin;
}
