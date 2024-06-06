using Undersoft.SDK.Service.Data.Cache;
using Undersoft.SDK.Service.Data.Mapper;

namespace Undersoft.SDK.Service.Data.Store;
public interface IStoreCache<TStore> : IDataCache
{
    IDataMapper Mapper { get; set; }
}