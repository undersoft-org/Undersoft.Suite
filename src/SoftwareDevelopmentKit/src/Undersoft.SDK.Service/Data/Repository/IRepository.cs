using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepository<TEntity>
        : IRepositoryMapper<TEntity>,
            IRepositoryQuery<TEntity>,
            IRepositoryCommand<TEntity>,
            IRepositoryMappedCommand<TEntity>,
            IRepositoryIndexer<TEntity>,
            IRepositoryDocumentCommands<TEntity>,
            IRepository,
            IOrderedQueryable<TEntity>,
            IEnumerable<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        IQueryable<TEntity> Query { get; }

        IQueryable<TEntity> AsQueryable();

        void LoadRemotesEvent(object sender, EntityEntryEventArgs e);

        TEntity Sign(TEntity entity);

        TEntity Stamp(TEntity entity);
    }
}
