using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class RemoteDataContext<TStore>
        where TStore : IDataServiceStore
    {
        protected ISeries<Arguments> CommandRegistry = new Registry<Arguments>();

        protected ApiDataContext ApiData;

        protected OpenDataContext OpenData;

        public RemoteDataContext(Uri serviceUri) { }

    }
}
