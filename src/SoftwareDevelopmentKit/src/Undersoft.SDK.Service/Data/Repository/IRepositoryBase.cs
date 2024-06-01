using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Undersoft.SDK.Service.Data.Mapper;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Repository;


namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepository : IRepositoryContext
    {
        Type ElementType { get; }

        Expression Expression { get; }

        IQueryProvider Provider { get; }

        IDataMapper Mapper { get; }

        CancellationToken Cancellation { get; set; }

        IEnumerable<IRemoteProperty> RemoteProperties { get; set; }

        void LoadRemotes(object entity);

        Task LoadRemotesAsync(object entity);

        void LoadRelated(EntityEntry entry, RelatedType relatedType);

        void LoadRelatedAsync(EntityEntry entry, RelatedType relatedType, CancellationToken token);
    }
}