using Microsoft.OData.Client;
using Undersoft.SDK.Service.Data.Repository;

namespace Undersoft.SDK.Service.Data.Remote.Repository
{
    public interface IRemoteSynchronizer
    {
        void AddRepository(IRepository repository);

        void OnLinked(object sender, LoadCompletedEventArgs args);

        void AcquireLinker();

        void ReleaseLinker();

        void AcquireResult();

        void ReleaseResult();
    }
}

